using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
public class MenuFunctionality : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    private string sfx = "SFXvol";
    private string music = "MusicVol";
    [SerializeField] string LevelName;

    public void PlayGame()
    {
        SceneManager.LoadScene(LevelName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void AdjustFXVolume(float volume)
    {
       mixer.SetFloat(sfx, volume); 
    }
    public void AdjustMusicVolume(float volume)
    {
        mixer.SetFloat(music, volume);
    }
}
