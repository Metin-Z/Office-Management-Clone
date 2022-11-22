using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    #region SerializeFields
    [Header("Objects")]
    [SerializeField] private PlayerController _player;
    [SerializeField] private SliderComponent _slider;
    [Header("Values")]
    [SerializeField] private int mainMoney;
    [SerializeField] private int levelId;
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
    private void Start()
    {
        levelId = PlayerPrefs.GetInt("level");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            IncreaseMoney(50);
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
    public SliderComponent GetSlider()
    {
        return _slider;
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
    public void LevelUp()
    {
        LevelManager.instance.Levels.RemoveAt(0);
        levelId = LevelManager.instance.Levels[0].id;
        _slider.ResetLevelBar();
    }
}