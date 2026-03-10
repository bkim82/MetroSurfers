using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerHit : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI pauseScoreText;

    private RunnerController buzzrunner;
    private int score;

    void Start()
    {
        buzzrunner = GetComponent<RunnerController>();
        SetScore();
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // OBSTACLE HIT
        if (hit.collider.GetComponent<Obstacle>())
        {
            Debug.Log("Buzz ran into an obstacle");

            // Trigger slowdown + recovery logic
            buzzrunner.OnHitObstacle();

            // Disable obstacle so it doesn't trigger again
            hit.collider.enabled = false;
        }

        // COIN
        if (hit.collider.CompareTag("Coin"))
        {
            hit.collider.enabled = false;
            Destroy(hit.collider.gameObject);

            score++;
            SetScore();
        }

        // BULLDOG
        if (hit.collider.CompareTag("Bulldog"))
        {
            Debug.Log("Bulldog caught Buzz");

            Time.timeScale = 0f;
            SceneManager.LoadScene("EndMenu");
        }
    }

    void SetScore()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}