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
            StartCoroutine(TeleportPlayer());
        }
    }



    private IEnumerator TeleportPlayer()
    {
        levelLoader.LoadScene(stt.nextSceneOrCurrentScene);
        yield return new WaitForSeconds(0.5f);
        player.Teleport(stt.x, stt.y);
    }
}
