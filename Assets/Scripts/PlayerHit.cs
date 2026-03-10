using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerHit : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI pauseScoreText;

    private RunnerController buzzrunner;
    private int score;

    [Header("Audio")]
    public AudioSource sfxSource;        // Assign in Inspector
    public AudioClip coinClip;
    public AudioClip dogClip;

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
           // Play obstacle hit SFX immediately
            if (sfxSource != null && dogClip != null)
                sfxSource.PlayOneShot(dogClip);

            Debug.Log("Buzz ran into an obstacle");

            // Trigger slowdown + recovery logic
            buzzrunner.OnHitObstacle();

            // Disable obstacle so it doesn't trigger again
            hit.collider.enabled = false;
        }

        // COIN
        if (hit.collider.CompareTag("Coin"))
        {
            if (sfxSource != null && coinClip != null)
                sfxSource.PlayOneShot(coinClip);
            hit.collider.gameObject.GetComponent<Collider>().enabled = false;
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