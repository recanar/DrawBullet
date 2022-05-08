using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent agent;
    private Vector3 startPos;

    void Awake()
    {
        startPos = transform.position;//take start pos
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }
    private void OnEnable()//Move enemy to start position when respawn
    {
        transform.position = startPos;
        agent.speed += 0.5f;
    }
    private void OnDisable()//Activate enemy after 10 second when killed
    {
        Invoke("ActivateEnemy", 10);
    }
    private void Update()//while playing follow player
    {
        if (GameManager.instance.currentState != States.TapToStart)
        {
            FollowPlayer();
        }
    }
    public void FollowPlayer()
    {
        agent.SetDestination(player.position);
    }
    private void ActivateEnemy()
    {
        gameObject.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.currentState = States.Lose;
        }
    }
}