using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class WorkerComponent : MonoBehaviour
{
    #region SerializeFields
    [Header("Objects")]
    [SerializeField] GameObject myDesk;
    [SerializeField] GameObject levelUpEffects;

    [Header("Values")]
    [SerializeField] int myLevel;
    [SerializeField] int startLevel;

    #endregion
    private void Start()
    {
        startLevel = LevelManager.instance.Levels[0].id;
    }
    public void GetMyDesk(GameObject desk)
    {
        myDesk = desk;
        GetMyLevel();
        GameManager.instance.GetWorkers().Add(this);
    }
    public void GetMyLevel()
    {
        myDesk.TryGetComponent(out DeskComponent desk);
        myLevel = desk.workerLevel;
    }
    public void LevelControl()
    {
        if (LevelManager.instance.Levels[0].id == startLevel+2)
        {
            myLevel++;
            Instantiate(levelUpEffects,transform.position,Quaternion.identity);
        }
    }
}
