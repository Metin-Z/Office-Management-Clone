using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonComponent : MonoBehaviour
{
    #region SerializeFields
    [Header("Values")]

    [SerializeField] private int level;
    [SerializeField] private int price;
    [SerializeField] private int xp;

    [Header("Workers")]
    [SerializeField] private GameObject worker;
    [SerializeField] private GameObject worker1;
    [SerializeField] private GameObject worker2;


    [Header("Meshs")]
    [SerializeField] private Mesh mesh1;
    [SerializeField] private Mesh mesh2;



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
    public GameObject GetWorker()
    {
        return worker;
    }
}
