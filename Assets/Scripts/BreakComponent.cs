using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BreakComponent : MonoBehaviour
{
    [SerializeField] private bool gaming;
    [SerializeField] private bool gamingArcade;
    private void Start()
    {
        GameManager.instance.BreakPoints.Add(transform);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Worker"))
        {
            GameManager.instance.BreakPoints.Remove(transform);
            other.TryGetComponent(out Animator anim);
            if (gaming)
            {         
                anim.SetBool("Gaming", true);
            }
            if (gamingArcade)
            {
                anim.SetBool("GamingArcade", true);
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Worker"))
        {
            GameManager.instance.BreakPoints.Add(transform);
            other.TryGetComponent(out Animator anim);
            if (gaming)
            {
                anim.SetBool("Gaming", false);
            }
            if (gamingArcade)
            {
                anim.SetBool("GamingArcade", false);
            }
        }
    }
}
