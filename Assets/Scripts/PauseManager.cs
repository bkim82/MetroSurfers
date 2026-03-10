using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;

    public AudioSource musicSource;

    bool isPaused = false;

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            pausePanel.SetActive(true);
            if (musicSource != null && musicSource.isPlaying)
                musicSource.Pause();
        }
        else
        {
            Time.timeScale = 1f;
            pausePanel.SetActive(false);
            if (musicSource != null)
                musicSource.UnPause();
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        // Resume music
        if (musicSource != null)
            musicSource.UnPause();
    }
}