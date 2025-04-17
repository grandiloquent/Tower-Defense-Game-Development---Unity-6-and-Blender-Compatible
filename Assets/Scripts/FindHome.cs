using UnityEngine;

public class FindHome : MonoBehaviour
{
    public Transform destination;
    UnityEngine.AI.NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.SetDestination(destination.position);
    }
    void Update()
    {
        if (agent.remainingDistance < 0.5f && agent.hasPath)
        {
            LevelManager.RemoveEnemy();
            Destroy(gameObject, .1f);
        }

    }
    void FixedUpdate()
    {

    }
}