using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskComponent : MonoBehaviour
{
    #region SerializeFields
    [Header("Meshs")]
    [SerializeField] private Mesh desk0;
    [SerializeField] private Mesh desk1;
    [SerializeField] private Mesh desk2;

    [SerializeField] private MeshFilter meshfilt;
    [Header("Available Level")]
    [SerializeField] private int avilableLevel;
    #endregion
    [Header("Base Panel")]
    public GameObject buyPanel;
    public GameObject buyWorker;
    [Header("Desk Objects")]
    public int deskLevel;


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
        if (LevelManager.instance.Levels[0].id == avilableLevel)
        {
            gameObject.SetActive(true);
        }
    }
}
