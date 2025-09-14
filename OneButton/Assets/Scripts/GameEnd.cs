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
    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            EndMenuObj.SetActive(true);

            int score = scoreManager.GetScore();
            float percent = scoreManager.GetDestructionPercent();

            ScoreDisplay.text = score.ToString() + " Points";
            PercentDisplay.text = percent.ToString("F0") + "% Damage";

            int MedalRanking = score * (int)percent / 10;


            Debug.Log(MedalRanking.ToString());

            switch (MedalRanking)
            {
                case < 15000:
                    MedalSpriteRenderer.sprite = MedalSprites[4];
                    break;
                case >= 15000 and < 20000:
                    MedalSpriteRenderer.sprite = MedalSprites[3];
                    break;
                case >= 20000 and < 25000:
                    MedalSpriteRenderer.sprite = MedalSprites[2];
                    break;
                case >= 25000 and < 30000:
                    MedalSpriteRenderer.sprite = MedalSprites[1];
                    break;
                case >= 30000:
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
        //hardcoded since there is only one gameplay scene
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
