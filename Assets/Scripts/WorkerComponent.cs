using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;
using UnityEngine.UI;
using DG.Tweening;
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
    [SerializeField] int breakTime = 10;
    [SerializeField] Vector3 spawnPos;
    [SerializeField] private GameObject breakObject;
    [SerializeField] private GameObject energyIcon;

    #endregion
    private void Start()
    {
        myDesk.TryGetComponent(out DeskComponent desk);
        startLevel = LevelManager.instance.Levels[0].id;
        if (myLevel == 0)
        {
            myLevel = 1;
        }
        energyBar = desk.energyBar;
        workBar = desk.workBar;
        baseJobTime = jobTime;
        workBar.maxValue = jobTime;
        workBar.value = jobTime;
        StartCoroutine(Work());
        inBreak = false;
        BreakControl();
        spawnPos = transform.position;
    }
    public void BreakControl()
    {
        if (inBreak == false)
        {
            energy = 180;
            anim.SetBool("Work", true);
            transform.LookAt(myDesk.transform);
            StartCoroutine(Work());
        }
        if (inBreak == true)
        {
            energy = 0;
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
    public void GetBreakObject(GameObject breakObj)
    {
        breakObject = breakObj;
    }
    public int GetMyBaseLevel()
    {
        return myBaseLevel;
    }
    public bool GetBreak()
    {
        return inBreak;
    }
    public void SetBreakCoroutine()
    {
        StartCoroutine(StartBreak());
        navMeshAgent.enabled = true;
    }
    public IEnumerator StartBreak()
    {
        inBreak = true;
        if (energy != 1)
        {
            energyIcon.SetActive(false);
        }
        breakTime = 10;
        Vector3 target = GameManager.instance.BreakPoints[0].transform.position;
        while (inBreak == true)
        {
            yield return new WaitForFixedUpdate();
            BreakControl();
            navMeshAgent.SetDestination(target);

            target.y = transform.position.y;

            if (Vector3.Distance(transform.position, target) < 0.2f)
            {
                navMeshAgent.enabled = false;
                while (breakTime > 0)
                {
                    yield return new WaitForSeconds(1);
                    transform.LookAt(breakObject.transform);
                    breakTime--;
                }
            }
            if (breakTime == 0)
            {
                navMeshAgent.enabled = true;
                navMeshAgent.SetDestination(spawnPos);
                spawnPos.y = transform.position.y;
                if (Vector3.Distance(transform.position, spawnPos) < 0.3f)
                {
                    inBreak = false;
                    transform.position = spawnPos;
                    BreakControl();
                    navMeshAgent.enabled = false;
                    StopCoroutine(StartBreak());
                }
            }

        }
    }
    public void LevelControl()
    {
        if (LevelManager.instance.Levels[0].id == startLevel + 2)
        {
            if (myLevel < 3)
            {
                myLevel++;
                myDesk.TryGetComponent(out DeskComponent desk);
                desk.workerLevel = myLevel;
                startLevel = LevelManager.instance.Levels[0].id;
                SaveWorker();
                Instantiate(levelUpEffects, transform.position, Quaternion.identity);
            }
        }
    }
    public void SaveWorker()
    {
        myDesk.TryGetComponent(out DeskComponent desk);
        //desk.JsonSave(desk.GetDataKey(), desk.GetDeskLevel(), myBaseLevel, myLevel);
        desk.GoSave();
    }

    public void ReloadLevel(int myLevelReload)
    {
        myLevel = myLevelReload;
    }
    public IEnumerator Work()
    {
        while (energy > 0)
        {
            if (energy <= 1)
            {
                energyIcon.SetActive(true);
            }
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
    public void GetFired()
    {
        StartCoroutine(ImFired());
    }
    public IEnumerator ImFired()
    {
        bool fired;
        fired = true;
        while (fired == true)
        {
            yield return new WaitForFixedUpdate();
            Vector3 target = LevelManager.instance.GetFirePos().position;
            navMeshAgent.enabled = true;
            navMeshAgent.SetDestination(LevelManager.instance.GetFirePos().position);
            target.y = transform.position.y;
            anim.SetBool("Work", false);
            GameManager.instance.GetWorkers().Remove(this);
            if (Vector3.Distance(transform.position, target) < 0.6f)
            {
                gameObject.SetActive(true);
                transform.DOScale(new Vector3(0.25f, 0.25f, 0.25f), 1).OnComplete(() =>
                Destroy(transform.gameObject)
                ).SetEase(Ease.Linear);
            }
        }
    }
}
