using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeleccionPanel : MonoBehaviour
{
    public void StopTime()
    {
        Cursor.visible = true;
        print("SeleccionPanelPause");
        Time.timeScale = 0;
    }
    public void ResetTime()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
    }
}
