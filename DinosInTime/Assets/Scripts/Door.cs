using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private int redDinoLayerNumber = 10;
    
    private Animator animator;
    private Lever lever;

    private bool open;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        lever = FindObjectOfType<Lever>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Open", lever.IsLeverActive());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == redDinoLayerNumber && lever.IsLeverActive())
        {
            FindObjectOfType<LevelController>().NextLevel();
        }
    }
}
