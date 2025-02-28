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
    public Transform FilePoint;
    public float bulletSpeed = 20f;
    public Coroutine ShootingCorotine;
    public bool isMoving=false;
    //protected virtual void Start()
    // {
    //     rb = GetComponent<Rigidbody>();

    // }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (joystick == null)
        {
            joystick = FindObjectOfType<Joystick>();
        }
    }
    void Start()
    {
       
    }

    protected void FixedUpdate()
    {
        Move();

    }
    private void Update()
    {

    }
    void Move()
    {

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");


        moveX = joystick.Horizontal;
        moveZ = joystick.Vertical;
        Vector3 moveDirection = new Vector3(moveX, 0, moveZ);

        isMoving = moveDirection.magnitude > 0; // Kiểm tra xem nhân vật có đang di chuyển không
       
        if (moveDirection.magnitude > 0)
        {
            this.rb.MovePosition(rb.position + moveDirection * speedx * Time.fixedDeltaTime);

         
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }

    IEnumerator shootwithdeley()
    {
        while (!isMoving) // Chỉ bắn khi không di chuyển
        {
            Shoot();
            yield return new WaitForSeconds(2f);
        }
        ShootingCorotine = null; // Dừng coroutine khi nhân vật bắt đầu di chuyển
    }
    //private void Shoot()
    //{


    //    GameObject bullet = Instantiate(Bulletprefab, FilePoint.position, FilePoint.rotation);
    //    Debug.Log("Đạn đã được tạo!");
    //    Rigidbody rigidbody = bullet.GetComponent<Rigidbody>();
    //    rigidbody.velocity = FilePoint.forward * bulletSpeed;

    //}
    protected void Shoot()
    {
        GameObject bullet = PoolingManager.Instance.GetGameObject("Bullet");
        if (bullet != null)
        {
            bullet.transform.position = FilePoint.position;
            bullet.transform.rotation = FilePoint.rotation;
            bullet.SetActive(true);

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = FilePoint.forward * bulletSpeed;
        }
    }


     void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && ShootingCorotine == null)
        {
            ShootingCorotine = StartCoroutine(shootwithdeley()); // Bắt đầu bắn 
        }
    }
     void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Rời khỏi Enemy, ngừng bắn");

            if (ShootingCorotine != null)
            {
                StopCoroutine(ShootingCorotine);
                ShootingCorotine = null;
            }
        }

       
    }


}
