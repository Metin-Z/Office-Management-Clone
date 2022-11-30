using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BreakComponent : MonoBehaviour
{
    [SerializeField] private bool gaming;
    [SerializeField] private bool gamingArcade;
    [SerializeField] private bool offenSiveIdle;
    private void Start()
    {
        GameManager.instance.BreakPoints.Add(transform);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Worker"))
        {
            GameManager.instance.BreakPoints.Remove(transform);
            other.TryGetComponent(out WorkerComponent workerComp);
            workerComp.GetBreakObject(transform.parent.gameObject);
            other.TryGetComponent(out Animator anim);
            if (gaming)
            {         
                anim.SetBool("Gaming", true);
            }
            if (gamingArcade)
            {
                anim.SetBool("GamingArcade", true);
            }
            if (offenSiveIdle)
            {
                anim.SetBool("OffenSiveIdle", true);
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
            if (offenSiveIdle)
            {
                anim.SetBool("OffenSiveIdle", false);
            }
        }
    }
}
