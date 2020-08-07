using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private int blueDinoLayerNumber = 8;

    private Animator animator;
    
    private bool active = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public bool IsLeverActive()
    {
        return active;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == blueDinoLayerNumber && !active)
        {
            active = true;
            animator.SetBool("Active", true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == blueDinoLayerNumber && active)
        {
            active = false;
            animator.SetBool("Active", false);
        }
    }
}
