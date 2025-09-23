using UnityEngine;

public class ButtonToMeteoComunicator : MonoBehaviour
{

    public void ToggleFog()
    {
        MeteoModes.instance.ToggleFog();
    }

    public void ToggleRain()
    {
        MeteoModes.instance.ToggleRain();

    }


    public void ToggleClouds()
    {
        MeteoModes.instance.ToggleClouds();
    }

    public void SetDay()
    {
        MeteoModes.instance.DayMode();
    }

    public void SetNight()
    {

        MeteoModes.instance.NightMode();
    }
}

