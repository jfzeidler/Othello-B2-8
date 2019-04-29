using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoardButtons : MonoBehaviour
{
    public void ToMainmenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
