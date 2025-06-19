using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnCPController : MonoBehaviour
{
    private List<GameObject> cpList;
    public static SpawnCPController Instance;
    private int totalScore;
    [SerializeField] private TMP_Text cpTotal_Text;
    private void Awake()
    {
        cpList = new List<GameObject>();
        if (Instance == null)
        {
            Instance = this;
        }
        totalScore = 0;
    }

    private void OnEnable()
    {
        ScoreController.OnUpdateScore += UpdateScoreDisplay;
    }

    private void UpdateScoreDisplay(int score)
    {
        totalScore += score;
        cpTotal_Text.text = totalScore + "/" + cpList.Count;

    }
    private void Update()
    {

    }
    void Start()
    {

    }
    public int GetCpTotal()
    {
        return cpList.Count;
    }

   public void AddCP(GameObject cp)
   {
        cpList.Add(cp);
        cpTotal_Text.text = "/" + cpList.Count;
   }
}
