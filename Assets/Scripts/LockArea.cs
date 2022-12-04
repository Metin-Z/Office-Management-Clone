using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockArea : MonoBehaviour
{
    #region SerializeFields
    [SerializeField] private GameObject gameGround;
    [SerializeField] private GameObject officeGround;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject officePanel;
    #endregion
    public static LockArea instance;

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
        ChekGround();
    }
    public void ChekGround()
    {
        StartCoroutine(ChekDelayGround());
    }
    public IEnumerator ChekDelayGround()
    {
        yield return new WaitForSeconds(2);
        if (PlayerPrefs.GetInt("GameBuy") == 1)
        {
            gameGround.SetActive(true);
            GameManager.instance.GetNavmesh().BuildNavMesh();
            gamePanel.SetActive(false);
        }

        if (PlayerPrefs.GetInt("OfficeBuy") == 1)
        {
            officeGround.SetActive(true);
            GameManager.instance.GetNavmesh().BuildNavMesh();
            officePanel.SetActive(false);
        }
    }
    public void BuyGameArea(GameObject buttonComp)
    {
        buttonComp.TryGetComponent(out ButtonComponent button);
        if (GameManager.instance.GetMoney() >= button.Getprice() && LevelManager.instance.Levels[0].id >= 3)
        {
            PlayerPrefs.SetInt("GameBuy", 1);
            GameManager.instance.GetSlider().LevelBarUpdate(button.GetXP());
            gameGround.SetActive(true);
            GameManager.instance.DecreaseMoney(button.Getprice());
            GameManager.instance.GetNavmesh().BuildNavMesh();
            gamePanel.SetActive(false);
        }
    }
    public void BuyOfficeArea(GameObject buttonComp)
    {
        buttonComp.TryGetComponent(out ButtonComponent button);
        if (GameManager.instance.GetMoney() >= button.Getprice() && LevelManager.instance.Levels[0].id >= 6)
        {
            PlayerPrefs.SetInt("OfficeBuy", 1);
            GameManager.instance.GetSlider().LevelBarUpdate(button.GetXP());
            officeGround.SetActive(true);
            GameManager.instance.DecreaseMoney(button.Getprice());
            GameManager.instance.GetNavmesh().BuildNavMesh();
            officePanel.SetActive(false);

        }
    }
}
