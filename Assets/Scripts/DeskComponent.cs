using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DeskComponent : MonoBehaviour
{
    [Header("Base Panel")]
    public GameObject buyPanel;
    #region SerializeFields
    [Header("Meshs")]
    [SerializeField] private Mesh desk0;
    [SerializeField] private Mesh desk1;
    [SerializeField] private Mesh desk2;

    [SerializeField] private MeshFilter meshfilt;
    [Header("Available Level")]
    [SerializeField] private int avilableLevel;
    #endregion

    [Header("Worker Objects")]
    public GameObject buyWorker;

    public GameObject worker0Available;
    public GameObject worker1Available;
    public GameObject worker2Available;

    public int workerLevel;

    public GameObject bars;

    public GameObject workerSpawnPos;

    public GameObject worker0;
    public GameObject worker1;
    public GameObject worker2;

    public GameObject activeWorker;

    [Header("Desk Objects")]
    public int deskLevel;

    public bool alredySpawned;
    public GameObject buyDesk;

    public GameObject desk1Available;
    public GameObject desk1NotAvailable;

    public GameObject desk2Available;
    public GameObject desk2NotAvailable;

    public GameObject chair1;
    public GameObject chair2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            buyPanel.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            buyPanel.SetActive(false);
        }
    }
    public void OpenWorkerPanel()
    {
        buyWorker.SetActive(true);
    }
    public void CloseWorkerPanel()
    {
        buyWorker.SetActive(false);
    }
    public void UpgradeWorker(GameObject buttonComp)
    {
        buttonComp.TryGetComponent(out ButtonComponent button);
        if (GameManager.instance.GetMoney() >= button.Getprice())
        {
            workerLevel = button.GetLevel();
            GameManager.instance.GetSlider().LevelBarUpdate(button.GetXP());
            workerLevelControl(buttonComp);
        }
    }
    public void OpenDeskPanel()
    {
        buyDesk.SetActive(true);
    }
    public void CloseDeskPanel()
    {
        buyDesk.SetActive(false);
    }
    public void UpgradeDesk(GameObject buttonComp)
    {
        buttonComp.TryGetComponent(out ButtonComponent button);
        if (GameManager.instance.GetMoney() >= button.Getprice())
        {
            deskLevel = button.GetLevel();
            GameManager.instance.GetSlider().LevelBarUpdate(button.GetXP());
            StartCoroutine(DeskSpawnTimer());
            deskLevelControl();
        }
    }

    public void workerLevelControl(GameObject buttonComp)
    {
        buttonComp.TryGetComponent(out ButtonComponent button);

        activeWorker = Instantiate(button.GetWorker(), workerSpawnPos.transform);
        GameManager.instance.DecreaseMoney(button.Getprice());
        ClosePanels();
        bars.SetActive(true);

        activeWorker.TryGetComponent(out WorkerComponent workerComp);
        workerComp.GetMyDesk(gameObject);
    }
    public void deskLevelControl()
    {
        if (deskLevel == 1)
        {
            meshfilt.mesh = desk1;
            GameManager.instance.DecreaseMoney(25);
            desk1Available.SetActive(false);
            desk1NotAvailable.SetActive(true);
            chair1.SetActive(true);
            chair2.SetActive(false);
        }
        if (deskLevel == 2)
        {
            meshfilt.mesh = desk2;
            GameManager.instance.DecreaseMoney(75);
            desk2Available.SetActive(false);
            desk2NotAvailable.SetActive(true);
            chair2.SetActive(true);
            chair1.SetActive(false);
        }
    }

    public void DeskAvailableControl()
    {
        if (LevelManager.instance.Levels[0].id >= avilableLevel && alredySpawned == false)
        {
            gameObject.SetActive(true);
            transform.DOScale(new Vector3(0.25f, 0.25f, 0.25f), 1).OnComplete(() =>
            transform.DOScale(new Vector3(1.25f, 1.25f, 1.25f), 1)
            ).SetEase(Ease.Linear);
            //GameManager.instance.GetDesk().Remove(this);
            GameManager.instance.GetNavmesh().BuildNavMesh();
        }
    }
    public void ClosePanels()
    {
        worker0Available.SetActive(false);
        worker1Available.SetActive(false);
        worker2Available.SetActive(false);
    }
    public IEnumerator DeskSpawnTimer()
    {
        yield return new WaitForSeconds(2.5f);
        alredySpawned = true;
    }
}

