using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public void ExitGame()
    {
        Application.Quit();

        #if UNITY_EDITOR //needed to add this part so that it can also quit in the editor
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
