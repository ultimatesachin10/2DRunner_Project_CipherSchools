using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _player;
  //  [SerializeField] private GameObject _playagainbutton;
    [SerializeField] private GameObject _gameoverplan;
    [SerializeField] private GameObject _camera;
    [SerializeField] private GameObject PausedMenu;
    [SerializeField] private GameObject Timeleft;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _portal;


    private void Start()
    {
        PausedMenu.SetActive(false);
    }

    private void Update()
    {
        _portal.gameObject.transform.Rotate(new Vector3(0, 0, 360) * Time.deltaTime * .40f);

        if (_player.gameObject.transform.position.y < -8)
        {
            _animator.SetBool("Death", true);
            _gameoverplan.SetActive(true);
            _camera.SetActive(false);
            Timeleft.SetActive(false);
        }
        else
        {
            _animator.SetBool("Death", false);
            _gameoverplan.SetActive(false);
            _camera.SetActive(true);
            Timeleft.SetActive(true);
        }
    }

    #region On Click Methods

    public void PlayGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayAgain()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        //SceneManager.LoadScene(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Pause()
    {
        PausedMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        PausedMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    #endregion



}
