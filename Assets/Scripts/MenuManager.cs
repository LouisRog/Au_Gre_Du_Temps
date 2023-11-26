using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartExp()
    {
        SceneManager.LoadScene("Intro");
    }
    public void QuitGame()
    {
        Debug.Log("QuitGame");
        Application.Quit();
    }
}
