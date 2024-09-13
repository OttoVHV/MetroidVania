using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTrigger : MonoBehaviour
{
    public SceneTransitionTrigger stt;
    public LevelLoader levelLoader;
    [SerializeField] private Player player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            levelLoader.LoadScene(stt.nextSceneOrCurrentScene);
        }
    }

    private void Start()
    {
        /*if ()
        {
            player.Teleport(stt.x, stt.y);
        }*/
    }
}
