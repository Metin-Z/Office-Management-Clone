using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelUpEffects : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasg;
    public GameObject Parent;
    public bool LevelUp;
    void Start()
    {
        if (LevelUp == true)
        {
            canvasg.DOFade(0, 3);
            Destroy(Parent, 3.1f);
        }
    }

    void Update()
    {
        if (LevelUp == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x,transform.position.y+1,transform.position.z) , 1 * Time.deltaTime);
        }     
    }
}
