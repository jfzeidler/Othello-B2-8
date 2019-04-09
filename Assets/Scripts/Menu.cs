using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Active Scene : " + SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync("_SCENE_");
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("_SCENE_"));
        Debug.Log("Active Scene : " + SceneManager.GetActiveScene().name);

    }
}
