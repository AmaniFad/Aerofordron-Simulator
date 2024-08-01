using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DontIncludeImageInPostProcess : MonoBehaviour
{
    [ImageEffectOpaque] Image imagen;
    // Start is called before the first frame update
    void Start()
    {
        imagen = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
