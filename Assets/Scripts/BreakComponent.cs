using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BreakComponent : MonoBehaviour
{
    private void Start()
    {
        GameManager.instance.BreakPoints.Add(transform);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Worker"))
        {
            other.TryGetComponent(out Animator anim);
            anim.SetBool("Gaming",true);          
            other.transform.Rotate(0, 180, 0);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Worker"))
        {           
            other.TryGetComponent(out Animator anim);
            anim.SetBool("Gaming", false);
        }
    }
}
