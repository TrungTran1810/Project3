using UnityEngine;
using UnityEngine.AI;

public class EnemyAI :Player
{
    public Transform enemy; 
    private NavMeshAgent agent; 
    public float speedEnemy ; 
    private bool isAttacking=false;
    public float attackDelay = 0.1f;
    protected void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speedEnemy;
        
    }
    //protected override void  FixedUpdate()
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
    //protected void Awake()
    //{
    //    agent = GetComponent<NavMeshAgent>();
    //    agent.speed = speedEnemy;
    //}
    protected override void Update()
    {


        CheckEnemy();
    }


    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("tron") && !isAttacking)
        {
            isAttacking = true;
            agent.isStopped = true;
            Shoot();
            CurrentTager = other.transform;

            // Sau khi bắn, gọi phương thức tiếp tục di chuyển sau attackDelay giây
            Invoke(nameof(ResumeMovement), attackDelay);
        }
    }
    protected override void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("tron"))
        {

            isAttacking = false;
            agent.isStopped = false;
            CurrentTager = null; // Thoát khỏi Enemy thì xóa mục tiêu
        }

        if (other.gameObject.CompareTag("Bullet")) // Nếu bị trúng đạn
        {
            //gameObject.SetActive(false); // Ẩn Enemy thay vì Destroy

        }
    }

    protected void ResumeMovement()
    {
        isAttacking = false;
        agent.isStopped = false;
    }





}
