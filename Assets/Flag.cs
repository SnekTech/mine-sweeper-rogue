using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    private Animator animator;
    private static readonly int Growing = Animator.StringToHash("growing");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        // animator.SetBool(Growing, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
