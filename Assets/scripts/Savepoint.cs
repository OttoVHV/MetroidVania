using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Savepoint : MonoBehaviour
{
    public Player player;
    public SceneTransitionTrigger stt;

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Savegame();
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            player.LoadGame();
        }
    }

    public void Savegame()
    {
        player.SetRoom(stt.nextSceneOrCurrentScene);
        SaveSystem.SaveGame(player);
        print("GAME SAVED");
    }
}
