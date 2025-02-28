using UnityEngine;
using UnityEngine.AI;

public class EnemyAI :Player
{
    public Transform player; 
    private NavMeshAgent agent; 
    public float speedEnemy ; 

   protected  void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speedEnemy;
    }

    void Update()
    {
        if (player != null)
        {
            agent.SetDestination(player.position); 
        }
    }



}
