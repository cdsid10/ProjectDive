using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTest : MonoBehaviour
{
    private Animator animator;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    public void Animate(bool open)
    {
        if (open)
        {
            animator.SetTrigger("Open");
        }
        else
        {
            animator.SetTrigger("Close");
        }
    }


    
    /* Diagnosis

    private void Start()
    {
        StartCoroutine(StartCoroutine());
    }


    private IEnumerator StartCoroutine()
    {
        yield return new WaitForSeconds(1.0f);
        Animate(true);
        yield return new WaitForSeconds(1.5f);
        Animate(false);

        yield return null;
    }
    */
}