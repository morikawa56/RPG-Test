using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class DialogUI : MonoBehaviour
{
    public static DialogUI Instance { get; private set; }

    private TextMeshProUGUI nameText;
    private TextMeshProUGUI contentText;
    private Button continueButton;
    private int contentIndex = 0;

    public List<string> contentList;

    private GameObject _uiGameObject;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }

    public void Start()
    {
        nameText = transform.Find("UI/NameTextBg/NameText").GetComponent<TextMeshProUGUI>();
        contentText = transform.Find("UI/Bg/ContentText").GetComponent<TextMeshProUGUI>();
        continueButton = transform.Find("UI/ContinueButton").GetComponent<UnityEngine.UI.Button>();
        continueButton.onClick.AddListener(this.OnContinueButtonClick);
        _uiGameObject = transform.Find("UI").gameObject;
        Hide();
    }

    public void Show()
    {
        _uiGameObject.SetActive(true);
    }

    public void Show(string name, string[] content)
    {
        if (_uiGameObject.activeSelf == false)
        {
            Show();
            contentIndex = 0;
        }
        nameText.text = name;
        contentList = new List<string>();
        contentList.AddRange(content);
        contentText.text = contentList[contentIndex];
    }

    public void Hide()
    {
        _uiGameObject.SetActive(false);
    }

    public void OnContinueButtonClick()
    {
        contentIndex++;
        if (contentIndex >= contentList.Count)
        {
            Hide();
            return;
        }
        contentText.text = contentList[contentIndex];
    }
}
