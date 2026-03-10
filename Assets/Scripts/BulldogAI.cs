using UnityEngine;
using UnityEngine.AI;

public class BulldogAI : MonoBehaviour
{
    public Transform buzz;
    private NavMeshAgent nma;
    void Start()
    {
        nma = GetComponent<NavMeshAgent>(); 
    }

    void Update()
    {
        nma.SetDestination(buzz.position);
    }
}
