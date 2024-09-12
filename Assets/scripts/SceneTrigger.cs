using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTrigger : MonoBehaviour
{
    public SceneTransitionTrigger stt;
    public LevelLoader levelLoader;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            levelLoader.LoadScene(stt.nextSceneOrCurrentScene);
        }
    }
}
