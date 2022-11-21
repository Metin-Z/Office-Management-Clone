using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskComponent : MonoBehaviour
{
    #region SerializeFields
    [SerializeField] private Mesh desk0;
    [SerializeField] private Mesh desk1;
    [SerializeField] private Mesh desk2;

    [SerializeField] private MeshFilter meshfilt;
    #endregion
    public GameObject buyPanel;
    public GameObject buyWorker;
    public int price;
    [Header("Desk Objects")]
    public int deskLevel;


    public GameObject buyDesk;
    public GameObject desk1Available;
    public GameObject desk1NotAvailable;
    public GameObject desk2Available;
    public GameObject desk2NotAvailable;

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
    public void UpgradeDesk(int level)
    {
        if (GameManager.instance.GetMoney() > price)
        {
            deskLevel = level;
            levelControl();
        }
    }
    public void levelControl()
    {
        if (deskLevel == 1)
        {
            meshfilt.mesh = desk1;
            GameManager.instance.DecreaseMoney(25);
            desk1Available.SetActive(false);
            desk1NotAvailable.SetActive(true);
        }
        if (deskLevel == 2)
        {
            meshfilt.mesh = desk2;
            GameManager.instance.DecreaseMoney(75);
            desk2Available.SetActive(false);
            desk2NotAvailable.SetActive(true);
        }
    }
}
