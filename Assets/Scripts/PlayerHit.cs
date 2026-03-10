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
        SetScore();
        buzzrunner = GetComponent<RunnerController>();
    }
    
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.GetComponent<Obstacle>())
        {
            
            //Debug.Log("HIT - GAME OVER");
            //Time.timeScale = 0f;
            //SceneManager.LoadScene("EndMenu");
            Debug.Log("Buzz ran into an obstacle");
            buzzrunner.forwardSpeed = 1f; //when it bumps into an obstacle, it slows down a lot and the game likely ends
        }
        if (hit.collider.CompareTag("Coin"))
        {
            hit.collider.gameObject.GetComponent<Collider>().enabled = false;
            Destroy(hit.collider.gameObject);
            score++;
            SetScore();
        }
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