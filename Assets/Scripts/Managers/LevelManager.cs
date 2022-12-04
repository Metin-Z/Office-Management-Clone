using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public List<Level> Levels;    

    #region SerializeFields
    [SerializeField] private Transform  fireArea;
    #endregion

    public Transform GetFirePos()
    {
        return fireArea;
    }
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
}
