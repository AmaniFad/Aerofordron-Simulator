using UnityEngine;
using System.Collections;
using UnityEngine.Localization.Settings;

public class SelectLlenguage : MonoBehaviour
{
    private bool active = false;
    private void Start()
    {
        int ID = PlayerPrefs.GetInt("LocaleLenguaje", 0);
        ChangeLocale(ID);
    }
    public void ChangeLocale(int localeID)
    {
        if (active== true)
          return;
        
        StartCoroutine(SetLocale(localeID));
    }
    IEnumerator SetLocale(int _localeID)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];
        PlayerPrefs.SetInt("LocaleLenguaje", _localeID);
        active = false;
    }
}
