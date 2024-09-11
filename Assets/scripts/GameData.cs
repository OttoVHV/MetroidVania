using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int lastSaveroom;
    public bool dashUnlocked;
    public bool wallWalkUnlocked;

    public GameData(Player player)
    {
        lastSaveroom = player.lastSaveroom;
        dashUnlocked = player.dashUnlocked;
        wallWalkUnlocked = player.wallWalkUnlocked;
    }
}
