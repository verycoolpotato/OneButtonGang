using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameEnd : MonoBehaviour
{
    [Tooltip("Name of the scene used for gameplay")]
    [SerializeField] string GameplaySceneName;

    [Tooltip("Name of the scene used for MainMenu")]
    [SerializeField] string MenuSceneName;

    [SerializeField] GameObject EndMenuObj;

    [SerializeField] TextMeshProUGUI ScoreDisplay;
    [SerializeField] TextMeshProUGUI PercentDisplay;

    [SerializeField] Sprite[] MedalSprites;

    [SerializeField] SpriteRenderer MedalSpriteRenderer;

    private ScoreManager scoreManager;

    [SerializeField] AudioLowPassFilter MusicLowPass;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            EndMenuObj.SetActive(true);

            MusicLowPass.cutoffFrequency = 500f;
            

            int score = scoreManager.GetScore();
            float percent = scoreManager.GetDestructionPercent();

            ScoreDisplay.text = score.ToString() + " Points";
            PercentDisplay.text = percent.ToString("F0") + "% Damage";

            int MedalRanking = score * (int)percent / 100;


          

            switch (MedalRanking)
            {
                case < 1000:
                    MedalSpriteRenderer.sprite = MedalSprites[4];
                    break;
                case >= 1000 and < 3500:
                    MedalSpriteRenderer.sprite = MedalSprites[3];
                    break;
                case >= 3500 and < 6000:
                    MedalSpriteRenderer.sprite = MedalSprites[2];
                    break;
                case >= 6000 and < 8500:
                    MedalSpriteRenderer.sprite = MedalSprites[1];
                    break;
                case >= 8500:
                    MedalSpriteRenderer.sprite = MedalSprites[0];
                    break;
            }

        }
    }
   
    private void Awake()
    {
        scoreManager = ScoreManager.Instance;
        

    }
   
    //Called by buttons
    public void ReturnToMenu()
    {
        SaveScore();
        SceneManager.LoadScene(MenuSceneName);
    }

   
    public void RetryGame()
    {
        SaveScore();
      
        SceneManager.LoadScene(GameplaySceneName);
    }

    //Close Game
    public void QuitGame()
    {
        SaveScore();
        Application.Quit();
    }
    private void SaveScore()
    {
        ScoreManager.Instance.SaveScore();
    }
}
