using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeMenu : MonoBehaviour
{
    [SerializeField] private GameObject SettingMenu;

    public void Start()
    {
        SettingMenu.SetActive(false);
    }
    public void PlayGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(1);
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    public void Settings()
    {
        SettingMenu.SetActive(true);
    }

    public void Back()
    {
        SettingMenu.SetActive(false);
    }
}
