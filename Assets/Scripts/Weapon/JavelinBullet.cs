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
    private Vector3 ejectPoint;
    private float destroyDistance = 20f;

    private bool hasEquiped = false;
    public bool HasEjected = false;

    private void Start()
    {
        rgd = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag(Tag.PLAYER)) return;
        if (!hasEquiped && !HasEjected)
        {
            print("!hasEquiped || !HasEjected");
            Invoke(nameof(FreezeObject), 2.0f);
            return;
        }

        FreezeObject();
        transform.parent = other.gameObject.transform;

        if (other.gameObject.tag == Tag.ENEMY)
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(attackVal);
        }

        Destroy(this.gameObject, 1f);
    }

    private void Update()
    {
        if (this.tag != Tag.INTERACTABLE && HasEjected)
        {
            float distance = Vector3.Distance(transform.position, ejectPoint);
            if (distance > destroyDistance)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void FreezeObject()
    {
        if (hasEquiped || HasEjected)
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

    public void SetEjectPoint(Vector3 Opoint)
    {
        ejectPoint = Opoint;
    }
}
