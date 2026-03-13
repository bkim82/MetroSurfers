using UnityEngine;
using UnityEngine.AI;

public class BulldogAI : MonoBehaviour
{
    public Transform buzz;
    private NavMeshAgent nma;
    public enum state
    {
        chase,
        celebrate
    }
    public state currentstate = state.chase;
    private Animation animation;
    void Start()
    {
        nma = GetComponent<NavMeshAgent>(); 
        animation = GetComponentInChildren<Animation>();
        
    }

    void Update()
    {
        switch (currentstate)
        {
            case state.chase:
                nma.SetDestination(buzz.position);
                break;

            case state.celebrate:
                nma.isStopped = true;
                animation.Play("celebratingJump");
                break;
        }
    }
}
