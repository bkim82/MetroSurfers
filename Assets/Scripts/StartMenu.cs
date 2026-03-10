using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioClip backgroundMusic;

    void Awake()
    {
        if (musicSource != null && backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }
}