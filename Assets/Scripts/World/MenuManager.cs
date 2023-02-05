using UnityEngine;

public class MenuManager : MonoBehaviour
{


    public void CloseApp()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void PauseGame(bool state) //true to pause and false to unpause
    {
        Time.timeScale = state ? 0 : 1;
    }
}
