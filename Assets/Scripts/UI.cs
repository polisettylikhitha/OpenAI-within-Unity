using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadStory()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadModerationAPI()
    {
        SceneManager.LoadScene(2);
    }
}
