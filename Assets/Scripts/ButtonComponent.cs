using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonComponent : MonoBehaviour
{
    #region SerializeFields
    [SerializeField] private int level;
    [SerializeField] private int price;

    #endregion

    public int GetLevel()
    {
        return level;
    }
    public int Getprice()
    {
        return price;
    }
}
