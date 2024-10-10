using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPick : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == Tag.INTERACTABLE)
        {
            PickableObject po = other.gameObject.GetComponent<PickableObject>();

            if (po != null)
            {
                Transform javelinGenerator = other.gameObject.transform.parent;

                InventoryManager.Instance.AddItem(po.itemSO);

                // 删除标枪生成器
                if (javelinGenerator != null)
                {
                    Destroy(javelinGenerator.gameObject);
                }

                Destroy(po.gameObject);
            }
        }
    }
}
