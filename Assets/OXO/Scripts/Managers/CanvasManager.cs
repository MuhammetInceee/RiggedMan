using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Managers.Sound;
using Managers.Vibration;
using TMPro;

public class CanvasManager : Singleton<CanvasManager>
{

    public GameObject tapToPlayButton;
    public GameObject nextLevelButton;
    public GameObject retryLevelButton;

    public GameObject tutorialRect;
    public GameObject mainMenuRect;
    public GameObject inGameRect;
    public GameObject finishRect;

    public Image levelSliderImage;

    public TextMeshProUGUI levelText;
    public TextMeshProUGUI coinText;

    public GameObject winPanel;
    public GameObject failPanel;

    [Header("Control Panel")] 
    [SerializeField] private CanvasGroup controlPanel;
    [SerializeField] private Button soundOpenButton;
    [SerializeField] private Button soundCloseButton;
    [SerializeField] private Button vibrationOpenButton;
    [SerializeField] private Button vibrationCloseButton;
    [SerializeField] private Button replayButton;

    private bool isControlPanelOpened = false;
    private void OnEnable()
    {
        ControlPanelsOnLevelChanged();
    }

    private void OnDisable()
    {
        ControlPanelsOnLevelChanged();
    }

    private void Awake()
    {
        mainMenuRect.SetActive(true);
    }

    public void TapToPlayButtonClick()
    {
        GameManager.Instance.StartGame();
    }
    
    public void RestartGame()
    {
        LevelManager.Instance.SetLevel();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void OpenFinishRect(bool isSuccess)
    {
        if (isSuccess)
        {
            failPanel.SetActive(false);
            winPanel.SetActive(true);
        }
        else
        {
            failPanel.SetActive(true);
            winPanel.SetActive(false);
        }

        inGameRect.SetActive(false);

        if (finishRect.TryGetComponent(out CanvasGroup finishCanvasGroup))
        {
            finishCanvasGroup.alpha = 0f;
            finishCanvasGroup.DOFade(1, 1.5f);    
        }
        

        finishRect.SetActive(true);
    }
    private void ControlPanelsOnLevelChanged()
    {
        mainMenuRect.SetActive(true);
        inGameRect.SetActive(false);
        finishRect.SetActive(false);
        
        winPanel.SetActive(false);
        failPanel.SetActive(false);
        
        retryLevelButton.SetActive(false);
        nextLevelButton.SetActive(false);
    }
    
    public void ControlPanelController(int index)
    {
        if (index == 1)
        {
            isControlPanelOpened = true;
            controlPanel.gameObject.SetActive(true);
            controlPanel.DOFade(1, 0.5f);
        }
        else
        {
            controlPanel.DOFade(0, 0.5f).OnComplete(DisablePanel);
        }
    }

    private void DisablePanel()
    {
        controlPanel.alpha = 0;
        controlPanel.gameObject.SetActive(false);
        isControlPanelOpened = false;
    }

    public void ControlSoundButtons(int buttonIndex)
    {
        if (buttonIndex == 1)
        {
            soundOpenButton.gameObject.SetActive(false);
            soundCloseButton.gameObject.SetActive(true);
            SoundManager.ControlSound();
        }
        else
        {
            soundCloseButton.gameObject.SetActive(false);
            soundOpenButton.gameObject.SetActive(true);
            SoundManager.ControlSound();
        }
    }
    public void ControlVibrationButtons(int buttonIndex)
    {
        if (buttonIndex == 1)
        {
            vibrationOpenButton.gameObject.SetActive(false);
            vibrationCloseButton.gameObject.SetActive(true);
            VibrationManager.ControlVibration();
        }
        else
        {
            vibrationCloseButton.gameObject.SetActive(false);
            vibrationOpenButton.gameObject.SetActive(true);
            VibrationManager.ControlVibration();
        }
    }
    public void ControlReplayButton()
    {
        DisablePanel();
        RestartGame();
        mainMenuRect.SetActive(true);
    }

    public void OpenPrivacyPolicyMenu()
    {
       // Elephant.ShowSettingsView();   
    }

}