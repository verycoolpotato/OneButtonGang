
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private List<Destructible> _totalDestructibles;
    private float _invTotalDestructibles;

    [SerializeField] private int Score;

    public static ScoreManager Instance;
    [SerializeField] private TextMeshProUGUI InGameScoreText;
    private void SingletonSetup()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        Score = 0;
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
        UpdateScore(score);
    }
   
    public void SaveScore()
    {
        //Save score locally
    }

    public int GetScore()
    {
        return Score;
    }

    private void UpdateScore(int addedscore)
    {
        if (addedscore > 30 && addedscore < 50)
        {
            StartCoroutine(AnimateText(Color.green));
        }
        else if(addedscore >= 50)
        {
            StartCoroutine(AnimateText(Color.red));

        }
        else
        {
            StartCoroutine(AnimateText(Color.yellow));
        }
            InGameScoreText.text = Score.ToString();
    }

    IEnumerator AnimateText(Color color)
    {
        InGameScoreText.color =  color;
        yield return new WaitForSeconds(0.2f);
        InGameScoreText.color = Color.white;

    }
}

