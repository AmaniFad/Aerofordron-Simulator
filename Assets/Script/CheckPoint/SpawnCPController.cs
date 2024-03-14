using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnCPController : MonoBehaviour
{
    private List<GameObject> cpList = new List<GameObject>();
    public static SpawnCPController Instance;

    [SerializeField] private TMP_Text cpTotal_Text;

    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

   public void AddCP(GameObject cp)
   {
        cpList.Add(cp);
        cpTotal_Text.text = "/" + cpList.Count;
   }
}
