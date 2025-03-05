using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class EnemyAI :Player
{
    public Transform enemy; 
    private NavMeshAgent agent; 
    public float speedEnemy ; 
    
    public float attackDelay = 0.1f;
    public bool ismoving=false;
    private Animator ani;
    protected void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speedEnemy;
        ani = GetComponent<Animator>();
    }
  
    //protected override void FixedUpdate()
    //{
    //    if (enemy != null)
    //    {

    //        agent.SetDestination(enemy.position);
    //        Shoot();
    //        HandleState();
    //        CheckEnemy();
    //    }
    //    //Shoot();
    //}
    
    protected override void Update()
    {
        if (enemy != null)
        {

            agent.SetDestination(enemy.position);
            
            // Shoot();
            changeState();
            //CheckEnemy();
        }


    }

    void changeState()
    {
       
            // Cập nhật ismoving theo tốc độ của agent
            ismoving = agent.velocity.magnitude > 0.1f;

            if (ismoving)
            {
                ani.ResetTrigger("idle");
                ani.SetTrigger("Run");
            }
            else
            {
                ani.ResetTrigger("Run");
                ani.SetTrigger("idle");
            }
        

    }
    //protected override void HandleState()
    //{
    //    isMoving = agent.velocity.magnitude > 0.1f;

    //}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("tron"))
        {
           
            agent.isStopped = true;
            Shoot();
            CurrentTager = other.transform;

            // Sau khi bắn, gọi phương thức tiếp tục di chuyển sau attackDelay giây
            Invoke(nameof(ResumeMovement), attackDelay);
        }
       
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("tron"))
        {

            agent.isStopped = false;
            CurrentTager = null; // Thoát khỏi Enemy thì xóa mục tiêu
        }

        //if (other.gameObject.CompareTag("Player")) // Nếu bị trúng đạn
        //{
        //    gameObject.SetActive(false); // Ẩn Enemy thay vì Destroy

        //}
    }

    private void ResumeMovement()
    {
       
        agent.isStopped = false;
    }





}
