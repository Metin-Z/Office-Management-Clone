using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.IO;
using UnityEngine.AI;


public class GameRoomComponent : MonoBehaviour
{
    #region SerializeFields

    [Header("Main Objects")]

    [SerializeField] private Mesh mesh1;
    [SerializeField] private Mesh mesh2;
    [SerializeField] private MeshFilter meshfilt;
    [SerializeField] private NavMeshObstacle navMeshObstacle;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material mat;
    [SerializeField] private GameObject breakPoint;
    [SerializeField] private GameObject breakPoint2;


    [SerializeField] private string dataKey;
    [SerializeField] private int deskLevel;
    [SerializeField] private GameRoomSave desk;
    [SerializeField] private bool meetingTable;


    [Header("Panel Objects")]

    [SerializeField] private GameObject buyPanel;
    [SerializeField] private GameObject gamePanel;

    [SerializeField] private GameObject desk1Available;
    [SerializeField] private GameObject desk1NotAvailable;

    [SerializeField] private GameObject desk2Available;
    [SerializeField] private GameObject desk2NotAvailable;

    #endregion
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
    private void Start()
    {
        Scale();
        JsonLoad();
    }
    public void OpenArcadePanel()
    {
        gamePanel.SetActive(true);
    }
    public void CloseArcadePanel()
    {
        gamePanel.SetActive(false);
    }
    public void Scale()
    {
        if (meetingTable == false)
        {
            gameObject.SetActive(true);
            transform.DOScale(new Vector3(0.25f, 0.25f, 0.25f), 1).OnComplete(() =>
            transform.DOScale(new Vector3(1.25f, 1.25f, 1.25f), 1)
            ).SetEase(Ease.Linear);
            GameManager.instance.GetNavmesh().BuildNavMesh();
        }
        if (meetingTable == true)
        {
            gameObject.SetActive(true);
            transform.DOScale(new Vector3(0.25f, 0.25f, 0.25f), 1).OnComplete(() =>
            transform.DOScale(new Vector3(1.5f, 0.75f, 1.5f), 1)
            ).SetEase(Ease.Linear);
            GameManager.instance.GetNavmesh().BuildNavMesh();
        }

    }
    public void Upgrade1(GameObject buttonComp)
    {
        buttonComp.TryGetComponent(out ButtonComponent button);
        if (GameManager.instance.GetMoney() >= button.Getprice())
        {
            deskLevel = button.GetLevel();
            GameManager.instance.GetSlider().LevelBarUpdate(button.GetXP());
            UpgradeControl(buttonComp);
        }
    }
    public void UpgradeControl(GameObject buttonComp)
    {
        buttonComp.TryGetComponent(out ButtonComponent button);
        if (deskLevel == 1)
        {
            meshfilt.mesh = mesh1;
            GameManager.instance.DecreaseMoney(button.Getprice());
            desk1Available.SetActive(false);
            desk1NotAvailable.SetActive(true);
        }
        if (deskLevel == 2)
        {
            meshfilt.mesh = mesh2;
            GameManager.instance.DecreaseMoney(button.Getprice());
            desk2Available.SetActive(false);
            desk2NotAvailable.SetActive(true);
        }
        if (meshRenderer != null)
        {
            meshRenderer.material = mat;
        }
        if (breakPoint != null)
        {
            breakPoint.SetActive(true);
        }
        if (breakPoint2 != null)
        {
            breakPoint2.SetActive(true);
        }
        JsonSave(dataKey, deskLevel);

        navMeshObstacle.center = meshfilt.gameObject.transform.localPosition - meshfilt.gameObject.transform.localPosition;
        navMeshObstacle.size = meshfilt.mesh.bounds.size;
        GameManager.instance.GetNavmesh().BuildNavMesh();
    }
    public void JsonSave(string dataKey, int deskLevel)
    {
        desk = new GameRoomSave(dataKey, deskLevel);
        string jsonString = JsonUtility.ToJson(desk);
        File.WriteAllText(Application.dataPath + "/Saves/" + dataKey + ".json", jsonString);
    }
    public void JsonLoad()
    {
        string path = Application.dataPath + "/Saves/" + dataKey + ".json";
        if (File.Exists(path))
        {
            string jsonUpload = File.ReadAllText(path);
            GameRoomSave gameRoomSave = JsonUtility.FromJson<GameRoomSave>(jsonUpload);
            if (gameRoomSave.deskLevel == 1)
            {
                meshfilt.mesh = mesh1;
                desk1Available.SetActive(false);
                desk1NotAvailable.SetActive(true);
            }
            if (gameRoomSave.deskLevel == 2)
            {
                meshfilt.mesh = mesh2;
                desk1Available.SetActive(false);
                desk1NotAvailable.SetActive(true);
                desk2Available.SetActive(false);
                desk2NotAvailable.SetActive(true);
            }
            if (gameRoomSave.deskLevel > 0 && meshRenderer != null)
            {
                meshRenderer.material = mat;
            }
            if (breakPoint != null)
            {
                breakPoint.SetActive(true);
            }
            if (breakPoint2 != null)
            {
                breakPoint2.SetActive(true);
            }
            navMeshObstacle.center = meshfilt.gameObject.transform.localPosition - meshfilt.gameObject.transform.localPosition;
            navMeshObstacle.size = meshfilt.mesh.bounds.size;
            GameManager.instance.GetNavmesh().BuildNavMesh();

        }
    }
}

