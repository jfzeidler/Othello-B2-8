using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;

public class MainMenu : MonoBehaviour
{

    public GameObject CanvasMenu;
    public GameObject Main_Camera;
    public GameObject Rotation_Camera;
    public GameObject UI;

    void WaitForSceneManager()
    {
        float temptime = Time.time + 5.0f; float time = Time.time;

        Debug.Log(temptime + "   " + time);

        while (temptime > time)
        {
            Debug.Log("WAIT");
        }
        CanvasMenu.SetActive(false);
        Main_Camera.SetActive(true);
        Rotation_Camera.SetActive(false);
        UI.SetActive(true);
        Debug.Log("TESTING");
    }

    public void Update()
    {

    }

    public void MenuButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PlayButton()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        WaitForSceneManager();
    }

    public void PlayAgain()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        WaitForSceneManager();
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("EXIT");
    }
}
