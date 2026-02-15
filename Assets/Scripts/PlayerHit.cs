using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.GetComponent<Obstacle>())
        {
            Debug.Log("HIT - GAME OVER");
            Time.timeScale = 0f;
        }
    }
}