using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuFunctionality : MonoBehaviour
{
    [SerializeField] string LevelName;

    public void PlayGame()
    {
        SceneManager.LoadScene(LevelName);
    }


}
