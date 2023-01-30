using System;
using System.Collections.Generic;
using System.Globalization;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CameraSettings : MonoBehaviour
{
    private Vector3 _startPos;

    [Header("References")] public ReferenceStruct Reference;
    public ValueSettingsStruct ValueSettings;
    public TextReferencesStruct TextReferences;

    private void Awake()
    {
        GetReferences();
        InitVariables();
    }

    private void CameraValueChanger(float arg0)
    {
        CinemachineTransposer transposerComponent = Reference.virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        
        Transform virtualCameraTransform = Reference.virtualCamera.transform;
        Vector3 changePos = _startPos + new Vector3(Reference.cameraXSlider.value, Reference.cameraYSlider.value,
            Reference.cameraZSlider.value);
        
        if (Reference.virtualCamera.Follow == null)
        {
            virtualCameraTransform.position = changePos;
        }
        else
        {
            transposerComponent.m_FollowOffset = changePos;
            
        }
        HeaderTextSetter();
        
    }

    private void CameraFovChanger(float arg0)
    {
        Reference.virtualCamera.m_Lens.FieldOfView = Reference.cameraFovSlider.value;
        HeaderTextSetter();
    }
    
    private void GameSpeedChanger(float arg0)
    {
        Time.timeScale = 0.5f;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;

        Time.timeScale = Reference.gameSpeedSlider.value;
        HeaderTextSetter();
    }
    private void InitVariables()
    {
        // Camera Start Pos Set
        _startPos = Reference.virtualCamera.transform.position;

        //Slider Min Max Values Set
        CameraSliderMinValueSetter(Reference.cameraXSlider, Reference.cameraYSlider, Reference.cameraZSlider);
        CameraSliderMaxValueSetter(Reference.cameraXSlider, Reference.cameraYSlider, Reference.cameraZSlider);
        
        // Fov Slider Values Set
        Reference.cameraFovSlider.minValue = ValueSettings.fovSliderMinValue;
        Reference.cameraFovSlider.maxValue = ValueSettings.fovSliderMaxValue;
        Reference.cameraFovSlider.value = Reference.virtualCamera.m_Lens.FieldOfView;
        
        // Game Speed Slider Values Set
        Reference.gameSpeedSlider.minValue = ValueSettings.gameSpeedMinValue;
        Reference.gameSpeedSlider.maxValue = ValueSettings.gameSpeedMaxValue;
        Reference.gameSpeedSlider.value = Time.timeScale;

        // Slider Value Changers
        Reference.cameraXSlider.onValueChanged.AddListener(CameraValueChanger);
        Reference.cameraYSlider.onValueChanged.AddListener(CameraValueChanger);
        Reference.cameraZSlider.onValueChanged.AddListener(CameraValueChanger);
        Reference.cameraFovSlider.onValueChanged.AddListener(CameraFovChanger);
        Reference.gameSpeedSlider.onValueChanged.AddListener(GameSpeedChanger);

        // Text Sets
        CameraSlidersMinMaxValueSet();
        CameraFovSliderMinMaxValueSet();
        GameSpeedSliderMinMaxValueSet();
        HeaderTextSetter();
    }




    private void GetReferences()
    {
        Reference.virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
    }

    #region CameraSliderValueSetterFunctions

    private void CameraSliderMaxValueSetter(params Slider[] maxValues)
    {
        foreach (Slider slider in maxValues)
        {
            slider.maxValue = ValueSettings.cameraSlidersMaxValue;
        }
    }

    private void CameraSliderMinValueSetter(params Slider[] minValues)
    {
        foreach (Slider slider in minValues)
        {
            slider.minValue = ValueSettings.cameraSlidersMinValue;
        }
    }

    #endregion

    #region TextSets

    private void CameraSlidersMinMaxValueSet()
    {
        // All Min Value Text Set
        foreach (TextMeshProUGUI text in TextReferences.minTexts)
        {
            text.text = ValueSettings.cameraSlidersMinValue.ToString(CultureInfo.InvariantCulture);
        }

        // All Max Value Text Set
        foreach (TextMeshProUGUI text in TextReferences.maxTexts)
        {
            text.text = ValueSettings.cameraSlidersMaxValue.ToString(CultureInfo.InvariantCulture);
        }
    }

    private void CameraFovSliderMinMaxValueSet()
    {
        TextReferences.cameraFovMinText.text = ValueSettings.fovSliderMinValue.ToString(CultureInfo.InvariantCulture);
        TextReferences.cameraFovMaxText.text = ValueSettings.fovSliderMaxValue.ToString(CultureInfo.InvariantCulture);
    }

    private void GameSpeedSliderMinMaxValueSet()
    {
        TextReferences.gameSpeedMinValue.text = ValueSettings.gameSpeedMinValue.ToString(CultureInfo.InvariantCulture);
        TextReferences.gameSpeedMaxValue.text = ValueSettings.gameSpeedMaxValue.ToString(CultureInfo.InvariantCulture);
    }

    private void HeaderTextSetter()
    {
        float currentX = Reference.cameraXSlider.value;
        float currentY = Reference.cameraYSlider.value;
        float currentZ = Reference.cameraZSlider.value;
        float currentFov = Reference.cameraFovSlider.value;
        float currentGameSpeed = Reference.gameSpeedSlider.value;

        TextReferences.cameraXHeaderText.text = $"Camera X : <color=orange>{currentX:0.0}</color>";
        TextReferences.cameraYHeaderText.text = $"Camera Y : <color=orange>{currentY:0.0}</color>";
        TextReferences.cameraZHeaderText.text = $"Camera Z : <color=orange>{currentZ:0.0}</color>";
        TextReferences.cameraFovHeaderText.text = $"Camera Fov : <color=orange>{currentFov:0.0}</color>";
        TextReferences.gameSpeedHeaderText.text = $"Game Speed : <color=orange>{currentGameSpeed:0.0}</color>";
    }

    #endregion

    #region Structs

    [Serializable]
    public struct ReferenceStruct
    {
        public CinemachineVirtualCamera virtualCamera;

        public Slider cameraXSlider;
        public Slider cameraYSlider;
        public Slider cameraZSlider;
        public Slider cameraFovSlider;
        public Slider gameSpeedSlider;
    }

    [Serializable]
    public struct ValueSettingsStruct
    {
        [Header("Camera Slider Values")]
        public float cameraSlidersMinValue;
        public float cameraSlidersMaxValue;

        [Header("Fov Slider Values")]
        public float fovSliderMinValue;
        public float fovSliderMaxValue;

        [Header("Game Speed Values")]
        public float gameSpeedMinValue;
        public float gameSpeedMaxValue;
    }

    [Serializable]
    public struct TextReferencesStruct
    {
        [Header("Camera Position Texts")]
        public List<TextMeshProUGUI> minTexts;
        public List<TextMeshProUGUI> maxTexts;

        [Header("Camera Fov Texts")] 
        public TextMeshProUGUI cameraFovMinText;
        public TextMeshProUGUI cameraFovMaxText;

        [Header("Game Speed Texts")] 
        public TextMeshProUGUI gameSpeedMinValue;
        public TextMeshProUGUI gameSpeedMaxValue;
        
        [Header("Header Texts")]
        public TextMeshProUGUI cameraXHeaderText;
        public TextMeshProUGUI cameraYHeaderText;
        public TextMeshProUGUI cameraZHeaderText;
        public TextMeshProUGUI cameraFovHeaderText;
        public TextMeshProUGUI gameSpeedHeaderText;

    }

    #endregion
}