using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeleccionPanel : MonoBehaviour
{
    public void StopTime()
    {
        Cursor.visible = true;
    }
    public void ResetTime()
    {
        Cursor.visible = false;
    }
}
