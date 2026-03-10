using UnityEngine;

public class EndMenu : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioClip backgroundMusic;

    void Awake()
    {
        if (musicSource != null && backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.Play();
        }
    }
}