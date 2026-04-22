using UnityEngine;
using UnityEngine.AI;
using System.Collections;

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

    [Header("Jump Settings")]
    public float obstacleCheckDistance = 2.5f;
    public float baseJumpHeightBuffer = 1.0f;
    public float baseLandingBuffer = 2.0f;
    public float jumpDuration = 0.8f;

    private bool isJumping = false;

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
                if (!isJumping)
                {
                    nma.SetDestination(buzz.position);
                    CheckForObstacle();
                }
                break;

            case state.celebrate:
                nma.isStopped = true;
                animation.Play("celebratingJump");
                break;
        }
    }

    void CheckForObstacle()
    {
        Vector3 origin = transform.position + Vector3.up * 0.5f;

        if (Physics.Raycast(origin, transform.forward, out RaycastHit hit, obstacleCheckDistance))
        {
            if (hit.collider.GetComponent<Obstacle>())
            {
                StartCoroutine(JumpOverObstacle(hit.collider));
            }
        }

        Debug.DrawRay(origin, transform.forward * obstacleCheckDistance, Color.red);
    }

    IEnumerator JumpOverObstacle(Collider obstacleCollider)
    {
        isJumping = true;
        nma.enabled = false;

        Vector3 startPos = transform.position;

        float obstacleHeight = obstacleCollider.bounds.size.y;
        float obstacleDepth = obstacleCollider.bounds.size.z;

        float dynamicJumpHeight = obstacleHeight + baseJumpHeightBuffer;
        float landingDistance = obstacleDepth + baseLandingBuffer;

        Vector3 endPos = startPos + transform.forward * landingDistance;

        float elapsed = 0f;

        while (elapsed < jumpDuration)
        {
            float t = elapsed / jumpDuration;

            float height = Mathf.Sin(t * Mathf.PI) * dynamicJumpHeight;

            Vector3 pos = Vector3.Lerp(startPos, endPos, t);
            pos.y = startPos.y + height;

            transform.position = pos;

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos;

        nma.enabled = true;
        isJumping = false;
    }
}