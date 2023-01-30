using System;
using Sirenix.OdinInspector;

using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class MoneyControlManager : Singleton<MoneyControlManager>
{
    public static Action OnMoneyChange;

    [SerializeField] private TextMeshProUGUI[] coinTexts;
    public float currentMoney;

    [Header("Debug Mode")]
    public float money = 100;
    public KeyCode keyCode = KeyCode.L;
    private IEnumerator Start()
    {
        currentMoney = PlayerPrefs.GetFloat("Money",0f);
        yield return new WaitUntil(() => CanvasManager.Instance);
        MoneyTextUpdate();
    }
    [Button]
    public bool ChangeMoney(float money)
    {
        if (!CheckCanBuy(money)) { return false; }
        else
        {
            OnMoneyChange?.Invoke();
            currentMoney += money;
            SetPlayerPrefs();
            MoneyTextUpdate();
            return true;

        }
      
    }
    public bool CheckCanBuy(float money)
    {
        if (currentMoney + money < 0) { return false; }
        else
            return true;
    }
    public void SetPlayerPrefs() => PlayerPrefs.SetFloat("Money", currentMoney);
    private void OnDisable() => SetPlayerPrefs();

    public void IncreaseMoney()
    {
        ChangeMoney(money);
    }

    public void DecreaseMoney()
    {
        ChangeMoney(-money);
    }
    private void Update()
    {
        if (Input.GetKeyDown(keyCode)) { IncreaseMoney(); }
    }
    
    
    private void MoneyTextUpdate()
    {
        foreach (TextMeshProUGUI text in coinTexts)
        {
            text.text = $"{Formatter.Format(currentMoney)}";
            text.transform.localScale = Vector3.one;
            text.transform.DOKill();
            text.transform.DOPunchScale(Vector3.one * 0.25f, .25f, 1, 0.1f).SetEase(Ease.InOutBounce);
        }
    }
}
