using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speedx = 5f;
    public float rotationSpeed = 10f;
    private Rigidbody rb;
    [SerializeField] Joystick joystick;
    public GameObject Bulletprefab;
    [SerializeField] private Transform FilePoint;
    public float bulletSpeed = 20f;
    public Coroutine ShootingCorotine;
    public bool isMoving = false;
    public bool isShooting = false;
    public Transform CurrentTager;
    private Animator animator;

    private enum PlayerState { idle, Walk }
    //private PlayerState currentState = PlayerState.idle;
    private  void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        if (joystick == null)
        {
            joystick = FindObjectOfType<Joystick>();
        }
    }
  
 

    protected virtual void FixedUpdate()
    {
        if (rb == null)
        {
            Debug.LogError("Rigidbody bị null!");
            return;
        }
        Move();
        HandleState();

    }
 
   protected virtual void Update()
    {
        CheckEnemy();
    }
    protected void CheckEnemy()
    {
        if (!isMoving && CurrentTager != null) // Nếu đứng yên và có Enemy
        {
            Vector3 directionToEnemy = (CurrentTager.position - transform.position).normalized;
            float angle = Vector3.Angle(transform.forward, directionToEnemy);

            if (angle < 10f) // Nếu Player đang hướng về Enemy (10 độ lệch)
            {
                Shoot();
                CurrentTager = null; // Chỉ bắn một lần
            }
        }
    }

    //void Move()
    //{

    //    float moveX = Input.GetAxis("Horizontal");
    //    float moveZ = Input.GetAxis("Vertical");


    //    moveX = joystick.Horizontal;
    //    moveZ = joystick.Vertical;
    //    Vector3 moveDirection = new Vector3(moveX, 0, moveZ);

    //    isMoving = moveDirection.magnitude > 0; // Kiểm tra xem nhân vật có đang di chuyển không

    //    if (moveDirection.magnitude > 0)
    //    {
    //        this.rb.MovePosition(rb.position + moveDirection * speedx * Time.fixedDeltaTime);


    //        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
    //        rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
    //    }
    //}


       protected void Move()
    {
        float moveX = joystick.Horizontal;
        float moveZ = joystick.Vertical;
        Vector3 moveDirection = new Vector3(moveX, 0, moveZ);

        isMoving = moveDirection.magnitude > 0; // Kiểm tra xem nhân vật có đang di chuyển không

        if (isMoving)
        {
            // Di chuyển nhân vật
            rb.MovePosition(rb.position + moveDirection.normalized * speedx * Time.fixedDeltaTime);

            // Xoay nhân vật về hướng di chuyển
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection.normalized);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }




    protected void Shoot()
    {
        GameObject bullet = PoolingManager.Instance.GetGameObject("Bullet");

        if (bullet != null )
        {
            bullet.transform.position = FilePoint.position;
            bullet.transform.rotation = FilePoint.rotation;
            bullet.SetActive(true);

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = FilePoint.forward * bulletSpeed;
        }
    }
   protected void HandleState()
    {
        if (animator == null)
        {
            Debug.LogError("Animator chưa được gán!");
            return;
        }
        if (isMoving)
        {
            animator.ResetTrigger("idle");
            animator.SetTrigger("Run");
        }
        else
        {
            animator.ResetTrigger("Run");
            animator.SetTrigger("idle");

        }
    }

    //void handlesate()
    //{
    //    switch (currentState)
    //    {
    //        case PlayerState.idle:
    //            animator.SetTrigger("idle"); // Trạng thái đứng yên
    //            CheckEnemy();
    //            break;

    //        case PlayerState.Walk:
    //            animator.SetTrigger("Run"); // Trạng thái đi
    //            Move();
    //            break;
    //    }
    //}

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("tron"))
        {
            CurrentTager = other.transform;

        }
    }
   protected virtual void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("tron"))
        {
            CurrentTager = null; // Thoát khỏi Enemy thì xóa mục tiêu
        }
    }

  


}