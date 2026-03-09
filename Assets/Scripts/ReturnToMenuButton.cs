using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenuButton : MonoBehaviour
{
    public void GoToMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
}