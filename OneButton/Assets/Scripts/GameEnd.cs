using TMPro;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            EndMenuObj.SetActive(true);

            
            ScoreDisplay.text = ScoreManager.Instance.GetScore().ToString() + " Points";
            PercentDisplay.text = ScoreManager.Instance.GetDestructionPercent().ToString("F0") + "% Damage";

        }
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
