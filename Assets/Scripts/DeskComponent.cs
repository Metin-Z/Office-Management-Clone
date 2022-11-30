using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.IO;
public class DeskComponent : MonoBehaviour
{

    [Header("Base Panel")]
    public GameObject buyPanel;
    [SerializeField] private string dataKey;
    [SerializeField] private DeskSave desk;
    #region SerializeFields
    [Header("Meshs")]
    [SerializeField] private Mesh desk0;
    [SerializeField] private Mesh desk1;
    [SerializeField] private Mesh desk2;

    [SerializeField] private MeshFilter meshfilt;
    [Header("Available Level")]
    [SerializeField] private int avilableLevel;

    #endregion
    #region Worker
    [Header("Worker Objects")]
    [SerializeField] private GameObject buyWorker;

    [SerializeField] private GameObject worker0Available;
    [SerializeField] private GameObject worker1Available;
    [SerializeField] private GameObject worker2Available;

    public int workerLevel;

    [SerializeField] private GameObject bars;

    public Slider workBar;
    public Slider energyBar;
    public GameObject breakButton;

    [SerializeField] private GameObject workerSpawnPos;

    [SerializeField] private GameObject worker0;
    [SerializeField] private GameObject worker1;
    [SerializeField] private GameObject worker2;

    [SerializeField] private GameObject activeWorker;
    #endregion
    #region Desk
    [Header("Desk Objects")]
    [SerializeField] private int deskLevel;

    [SerializeField] private bool alredySpawned;
    [SerializeField] private GameObject buyDesk;

    [SerializeField] private GameObject desk1Available;
    [SerializeField] private GameObject desk1NotAvailable;

    [SerializeField] private GameObject desk2Available;
    [SerializeField] private GameObject desk2NotAvailable;

    [SerializeField] private GameObject chair1;
    [SerializeField] private GameObject chair2;
    #endregion
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            buyPanel.SetActive(true);
        }
    }
    private void Start()
    {
        Scale();
        JsonLoad();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            buyPanel.SetActive(false);
        }
    }
    public string GetDataKey()
    {
        return dataKey;
    }
    public int GetDeskLevel()
    {
        return deskLevel;
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
        //JsonSave(dataKey, deskLevel, workerComp.GetMyBaseLevel(), workerLevel);
        GoSave();
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
        //JsonSave(dataKey, deskLevel, workerLevel, workerLevel);
        GoSave();
        GameManager.instance.GetNavmesh().BuildNavMesh();
    }
    public void GoSave()
    {
        if (activeWorker !=null)
        {
            activeWorker.TryGetComponent(out WorkerComponent workerComp);
            JsonSave(dataKey, deskLevel, workerComp.GetMyBaseLevel(), workerLevel);
        }
        if (activeWorker == null)
        {
            JsonSave(dataKey, deskLevel, 0, workerLevel);
        }
    }
    public void DeskAvailableControl()
    {
        if (LevelManager.instance.Levels[0].id >= avilableLevel && alredySpawned == false)
        {
            transform.TryGetComponent(out BoxCollider collider);
            collider.enabled = true;
        }
    }
    public void ClosePanels()
    {
        worker0Available.SetActive(false);
        worker1Available.SetActive(false);
        worker2Available.SetActive(false);
    }
    public void Scale()
    {
        gameObject.SetActive(true);
        transform.DOScale(new Vector3(0.25f, 0.25f, 0.25f), 1).OnComplete(() =>
        transform.DOScale(new Vector3(1.25f, 1.25f, 1.25f), 1)
        ).SetEase(Ease.Linear);
        GameManager.instance.GetNavmesh().BuildNavMesh();
    }
    public void GoBreak()
    {
        activeWorker.TryGetComponent(out WorkerComponent workerComp);
        if (workerComp.GetBreak() == false)
        {
            workerComp.SetBreakCoroutine();
        }
    }
    public void JsonSave(string dataKey, int deskLevel, int workerStartLevel, int workerEndLevel)
    {
        desk = new DeskSave()
        {
            dataKey = dataKey,
            deskLevel = deskLevel,
            workerStartLevel = workerStartLevel,
            workerLastLevel = workerEndLevel,
        };

        string jsonString = JsonUtility.ToJson(desk);
        File.WriteAllText(Application.dataPath + "/Saves/" + dataKey + ".json", jsonString);
    }
    public void JsonLoad()
    {
        string path = Application.dataPath + "/Saves/" + dataKey + ".json";
        if (File.Exists(path))
        {
            string jsonUpload = File.ReadAllText(path);
            DeskSave deskSave = JsonUtility.FromJson<DeskSave>(jsonUpload);
            if (deskSave.deskLevel == 1)
            {
                meshfilt.mesh = desk1;
                desk1Available.SetActive(false);
                desk1NotAvailable.SetActive(true);
                chair1.SetActive(true);
                chair2.SetActive(false);
            }
            if (deskSave.deskLevel == 2)
            {
                meshfilt.mesh = desk2;
                desk1Available.SetActive(false);
                desk1NotAvailable.SetActive(true);
                desk2Available.SetActive(false);
                desk2NotAvailable.SetActive(true);
                chair2.SetActive(true);
            }
            
            if (deskSave.workerStartLevel == 1)
            {
                activeWorker = Instantiate(worker0, workerSpawnPos.transform);
                WorkerGetDesk(activeWorker, deskSave);
            }
            if (deskSave.workerStartLevel == 2)
            {
                activeWorker = Instantiate(worker1, workerSpawnPos.transform);
                WorkerGetDesk(activeWorker, deskSave);
            }
            if (deskSave.workerStartLevel == 3)
            {
                activeWorker = Instantiate(worker2, workerSpawnPos.transform);
                WorkerGetDesk(activeWorker, deskSave);
            }
            
        }    
    }
    public void WorkerGetDesk(GameObject activeWorker, DeskSave deskSave)
    {
        activeWorker.TryGetComponent(out WorkerComponent workerComp);
        bars.SetActive(true);
        ClosePanels();
        workerComp.GetMyDesk(transform.gameObject);
        workerComp.ReloadLevel(deskSave.workerLastLevel);
    }
}