using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JavelinBullet : MonoBehaviour
{
    private Rigidbody rgd;
    private Collider col;

    private void Start()
    {
        rgd = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag(Tag.PLAYER)) return;

        rgd.velocity = Vector3.zero;
        rgd.isKinematic = true;
        col.enabled = false;

        Destroy(this.gameObject, 1f);
    }
}
