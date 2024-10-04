using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDBManger : MonoBehaviour
{
    public static ItemDBManger Instance { get; private set; }

    public ItemDBSO itemDB;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }

    public ItemScriptObject GetRandomItem()
    {
        int randomindex = Random.Range(0, itemDB.itemList.Count);
        return itemDB.itemList[randomindex];
    }
}
