using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UI_Interaction : MonoBehaviour
{
    public GameObject ui_canvas;
    GraphicRaycaster ui_raycaster;
    PointerEventData click_data;
    List<RaycastResult> click_result;
    public GameObject pausePanel;
    public Button pauseButton;
    public Button continueButton;
    public Slider musicSlider;
    public Slider sfxSlider;
    public int enemyKilled;
    public GameObject gameoverPanel;
    public TextMeshProUGUI currentScoreUI;
    public TextMeshProUGUI highestScoreUI;
    public Button restartButton;
    public TextMeshProUGUI score;
    public static UI_Interaction Instance { get; private set; }
    private void Awake()
    {
        pausePanel.SetActive(false);
        gameoverPanel.SetActive(false);
        if (Instance == null)
        {
            Instance = this;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        ui_raycaster = ui_canvas.GetComponent<GraphicRaycaster>();
        click_data = new PointerEventData(EventSystem.current);
        click_result = new List<RaycastResult>();
        pauseButton.onClick.AddListener(DisplayPausePanel);
        continueButton.onClick.AddListener(DisablePausePanel);
        restartButton.onClick.AddListener(RestartGame);
        enemyKilled = 0;
    }

    private void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void DisableGameComponent(bool b)
    {
        GameObject.FindWithTag("Player").GetComponent<PlayerController>().enabled = b;
        GameObject.FindWithTag("Manager").GetComponent<InputController>().enabled = b;
    }
    private void DisablePausePanel()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
        DisableGameComponent(true);
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    private void DisplayPausePanel()
    {
        pausePanel.SetActive(true);
        DisableGameComponent(false);
        Time.timeScale = 0;
    }
    public void DisplayGameOverPanel()
    {
        gameoverPanel.SetActive(true);
        currentScoreUI.text = "Your Score: " + enemyKilled;

        DisableGameComponent(false);
        if (PlayerPrefs.HasKey("HighestScore"))
        {
            int highestScore = PlayerPrefs.GetInt("HighestScore");
            if (enemyKilled > highestScore)
            {
                PlayerPrefs.SetInt("HighestScore", enemyKilled);
            }
            highestScoreUI.text = "Highest Score: " + PlayerPrefs.GetInt("HighestScore");
        }
        else
        {
            PlayerPrefs.SetInt("HighestScore", enemyKilled);
            highestScoreUI.text = "Highest Score: " + enemyKilled;
        }

    }


    // Update is called once per frame
    void Update()
    {
       
        score.text = "Enemy Killed: " + enemyKilled;
    }

    private void GetUIElementClicked()
    {
        click_data.position = Mouse.current.position.ReadValue();
        click_result.Clear();

        ui_raycaster.Raycast(click_data, click_result);

        foreach(RaycastResult item in click_result)
        {
            GameObject ui_element = item.gameObject;
         
        }
    }
}
