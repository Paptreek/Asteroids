using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverPanel;

    [SerializeField] private GameManager _gameManager;

    [SerializeField] private TMP_Text _totalTimeText;
    [SerializeField] private TMP_Text _asteroidsDestroyedText;
    [SerializeField] private TMP_Text _shipsDestroyedText;
    [SerializeField] private TMP_Text _totalDeathsText;
    [SerializeField] private TMP_Text _totalScoreText;

    [SerializeField] private Button _playButton;
    [SerializeField] private Button _quitButton;

    private void Awake()
    {
        _gameOverPanel.SetActive(true);

        _playButton.onClick.AddListener(StartNewGame);
        _quitButton.onClick.AddListener(QuitGame);
    }

    private void Start()
    {
        DisplayTotalTime();
        DisplayAsteroidsDestroyed();
        DisplayShipsDestroyed();
        DisplayDeaths();
        DisplayFinalScore();

        Time.timeScale = 0;
    }

    private void Update() // maybe move everything to Start() once testing is done
    {
        //DisplayTotalTime();
        //DisplayAsteroidsDestroyed();
        //DisplayShipsDestroyed();
        //DisplayDeaths();
        //DisplayFinalScore();
    }

    private void StartNewGame()
    {
        SceneManager.LoadScene("Game");
    }

    private void QuitGame()
    {
        SceneManager.LoadScene("Title");
    }

    private void DisplayTotalTime()
    {
        TimeSpan totalTime = TimeSpan.FromSeconds(_gameManager.TotalPlayTime);
        string minutes = totalTime.ToString("mm");
        string seconds = totalTime.ToString("ss");

        int bonusScore = _gameManager.BonusTimeScore;

        _totalTimeText.text = $"time: {minutes}m {seconds}s\t=\t{bonusScore:0000} pts";
    }

    private void DisplayAsteroidsDestroyed()
    {
        int destroyedAsteroids = _gameManager.GetDestroyedAsteroidCount();
        int pointsFromAsteroids = _gameManager.PointsFromAsteroids;

        _asteroidsDestroyedText.text = $"asteroids: {destroyedAsteroids:000}\t=\t{pointsFromAsteroids:0000} pts";
    }

    private void DisplayShipsDestroyed()
    {
        int destroyedShips = _gameManager.GetDestroyedShipCount();
        int pointsFromShips = _gameManager.PointsFromShips;

        _shipsDestroyedText.text = $"ships: {destroyedShips:00}\t\t=\t{pointsFromShips:0000} pts";
    }

    private void DisplayDeaths()
    {
        int deathCount = _gameManager.GetPlayerDeathCount();
        int pointsFromDeaths = _gameManager.GetBonusDeathScore();

        _totalDeathsText.text = $"deaths: {deathCount:00}\t\t=\t{pointsFromDeaths:0000} pts";
    }

    private void DisplayFinalScore()
    {
        int finalScore = _gameManager.Score;

        _totalScoreText.text = $"FINAL: {finalScore:00000}";
    }
}
