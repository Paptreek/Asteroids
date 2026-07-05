using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverPanel;

    [SerializeField] private GameManager _gameManager;

    [SerializeField] private TMP_Text _gameOverText;
    [SerializeField] private TMP_Text _totalTimeText;
    [SerializeField] private TMP_Text _asteroidsDestroyedText;
    [SerializeField] private TMP_Text _shipsDestroyedText;
    [SerializeField] private TMP_Text _livesLeftText;
    [SerializeField] private TMP_Text _bossScoreText;
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
        DisplayGameOverText();
        DisplayTotalTime();
        DisplayAsteroidsDestroyed();
        DisplayShipsDestroyed();
        DisplayLivesLeft();
        DisplayBossScore();
        DisplayFinalScore();

        Time.timeScale = 0;
    }

    private void StartNewGame()
    {
        SceneManager.LoadScene("Game");
    }

    private void QuitGame()
    {
        SceneManager.LoadScene("Title");
    }

    private void DisplayGameOverText()
    {
        if (_gameManager.PlayerHasWon)
        {
            _gameOverText.text = $"VICTORY";
        }
        else
        {
            _gameOverText.text = $"DEFEAT";
        }
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

    private void DisplayLivesLeft()
    {
        int lifeCount = _gameManager.GetPlayerLifeCount();
        int pointsFromLives = _gameManager.GetBonusLivesScore();

        _livesLeftText.text = $"lives left: {lifeCount:0}\t\t=\t{pointsFromLives:0000} pts";
    }

    private void DisplayBossScore()
    {
        int bossPartsDestroyed = _gameManager.GetBossPartsDestroyedCount();
        int pointsFromBoss = _gameManager.GetPointsFromBoss();

        _bossScoreText.text = $"boss parts: {bossPartsDestroyed}\t=\t{pointsFromBoss:0000} pts";
    }

    private void DisplayFinalScore()
    {
        int finalScore = _gameManager.Score;

        _totalScoreText.text = $"FINAL: {finalScore:00000}";
    }
}
