using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public void GoToScene(string scenename) {
        SceneManager.LoadScene(scenename);
    }

    public void QuitApp ()
    {
        Application.Quit();
        Debug.Log("Application has Quit");
    }
}
