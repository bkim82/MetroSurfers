using UnityEngine;
using TMPro;

public class PlayerHit : MonoBehaviour
{

    public TextMeshProUGUI scoreText;

    private int score;

    void Start()
    {
        SetScore();
    }
    
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.GetComponent<Obstacle>())
        {
            Debug.Log("HIT - GAME OVER");
            Time.timeScale = 0f;
        }
        if (hit.collider.CompareTag("Coin"))
        {
            hit.collider.gameObject.GetComponent<Collider>().enabled = false;
            Destroy(hit.collider.gameObject);
            score++;
            SetScore();
        }

    }

    void SetScore()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}