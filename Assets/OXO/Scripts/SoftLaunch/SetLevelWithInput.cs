using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetLevelWithInput : MonoBehaviour
{
    private Button _button;
    
    [SerializeField] private TMP_InputField text;

    private void Awake()
    {
        GetReference();
        InitVariables();
    }

    private void SetLevel()
    {
        if(string.IsNullOrEmpty(text.text)) return;
        LevelManager.Instance.SetLevelWithInput(int.Parse(text.text));
    }

    private void GetReference()
    {
        _button = GetComponent<Button>();
    }

    private void InitVariables()
    {
        _button.onClick.AddListener(SetLevel);
    }
}
