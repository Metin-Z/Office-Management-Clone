using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class WorkerComponent : MonoBehaviour
{
    #region SerializeFields
    [Header("Objects")]
    [SerializeField] GameObject myDesk;
    [SerializeField] GameObject levelUpEffects;

    [SerializeField] Slider energyBar;
    [SerializeField] Slider workBar;

    [Header("Values")]
    [SerializeField] int myLevel;
    [SerializeField] int startLevel;
    [SerializeField] int jobTime;
    [SerializeField] int energy;
    [SerializeField] int baseJobTime;

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
        if (LevelManager.instance.Levels[0].id == startLevel + 2)
        {
            if (myLevel <3)
            {
                myLevel++;
                Instantiate(levelUpEffects, transform.position, Quaternion.identity);
            }
        }
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
                    Debug.Log("asdsad");
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
