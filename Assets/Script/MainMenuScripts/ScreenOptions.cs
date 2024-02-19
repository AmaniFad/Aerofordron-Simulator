using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenOptions : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI resolutionText;
    [SerializeField] TextMeshProUGUI refreshRateText;
    [SerializeField] TextMeshProUGUI screenModeText;
    [SerializeField] Button applyButton;
    private int currentResolutionOption;
    private int currentScreenModeOption;
    private int currentRefreshRateOption;
    private String[] screenModeOptions;
    private String[] resolutionOptions;
    [SerializeField] private int[] refreshRate;
    private bool isConfigSaved;
    private int[] playerPrefOptions;
    private int[] currentPlayerOptions;

    // Start is called before the first frame update
    void Start()
    {
        currentPlayerOptions = new int[3];
        ComproveIfConfigChanged();
        playerPrefOptions = GetCurrentPlayerPrefsOptions();
        screenModeOptions = new string[2];
        screenModeOptions[0] = "On";
        screenModeOptions[1] = "Off";
        resolutionOptions = new string[Screen.resolutions.Length];
        AddResolutionsToArray();
        currentResolutionOption = 0;
    }

    private void Update()
    {
        if (isConfigSaved)
        {
            applyButton.interactable = false;
        }
        else
        {
            applyButton.interactable = true;
        }
    }
    public void AddResolutionsToArray()
    {
        Resolution[] resolutions = Screen.resolutions;
        int counter = 0;
        foreach (Resolution resolution in resolutions)
        {
            string option = $"{resolution.width} x {resolution.height}";
            resolutionOptions[counter] = option;
            counter++;
        }
    }
    public void ChangeResolutionOption(int direction)
    {
        currentResolutionOption = ChangeOption(direction,currentResolutionOption,resolutionOptions.Length);
        currentPlayerOptions[0] = currentResolutionOption;
        resolutionText.text = resolutionOptions[currentResolutionOption];
        ComproveIfConfigChanged();
        SetResolution(currentResolutionOption);
    }

    public void ChangeRefreshRate(int direction)
    {
        currentRefreshRateOption = ChangeOption(direction, currentRefreshRateOption, refreshRate.Length);
        currentPlayerOptions[1] = currentRefreshRateOption;
        refreshRateText.text = refreshRate[currentRefreshRateOption] + " HZ";
        ComproveIfConfigChanged();

    }
    //Direction es -1 o 1 dependiendo si tira para alante en la array o al reves
    private int ChangeOption(int direction, int currentOption, int maxArrayIndex)
    {
        if (currentOption  + direction >= 0 && currentOption + direction < maxArrayIndex)
        {
            currentOption += direction;

        }
        else if (currentOption + direction < 0)
        {
            currentOption = maxArrayIndex - 1;
        }
        else if (currentOption + direction > maxArrayIndex -1)
        {
            currentOption = 0;
        }
        return currentOption;

    }

    public void SetResolution(int index)
    {

        Resolution[] resolutions = Screen.resolutions;
        Resolution selectedResolution = resolutions[index];
        PlayerPrefs.SetInt("currentResolutionOption", index);
        PlayerPrefs.SetInt("ResolutionWidth", selectedResolution.width);
        PlayerPrefs.SetInt("ResolutionHeight", selectedResolution.height);

    }

    public void SetRefreshRate(int index)
    {
        PlayerPrefs.SetInt("currentRefreshRateOption", index);
        PlayerPrefs.SetInt("RefreshRate", refreshRate[index]);
    }

    public void ChangeScreenModeOption(int direction)
    {
        currentScreenModeOption = ChangeOption(direction, currentScreenModeOption, screenModeOptions.Length);
        currentPlayerOptions[2] = currentScreenModeOption;
        ComproveIfConfigChanged();
        screenModeText.text = screenModeOptions[currentScreenModeOption];
        if (currentScreenModeOption == 0)
        {
            SetScreenMode(0);
        }
        else
        {
            SetScreenMode(1);
        }
    }

    public void SetScreenMode(int fullscreen)
    {
        PlayerPrefs.SetInt("currentScreenModeOption", fullscreen);
        if (fullscreen == 0)
        {
            PlayerPrefs.SetInt("Fullscreen", 0);
        }
        else
        {
            PlayerPrefs.SetInt("Fullscreen", 1);
        }
    }

    public void ApplyOptions()
    {
        if (PlayerPrefs.GetInt("Fullscreen") == 0)
        {
            Screen.SetResolution(PlayerPrefs.GetInt("ResolutionWidth"), PlayerPrefs.GetInt("ResolutionHeight"), Screen.fullScreen);
        }
        else
        {
            Screen.SetResolution(PlayerPrefs.GetInt("ResolutionWidth"), PlayerPrefs.GetInt("ResolutionHeight"), FullScreenMode.Windowed);
        }
        Application.targetFrameRate = PlayerPrefs.GetInt("RefreshRate");
        playerPrefOptions = GetCurrentPlayerPrefsOptions();
    }

    public int[] GetCurrentPlayerPrefsOptions()
    {
        int[] aux = new int[3];
        if (PlayerPrefs.HasKey("currentResolutionOption"))
        {
            aux[0] = PlayerPrefs.GetInt("currentResolutionOption");

        }
        if (PlayerPrefs.HasKey("currentRefreshRateOption"))
        {
            aux[1] = PlayerPrefs.GetInt("currentRefreshRateOption");

        }
        if (PlayerPrefs.HasKey("currentScreenModeOption"))
        {
            aux[2] = PlayerPrefs.GetInt("currentScreenModeOption");

        }
        return aux;
    }

    public void ComproveIfConfigChanged()
    {
        isConfigSaved = true;
        for (int i = 0; i < playerPrefOptions.Length; i++)
        {
            if (playerPrefOptions[i] != currentPlayerOptions[i])
            {
                isConfigSaved = false;
            }
        }
    }

    public void LoadDefaultConfigToPlayerPrefs()
    {

    }

}
