using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject panelMainMenu;
    public GameObject panelCredits;
    public string nameScene;

    public void Play(){
        SceneManager.LoadScene(nameScene);
    }

    public void OpenCredits(){
        panelMainMenu.SetActive(false);
        panelCredits.SetActive(true);
    }

    public void CloseCredits(){
        panelCredits.SetActive(false);
        panelMainMenu.SetActive(true);
    }

    public void Exit(){
        Application.Quit();
    }
}
