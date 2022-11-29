using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    #region SerializeFields
    [Header("Objects")]
    [SerializeField] private PlayerController _player;
    [SerializeField] private SliderComponent _slider;
    [SerializeField] private NavMeshSurface _navmesh;

    [Header("Values")]
    [SerializeField] private int mainMoney;
    [SerializeField] private int levelId;

    [Header("Lists")]
    [SerializeField] List<DeskComponent> Desks;
    [SerializeField] List<WorkerComponent> Workers;
     public List<Transform> BreakPoints;


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
        levelId = 1;
        DeskControl();
        mainMoney = PlayerPrefs.GetInt("Money");
        if (PlayerPrefs.GetInt("Money") == 0)
        {
            mainMoney = 150;
            PlayerPrefs.SetInt("Money", mainMoney);
            InterfaceManager.instance.UpdateMoney();
        }
        for (int i = 0; i < PlayerPrefs.GetInt("level") - 1; i++)
        {
            LevelManager.instance.Levels.RemoveAt(0);
        }
        for (int i = 0; i < Desks.Count; i++)
        {
            Desks[i].DeskAvailableControl();
        }
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
    public List<DeskComponent> GetDesk()
    {
        return Desks;
    }
    public List<WorkerComponent> GetWorkers()
    {
        return Workers;
    } 
    public NavMeshSurface GetNavmesh()
    {
        return _navmesh;
    }
    public void IncreaseMoney(int money)
    {
        mainMoney += money;
        PlayerPrefs.SetInt("Money", mainMoney);
        InterfaceManager.instance.UpdateMoney();
    }

    public void DecreaseMoney(int money)
    {
        mainMoney -= money;
        PlayerPrefs.SetInt("Money", mainMoney);
        InterfaceManager.instance.UpdateMoney();
    }
    public void LevelUp()
    {
        LevelManager.instance.Levels.RemoveAt(0);
        levelId = LevelManager.instance.Levels[0].id;
        PlayerPrefs.SetInt("level",levelId);
        _slider.ResetLevelBar();
        
        for (int i = 0; i < Desks.Count; i++)
        {
            Desks[i].DeskAvailableControl();
        }
        for (int i = 0; i < Workers.Count; i++)
        {
            Workers[i].LevelControl();
        }
    }
    public void DeskControl()
    {
        for (int i = 0; i < Desks.Count; i++)
        {
            Desks[i].DeskAvailableControl();
        }
    }
}