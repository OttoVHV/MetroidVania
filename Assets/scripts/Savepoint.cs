using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Savepoint : MonoBehaviour
{
    public Player player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            print("Press J to save");

            if (Input.GetKeyDown(KeyCode.B))
            {
                player.Savegame();
                print("will it save?");
            }
        }
    }
}
