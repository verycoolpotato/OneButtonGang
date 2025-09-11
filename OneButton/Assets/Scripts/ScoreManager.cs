
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private List<Destructible> _totalDestructibles;
    private float _invTotalDestructibles;

    private int Score;

    public static ScoreManager Instance;

    private void SingletonSetup()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void Awake()
    {
        SingletonSetup();
        _totalDestructibles = new List<Destructible>(
            Object.FindObjectsByType<Destructible>(FindObjectsSortMode.None));

        _invTotalDestructibles = 100f / _totalDestructibles.Count;
    }

    public float GetDestructionPercent()
    {
        List<Destructible> endDestructibleCount = new List<Destructible>(
            Object.FindObjectsByType<Destructible>(FindObjectsSortMode.None));

        float destructionPercentage =
            (_totalDestructibles.Count - endDestructibleCount.Count) * _invTotalDestructibles;

        return destructionPercentage; 
    }
    public void AddScore(int score)
    {
        Score += score;
    }
   
    public void SaveScore()
    {
        //Save score locally
    }
}

