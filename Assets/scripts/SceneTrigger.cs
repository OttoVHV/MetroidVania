using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTrigger : MonoBehaviour
{
    private int triggerLevel = 2;
    public LevelLoader levelLoader;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            levelLoader.LoadScene(triggerLevel);
        }
    }
}
