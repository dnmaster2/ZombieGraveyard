using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject controlPanel;
    bool isOpen;

    public void Play()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void Menu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Controls()
    {
        if(controlPanel != null)
        {
            controlPanel.SetActive(!controlPanel.activeInHierarchy);
        }
    }
}
