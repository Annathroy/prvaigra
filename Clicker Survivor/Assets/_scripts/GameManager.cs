using TMPro;
using UnityEditor.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Buildings;
    public GameObject StartGamePanel;
    public GameObject StartGame;
    public GameObject optionsButton;
    public GameObject OptionsPanel;
    public GameObject PauseGamePanel;
    public GameObject GameOverPanel;
    public GameObject MainMenuPanel;
    public static GameManager Instance;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {

    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !MainMenuPanel.activeSelf && !GameOverPanel.activeSelf && !OptionsPanel.activeSelf)
        {
            PauseGame();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            optionsButton.SetActive(true);
            MainMenuPanel.SetActive(true);
            StopAllCoroutines();
            ExitGame();
            
        }
    }
    public int RandomNumber(int index)
    {

        return Random.Range(0, index);
    }

    public void PlayGame()
    {
        Buildings.SetActive(true);
        Time.timeScale = 1f;
        optionsButton.SetActive(false);
        GameOverPanel.SetActive(false);
        MainMenuPanel.SetActive(false);
        StartGame.SetActive(true);
        StartGamePanel.SetActive(true);

    }
    public void ExitGame()
    {
        MainMenuPanel.SetActive(true);
        GameOverPanel.SetActive(false);
        StopAllCoroutines();
        StartGamePanel.SetActive(false );
        OptionsPanel.SetActive(false);
        Buildings.SetActive(false);
        Debug.Log("Exit Game");
    }

    private void PauseGame()
    {

        if (Time.timeScale == 1f)
        {
            Time.timeScale = 0f;
            Debug.Log("Game Paused");
            PauseGamePanel.SetActive(true);

        }
        else if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            Debug.Log("Game Resumed");
            PauseGamePanel.SetActive(false);
        }
    }
    public void OptionsButton()
    {
        Buildings.SetActive(false);
        optionsButton.SetActive(false);
        MainMenuPanel.SetActive(false);
        PauseGamePanel.SetActive(false);
        OptionsPanel.SetActive(true);
    }
}


