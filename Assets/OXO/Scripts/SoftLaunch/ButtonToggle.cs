using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonToggle : MonoBehaviour
{
    private Button _button;
    
    [SerializeField] private TextMeshProUGUI positiveText;
    [SerializeField] private TextMeshProUGUI negativeText;


    private void Awake()
    {
        GetReference();
        InitVariables();
    }

    private void Toggle()
    {
        if (positiveText.gameObject.activeInHierarchy)
        {
            positiveText.gameObject.SetActive(false);
            negativeText.gameObject.SetActive(true);
        }
        else
        {
            positiveText.gameObject.SetActive(true);
            negativeText.gameObject.SetActive(false);
        }
    }

    private void GetReference()
    {
        _button = GetComponent<Button>();
    }

    private void InitVariables()
    {
        _button.onClick.AddListener(Toggle);
    }
}
