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
        nameText = transform.Find("NameTextBg/NameText").GetComponent<TextMeshProUGUI>();
        contentText = transform.Find("Bg/ContentText").GetComponent<TextMeshProUGUI>();
        continueButton = transform.Find("ContinueButton").GetComponent<UnityEngine.UI.Button>();
        continueButton.onClick.AddListener(this.OnContinueButtonClick);
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Show(string name, string[] content)
    {
        if (gameObject.activeSelf == false)
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
        gameObject.SetActive(false);
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
