using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerHit : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI pauseScoreText;
    public GameObject coinWall;

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

    void SetScore()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        CoinGate gate = hit.collider.GetComponent<CoinGate>();

        if (gate != null)
        {
            if (score >= gate.cost)
            {
                score -= gate.cost;
                SetScore();
                Destroy(hit.collider.gameObject);
            }

            return;
        }

        if (hit.collider.GetComponent<Obstacle>())
        {
           // Play obstacle hit SFX immediately
            if (sfxSource != null && dogClip != null) // rename dogClip to obstacleClip if you like
                sfxSource.PlayOneShot(dogClip);

            Debug.Log("Buzz ran into an obstacle");
            buzzrunner.forwardSpeed = 1f;
            buzzrunner.OnHitObstacle();
        }

        if (hit.collider.CompareTag("Coin"))
        {
            if (sfxSource != null && coinClip != null)
                sfxSource.PlayOneShot(coinClip);
            hit.collider.gameObject.GetComponent<Collider>().enabled = false;
            Destroy(hit.collider.gameObject);
            GameObject coin = hit.collider.gameObject;

            if (!coin.activeSelf)
                return;

            coin.SetActive(false);

            score++;
            SetScore();

            Destroy(coin);
        }

        if (hit.collider.CompareTag("Bulldog"))
        {
            Debug.Log("Bulldog caught Buzz");
            Time.timeScale = 0f;
            SceneManager.LoadScene("EndMenu");
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        CoinGate gate = hit.collider.GetComponent<CoinGate>();

        if (gate != null)
        {
            if (score >= gate.cost)
            {
                score -= gate.cost;
                SetScore();
                Destroy(hit.collider.gameObject);
            }

            return;
        }

        if (hit.collider.GetComponent<Obstacle>())
        {
            Debug.Log("Buzz ran into an obstacle");
            buzzrunner.OnHitObstacle();
        }

        if (hit.collider.CompareTag("Coin"))
        {
            GameObject coin = hit.collider.gameObject;

            if (!coin.activeSelf)
                return;

            coin.SetActive(false);

            score++;
            SetScore();

            Destroy(coin);
        }

        if (hit.collider.CompareTag("Bulldog"))
        {
            Debug.Log("Bulldog caught Buzz");
            Time.timeScale = 0f;
            SceneManager.LoadScene("EndMenu");
        }
    }
}