using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPropertyUI : MonoBehaviour
{
    public static PlayerPropertyUI Instance { get; private set; }

    private GameObject UIGameObject;

    private Image HPProgressBar;
    private TextMeshProUGUI HPText;

    private Image LevelProgressBar;
    private TextMeshProUGUI LevelText;

    private GameObject PropertyGrid;
    public GameObject PropertyTemplate;

    private Image WeaponIcon;

    private PlayerProperty Pp;

    private PlayerAttack Pa;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        UIGameObject = transform.Find("UI").gameObject;
        HPProgressBar = transform.Find("UI/HPProgressBar/ProgressBar").GetComponent<Image>();
        HPText = transform.Find("UI/HPProgressBar/HPText").GetComponent<TextMeshProUGUI>();
        LevelProgressBar = transform.Find("UI/LevelProgressBar/ProgressBar").GetComponent<Image>();
        LevelText = transform.Find("UI/LevelProgressBar/LevelText").GetComponent<TextMeshProUGUI>();
        PropertyGrid = transform.Find("UI/PropertyGrid").gameObject;
        WeaponIcon = transform.Find("UI/WeaponIcon").GetComponent<Image>();

        PropertyTemplate.SetActive(false);

        GameObject player = GameObject.FindGameObjectWithTag(Tag.PLAYER);
        Pp = player.GetComponent<PlayerProperty>();
        Pa = player.GetComponent<PlayerAttack>();
        UpdatePlayerPropertyUI();
        Hide();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (UIGameObject.activeSelf)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }
    }

    private void Show()
    {
        UIGameObject.SetActive(true);
    }

    private void Hide()
    {
        UIGameObject.SetActive(false);
    }

    public void UpdatePlayerPropertyUI()
    {
        HPProgressBar.fillAmount = Pp.Hp / 100.0f;
        HPText.text = Pp.Hp + "/100";
        LevelProgressBar.fillAmount = (Pp.CurrentExp * 1.0f) / ( Pp.Level * 30.0f );
        LevelText.text = Pp.Level.ToString();

        foreach (Transform child in PropertyGrid.transform)
        {
            if (child.gameObject.activeSelf)
            {
                Destroy(child.gameObject);
            }
        }
        AddProperty("EP: " + Pp.Energy);
        AddProperty("MP: " + Pp.Mental);

        foreach (var item in Pp.PropertyDict)
        {
            string propertyType = "";
            switch (item.Key)
            {
                case PropertyType.AttackValue:
                    propertyType = "AP: ";
                    break;
                case PropertyType.SpeedValue:
                    propertyType = "SP: ";
                    break;
                default:
                    break;
            }

            int sum = 0;
            foreach (var everyValue in item.Value)
            {
                sum += everyValue.value;
            }

            AddProperty(propertyType + sum);
        }

        if (Pa.WeaponIcon != null)
        {
            WeaponIcon.sprite = Pa.WeaponIcon;
        }
    }

    private void AddProperty(string propertyStr)
    {
        GameObject go = GameObject.Instantiate(PropertyTemplate);
        go.SetActive(true);
        go.transform.SetParent(PropertyGrid.transform);
        go.transform.localScale = Vector3.one;
        go.transform.Find("Property").GetComponent<TextMeshProUGUI>().text = propertyStr;
    }
}
