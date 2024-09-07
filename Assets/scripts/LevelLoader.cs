using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    //private int scene;
    private float transitionTime = 0.5f;
    //public Animator transition;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            LoadScene(2);
        }
    }

    public void LoadScene(int level)
    {
        StartCoroutine(LoadLevel(level));
        //SceneManager.GetActiveScene().buildIndex - 1
    }

    private IEnumerator LoadLevel(int levelIndex)
    {
        // play animation
        //transition.SetTrigger("start");

        //wait
        yield return new WaitForSeconds(transitionTime);

        //load scene
        SceneManager.LoadScene(levelIndex);
    }
}
