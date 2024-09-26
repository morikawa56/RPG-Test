using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheWeapon : Weapon
{
    private const string ANIM_PARM_ISATTACK = "IsAttack";
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {

    }

    public override void Attack()
    {
        animator.SetTrigger(ANIM_PARM_ISATTACK);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            print("Trigger with: " + other.name);
        }
    }
}
