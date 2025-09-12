using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public void AutoDestroySelf()
    {
        Debug.Log("AutoDestroySelf was called");
        Destroy(this.gameObject);
    }

}
