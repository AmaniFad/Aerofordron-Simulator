using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private int score;

    public static event Action<int> OnUpdateScore = delegate { };

    public void UpdateSocre()
    {
        OnUpdateScore.Invoke(score);
    }
}
