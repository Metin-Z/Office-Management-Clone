using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoomSave : MonoBehaviour
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