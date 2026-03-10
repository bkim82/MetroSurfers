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
        SetScore();
        buzzrunner = GetComponent<RunnerController>();
    }
    
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.GetComponent<Obstacle>())
        {
           // Play obstacle hit SFX immediately
            if (sfxSource != null && dogClip != null) // rename dogClip to obstacleClip if you like
                sfxSource.PlayOneShot(dogClip);

            Debug.Log("Buzz ran into an obstacle");
            buzzrunner.forwardSpeed = 1f;
        }
        if (hit.collider.CompareTag("Coin"))
        {
            if (sfxSource != null && coinClip != null)
                sfxSource.PlayOneShot(coinClip);
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