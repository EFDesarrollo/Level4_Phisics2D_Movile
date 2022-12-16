using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private TimeManager timeManager;
    private float Timer;
    [HideInInspector] public float PlayerPoints = 0;
    private float MaxPointsReached = 0;

    [SerializeField] private bool endGame = false;
    [SerializeField] private float gameTime = 60;
    [SerializeField] private TextMeshProUGUI timerUI;
    [SerializeField] private GameObject endGameMenu;
    [SerializeField] private TextMeshProUGUI pointsUI;
    [SerializeField] private TextMeshProUGUI recordPointsTextUI;
    [SerializeField] private string[] congratulationsMessages;
    [SerializeField] private TextMeshProUGUI congratulationsTextUI;
    // Start is called before the first frame update
    void Start()
    {
        timeManager = Camera.main.GetComponent<TimeManager>();
        Timer = gameTime;
        MaxPointsReached = PlayerPrefs.GetFloat("HigScore");
        PrintCongratulations();
    }

    // Update is called once per frame
    void Update()
    {
        if (endGame)
            return;
        if (Timer <= 0)
        {
            timeManager.PauseTime();
            endGameMenu.SetActive(true);
            PrintPoints();
            PrintCongratulations();
            endGame = true;
        }
        Timer -= Time.deltaTime;
        PrintTimer();
    }
    void PrintTimer()
    {
        int minutes = (int)(Timer / 60);
        int seconds = (int)(Timer % 60);
        timerUI.text = (minutes < 10 ? "0" : "") +minutes + ":" + (seconds < 10 ? "0" : "") + seconds;
    }
    void PrintPoints()
    {
        pointsUI.text = PlayerPoints.ToString();
        if (PlayerPoints > MaxPointsReached)
        {
            MaxPointsReached = PlayerPoints;
            PlayerPrefs.SetFloat("HigScore", PlayerPoints);
        }
        recordPointsTextUI.text = MaxPointsReached.ToString();
    }
    void PrintCongratulations()
    {
        int messageID = Random.Range(0, congratulationsMessages.Length);
        congratulationsTextUI.text = congratulationsMessages[messageID];
    }
    public void OnClickResetScene()
    {
        Timer = Time.time + gameTime;
        endGame = false;
        GetComponent<SceneLoader>().ResetScene();
    }
    public void OnClickGoToMainMenu()
    {
        GetComponent<SceneLoader>().LoadScene("MainMenu");
    }
}
