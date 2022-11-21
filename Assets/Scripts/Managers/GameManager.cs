using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    #region SerializeFields
    [SerializeField] private PlayerController _player;
    [SerializeField] private int mainMoney;
    #endregion

    public virtual void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            IncreaseMoney(500);
        }
    }

    public PlayerController GetPlayerController()
    {
        return _player;
    }

    public int GetMoney()
    {
        return mainMoney;
    }

    public void IncreaseMoney(int money)
    {
        mainMoney += money;
        InterfaceManager.instance.UpdateMoney();
    }

    public void DecreaseMoney(int money)
    {
        mainMoney -= money;
        InterfaceManager.instance.UpdateMoney();
    }
}
