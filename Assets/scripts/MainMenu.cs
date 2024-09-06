using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void NewGame()
    {
        SceneManager.LoadScene("inicio");
    }

    /*public void LoadGame()
    {

    }*/

    public void QuitGame()
    {
        Application.Quit();
    }
}
