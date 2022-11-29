using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakComponent : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Worker"))
        {
            other.TryGetComponent(out Animator anim);
            anim.SetBool("Gaming",true);
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
