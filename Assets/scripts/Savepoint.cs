using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Savepoint : MonoBehaviour
{
    public Player player;
    private static int room = 3;

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ShowButton();
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            player.LoadGame();
        }
    }

    public void ShowButton()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Savegame();
        }
    }

    public void Savegame()
    {
        player.SetRoom(room);
        SaveSystem.SaveGame(player);
        print("GAME SAVED");
    }
}
