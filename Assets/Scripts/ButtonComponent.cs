using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonComponent : MonoBehaviour
{
    #region SerializeFields
    [SerializeField] private int level;
    [SerializeField] private int price;
    [SerializeField] private int xp;

    #endregion

    public int GetLevel()
    {
        return level;
    }
    public int Getprice()
    {
        return price;
    }
    public int GetXP()
    {
        return xp;
    }
}
