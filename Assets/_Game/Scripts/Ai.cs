using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;  // Gán Player vào đây
    private NavMeshAgent agent;
    [SerializeField] private float speed;

    void Start()
     {
        agent = GetComponent<NavMeshAgent>(); // Lấy NavMeshAgent
        agent.SetDestination(player.position);
        agent.speed = speed;
    }

    void Update()
    {
        Debug.Log(agent.destination);

        if (player != null)
        {
            agent.SetDestination(player.position); // Di chuyển tới vị trí của Player
        }
    }
}
