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
    }

    //void Update()
    //{
    //    if (player != null)
    //    {
    //        agent.SetDestination(player.position); // Di chuyển tới vị trí của Player
    //    }
    //}
}
