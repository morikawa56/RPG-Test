using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JavelinBullet : MonoBehaviour
{
    public int attackVal = 20;
    private Rigidbody rgd;
    private Collider col;

    private bool hasEquiped = false;
    public bool HasEjected = false;

    private void Start()
    {
        rgd = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(hasEquiped.ToString());
        if (other.collider.CompareTag(Tag.PLAYER)) return;
        if (!hasEquiped)
        {
            Invoke(nameof(FreezeObject), 2.0f);
            return;
        }

        FreezeObject();
        transform.parent = other.gameObject.transform;

        if (other.gameObject.tag == Tag.ENEMY)
        {
            print(11111);
            other.gameObject.GetComponent<Enemy>().TakeDamage(attackVal);
        }

        Destroy(this.gameObject, 1f);
    }

    private void FreezeObject()
    {
        if (hasEquiped)
        {
            rgd.isKinematic = true;
            col.enabled = false;
        }
        rgd.velocity = Vector3.zero;

    }

    public void SetEquiped(bool value)
    {
        hasEquiped = value;
    }
}
