
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private List<Destructible> _totalDestructibles;
    private float _invTotalDestructibles;

    public static ScoreManager instance;

    private void SingletonSetup()
    {

    }

    private void Awake()
    {
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

   
}

