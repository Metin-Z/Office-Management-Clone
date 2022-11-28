using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskSave
{
    public string dataKey;
    public int deskLevel;
    public int workerStartLevel;
    public int workerLastLevel;
    public DeskSave()
    {

    }
    public DeskSave(string dataKey, int deskLevel,int workerStartLevel,int workerLastLevel)
    {
        this.dataKey = dataKey;
        this.deskLevel = deskLevel;
        this.workerStartLevel = workerStartLevel;
        this.workerLastLevel = workerLastLevel;
    }
}
