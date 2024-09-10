using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int currentScene;
    public bool dashUnlocked;
    public bool wallWalkUnlocked;

    public GameData(Player player)
    {
        currentScene = player.currentScene;
        dashUnlocked = player.dashUnlocked;
        wallWalkUnlocked = player.wallWalkUnlocked;
    }
}
