using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public void Quit() => Application.Quit();
    public void LoadNextScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    public void StartAgain()
    {
        
        SceneManager.LoadScene(0);
        FindObjectOfType<GameStatus>().Reset();
    }
}
