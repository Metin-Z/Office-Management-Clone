using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class InterfaceManager : MonoBehaviour
{
    public static InterfaceManager instance;

    #region SerializeFields
    [SerializeField] private TextMeshProUGUI mainMoney_TXT;
    #endregion

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
        UpdateMoney();
    }

    public TextMeshProUGUI GetMoneyText()
    {
        return mainMoney_TXT;
    } 
    public void UpdateMoney()
    {
        mainMoney_TXT.text = $"${GameManager.instance.GetMoney()}";
    }
}
