using UnityEngine;
using UnityEngine.AI;

public class EnemyFollower : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Transform player;
    private NavMeshAgent agent;
    private Animator animator;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            Debug.Log("EnemyFollower: Found player at position " + player.position);
        }
        else
        {
            Debug.LogError("EnemyFollower: Player not found! Make sure the Player GameObject has the 'Player' tag.");
        }

        agent = GetComponent<NavMeshAgent>();  
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player != null)
        {
            agent.SetDestination(player.position);
            animator.SetBool("IsRunning", true);

        }
    }
}
