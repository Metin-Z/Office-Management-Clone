using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameRoomSave
{
    public string dataKey;
    public int deskLevel;

    public GameRoomSave()
    {

    }
    public GameRoomSave(string dataKey, int deskLevel)
    {
        this.dataKey = dataKey;
        this.deskLevel = deskLevel;
    }
}