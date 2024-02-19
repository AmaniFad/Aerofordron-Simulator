using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOptions : MonoBehaviour
{
    // Start is called before the first frame update
    
    public void StartNewScene(string scene)
    {
        SceneLoader.Instance.SceneLoad(scene);
    }



    public void ExitGame()
    {
        Application.Quit();
    }
}
