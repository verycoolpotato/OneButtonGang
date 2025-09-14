using UnityEngine;
using UnityEngine.SceneManagement;
public class GameEnd : MonoBehaviour
{
    [Tooltip("Name of the scene used for gameplay")]
    [SerializeField] string GameplaySceneName;

    [Tooltip("Name of the scene used for MainMenu")]
    [SerializeField] string MenuSceneName;

    [SerializeField] GameObject EndMenuObj;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            EndMenuObj.SetActive(true);
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
