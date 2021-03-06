using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScreens : MonoBehaviour
{
    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject Options;
    [SerializeField] GameObject Credits;
    float time = 0;
    bool startTimer;
    bool change_MainToOptions,change_OptionsToMain, change_MainToCredits,change_CreditsToMain;
    
    public void StartMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void Update()
    {
        if(startTimer)
        {
            time += Time.deltaTime;
            Debug.Log(time);
            if (change_MainToOptions && time >= 2f)
            {
                Options.SetActive(true);
                change_MainToOptions = false;
                startTimer = false;
                time = 0f;
            }
            if (change_OptionsToMain && time >= 2f)
            {
                MainMenu.SetActive(true);
                change_OptionsToMain = false;
                startTimer = false;
                time = 0f;
            }
            if (change_MainToCredits && time >= 2f)
            {
                Credits.SetActive(true);
                change_MainToCredits = false;
                startTimer = false;
                time = 0f;
            }
            if (change_CreditsToMain && time >= 2f)
            {
                MainMenu.SetActive(true);
                change_CreditsToMain = false;
                startTimer = false;
                time = 0f;
            }
        }

    }
    public void Change_MainToOptions()
    {
        MainMenu.SetActive(false);
        startTimer = true;
        change_MainToOptions = true;
    }
    public void Change_OptionsToMain()
    {
        Options.SetActive(false);
        startTimer = true;
        change_OptionsToMain = true;
    }
    public void Change_MainToCredits()
    {
        MainMenu.SetActive(false);
        change_MainToCredits = true;
        startTimer = true;
    }
    public void Change_CreditsToMain()
    {
        Credits.SetActive(false);
        change_CreditsToMain = true;
        startTimer = true;
    }
    public void ExitGame()
    {
        Application.Quit();
    }


}
