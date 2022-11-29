using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;
using UnityEngine.UI;
public class WorkerComponent : MonoBehaviour
{
    #region SerializeFields
    [Header("Objects")]
    [SerializeField] GameObject myDesk;
    [SerializeField] GameObject levelUpEffects;
    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] Animator anim;

    [SerializeField] Slider energyBar;
    [SerializeField] Slider workBar;

    [Header("Values")]
    [SerializeField] int myLevel;
    [SerializeField] int myBaseLevel;
    [SerializeField] int startLevel;
    [SerializeField] int jobTime;
    [SerializeField] int energy;
    [SerializeField] int baseJobTime;
    [SerializeField] bool inBreak;

    #endregion
    private void Start()
    {
        myDesk.TryGetComponent(out DeskComponent desk);
        startLevel = LevelManager.instance.Levels[0].id;
        energyBar = desk.energyBar;
        workBar = desk.workBar;
        baseJobTime = jobTime;
        workBar.maxValue = jobTime;
        workBar.value = jobTime;
        StartCoroutine(Work());
        inBreak = false;
        BreakControl();
    }
    public void BreakControl()
    {
        if (inBreak == false)
        {
            anim.SetBool("Work", true);
        }
        if (inBreak == true)
        {
            anim.SetBool("Work", false);
        }
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
    public int GetMyBaseLevel()
    {
        return myBaseLevel;
    }
    public bool GetBreak()
    {
        return inBreak;
    }
    public void StartBreak()
    {
        Vector3 target = GameManager.instance.BreakPoints[0].transform.position;
        inBreak = true;
        BreakControl();
        navMeshAgent.enabled = true;
        navMeshAgent.SetDestination(target);
    }
    public void LevelControl()
    {
        if (LevelManager.instance.Levels[0].id == startLevel + 2)
        {
            if (myLevel <3)
            {
                myLevel++;
                startLevel = LevelManager.instance.Levels[0].id;
                SaveWorker();
                Instantiate(levelUpEffects, transform.position, Quaternion.identity);
            }
        }
    }
    public void SaveWorker()
    {
        myDesk.TryGetComponent(out DeskComponent desk);
        desk.JsonSave(desk.GetDataKey(), desk.GetDeskLevel(), myBaseLevel, myLevel);
    }

    public void ReloadLevel(int myLevelReload)
    {
        myLevel = myLevelReload;
    }
    public IEnumerator Work()
    {
        while (energy > 0)
        {
            energy--;
            energyBar.value = energy;

            if (jobTime > 0)
            {
                jobTime -= myLevel;
                workBar.value = jobTime;

                if (0 >= jobTime)
                {
                    StopCoroutine(Work());
                    WorkCompleted();
                }
                yield return new WaitForSeconds(1);
            }
        }
    }
    public void WorkCompleted()
    {
        jobTime = baseJobTime;

        GameManager.instance.IncreaseMoney(30);
        GameManager.instance.GetSlider().LevelBarUpdate(15);

    }
}
