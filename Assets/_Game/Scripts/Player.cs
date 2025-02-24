using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 10f;
    private Rigidbody rb;
    [SerializeField] private Joystick joystick;
    public GameObject Bulletprefab;
    public Transform FilePoint;
    public float bulletSpeed=20f;

    private bool isShooting=false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Move();
        
    }
    private void Update()
    {

            Shoot();
        GameObject a = PoolingManager.Instance.GetGameObject("bullet");
        if(a != null )
         {
            a.SetActive(true);
      
        }
    }
    private void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");


        moveX = joystick.Horizontal;
        moveZ = joystick.Vertical;  
        Vector3 moveDirection = new Vector3(moveX, 0, moveZ);
        // Di chuyển nhân vật
        if (moveDirection.magnitude > 0)
        {
            rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);

            // Xoay nhân vật theo hướng di chuyển
            //Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            //rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isShooting == true)
        {
            GameObject bullet = Instantiate(Bulletprefab, FilePoint.position, FilePoint.rotation);
            Debug.Log("Đạn đã được tạo!");
            Rigidbody rigidbody = bullet.GetComponent<Rigidbody>();
            rigidbody.velocity = FilePoint.forward * bulletSpeed;

            Destroy(bullet, 2f);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
                Shoot();
                isShooting = true;
            
           
        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Rời khỏi Enemy, ngừng bắn");
            isShooting = false;
        }
    }
    

}
