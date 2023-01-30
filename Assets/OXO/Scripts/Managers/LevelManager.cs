using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Sirenix.OdinInspector;
using UnityEngine;


public class LevelManager : Singleton<LevelManager>
{
    [HideInInspector] public List<GameObject> currentLevelObjectsList;

    //[HideInInspector]
    public List<GameObject> levelPrefabList;

    [Header("Current Level")] public GameObject currentLevel;
    public GameObject player;

    [Header("Player Prefab")] public GameObject playerPrefab;

    public int level;

    private void Awake()
    {
        level = PlayerPrefs.GetInt("Level");
    }

    private void Start()
    {
        SetLevel();
    }

    public void SetLevel()
    {
        ResetLevel();
        ConfettiManager.Instance.Stop();
        Spawn();
    }

    public void Spawn()
    {
        if (levelPrefabList.Count == 0)
        {
            Debug.Log($"<color=orange><b>(!) Couldn't find level in the Level List.</b> </color>");
            return;
        }

        currentLevel = Instantiate(levelPrefabList[level % levelPrefabList.Count]);
        currentLevel.SetActive(true);
        currentLevelObjectsList.Add(currentLevel);

        CanvasManager.Instance.levelText.text = "Level " + (level + 1).ToString();

        if (playerPrefab)
        {
            player = Instantiate(playerPrefab);
            player.transform.position = Vector3.zero;
            player.SetActive(true);
            currentLevelObjectsList.Add(player);
        }
    }

    public void ResetLevel()
    {
        ResetList(currentLevelObjectsList);
    }

    public void IncreaseLevel()
    {
        level++;
        PlayerPrefs.SetInt("Level", level);
    }

    public void ResetList(List<GameObject> list)
    {
        list.ForEach(x => Destroy(x.gameObject));
        list.Clear();
    }

    public void DecreaseLevel()
    {
        level--;
        PlayerPrefs.SetInt("Level", level);
    }

    public void ResetCamera()
    {
        //Camera.main.GetComponent<CameraDisplay>().ResetCamera();
    }

    public void NextLevel()
    {
        IncreaseLevel();
        SetLevel();
    }

    public void PreviousLevel()
    {
        DecreaseLevel();
        SetLevel();
    }

    public void SetLevelWithInput(int setLevel)
    {
        level = setLevel - 1;
        PlayerPrefs.SetInt("Level", level);

        SetLevel();
    }
}