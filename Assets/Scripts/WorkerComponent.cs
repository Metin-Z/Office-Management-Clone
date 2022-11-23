using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class WorkerComponent : MonoBehaviour
{
    #region SerializeFields
    [Header("Objects")]
    [SerializeField] GameObject myDesk;


    //[Header("Values")]
    

    #endregion
    public void GetMyDesk(GameObject desk)
    {
        myDesk = desk;
    }
}
