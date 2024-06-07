using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUtils : MonoBehaviour
{

    public void StopPause()
    {
        Cursor.visible = false;
        Time.timeScale = 1f;
        PlayerInputController.Instance.HasPaused();
    }
}
