using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance { get; private set; }
    private GameObject _uiGameObject;
    private GameObject _content;
    public GameObject ItemPrefab;
    private bool _isShow = false;

    public ItemDetailUI ItemDetailUIObj;

    private void Awake()
    {
        if (Instance != null && Instance != null)
        {
            Destroy(this.gameObject);
        }
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _uiGameObject = transform.Find("UI").gameObject;
        _content = transform.Find("UI/ListBg/Scroll View/Viewport/Content").gameObject;
        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (_isShow)
            {
                Hide();
                _isShow = false;
            }
            else
            {
                Show();
                _isShow = true;
            }
        }
    }

    public void Show()
    {
        _uiGameObject.SetActive(true);
    }

    public void Hide()
    {
        _uiGameObject.SetActive(false);
    }

    public void AddItem(ItemScriptObject itemSO)
    {
        GameObject itemGo = GameObject.Instantiate(ItemPrefab);
        itemGo.transform.SetParent(_content.transform);
        itemGo.transform.localScale = Vector3.one;
        ItemUI itemUI = itemGo.GetComponent<ItemUI>();
        itemUI.InitItem(itemSO);
    }

    public void OnItemClick(ItemScriptObject itemSO, ItemUI itemUI)
    {
        ItemDetailUIObj.UpdateItemDetailUI(itemSO, itemUI);
    }

    public void OnItemUse(ItemScriptObject itemSO, ItemUI itemUI)
    {
        Destroy(itemUI.gameObject);
        InventoryManager.Instance.RemoveItem(itemSO);

        GameObject.FindGameObjectWithTag(Tag.PLAYER).GetComponent<Player>().UseItem(itemSO);
    }
}
