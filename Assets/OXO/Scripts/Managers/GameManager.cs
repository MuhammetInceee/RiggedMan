using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


public class GameManager : Singleton<GameManager>
{
    [FoldoutGroup("Game Status Bools")]
     public bool isStarted = false;
     [FoldoutGroup("Game Status Bools")]
     public bool isFinished = false;
     [FoldoutGroup("Game Status Bools")]
     public bool isWin = false;
     [FoldoutGroup("Game Status Bools")]
     public bool isFail = false;


    private void Awake()
    {
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    public void StartGame()
    {
        Debug.Log($"<color=#5fe769><b>Game is started!</b> </color>");
        isStarted = true;
        
        isFinished = false;
        isFail = false;
        isWin = false;
        
        Actions.OnGameStarted?.Invoke();
    }

    public void FinishTheGame()
    {
        isFinished = true;
        isStarted = false;
    }
    [Button]
    public void LevelComplete()
    {
        isWin = true;
        
        FinishTheGame();
        ConfettiManager.Instance.Play();
        CanvasManager.Instance.OpenFinishRect(true);
        
        Actions.OnGameCompleted?.Invoke();
    }
    [Button]
    public void LevelFail()
    {
        isFail = true;
        
        FinishTheGame();

        CanvasManager.Instance.OpenFinishRect(false);
        
        Actions.OnGameFailed?.Invoke();
    }
}