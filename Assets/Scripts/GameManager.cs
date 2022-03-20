using System;
using UnityEngine;

/// <summary>
/// Game Manager class manages the gameflow
/// </summary>
public class GameManager : MonoBehaviour
{
    private int _score;

    [SerializeField] private int enemiesToDefeat;
    private int _enemyRemaining;
    public static GameManager PInstance { get; private set; }

    public Action onGameEnd;
    public Action onGameStart;
    public Action<int> onScoreUpdated;
    public Action<int> onEnemiesToDefeatUpdated;

    private void Awake()
    {
        if (PInstance == null)
            PInstance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        StartGame();
    }

    /// <summary>
    /// Resets the score and invoke onStart on other classes
    /// </summary>
    public void StartGame()
    {
        _enemyRemaining = enemiesToDefeat;
        _score = 0;
        UpdateScore(_score);
        onGameStart?.Invoke();
    }

    /// <summary>
    /// Sets the high score and invokes onGameEnd on other classes
    /// </summary>
    public void OnPlayerDestroyed()
    {
        EnemyPooling.EnemyPoolInstance.DisableAllEnemies();
        MissilePooling.MissilePoolInstance.DisableAllMissiles();
        if (_score > GetHighScore()) SetHighScore(_score);

        onGameEnd?.Invoke();
    }

    /// <summary>
    /// Updates the remaining number of enemies. Checks if the required number of enemies are destroyed and invokes onGameEnd on other classes
    /// </summary>
    public void OnEnemiesDestroyed()
    {
        UpdateEnemiesToDefeat(_enemyRemaining);
        if (_enemyRemaining <= 0)
        {
            EnemyPooling.EnemyPoolInstance.DisableAllEnemies();
            MissilePooling.MissilePoolInstance.DisableAllMissiles();
            if (_score > GetHighScore()) SetHighScore(_score);
            onGameEnd?.Invoke();
        }
    }

    /// <summary>
    /// Updates the score and invokes onScoreUpdated
    /// </summary>
    /// <param name="value">the score value</param>
    public void UpdateScore(int value)
    {
        _score += value;
        onScoreUpdated?.Invoke(_score);
    }

    /// <summary>
    /// Updates the remaining number of enemies and invokes onEnemiesToDefeatUpdated
    /// </summary>
    /// <param name="value">the score value</param>
    private void UpdateEnemiesToDefeat(int value)
    {
        _enemyRemaining--;
        onEnemiesToDefeatUpdated?.Invoke(value);
    }

    /// <summary>
    /// Returns the remaining number of enemies
    /// </summary>
    public int GetEnemiesToDefeatCount()
    {
        return _enemyRemaining;
    }

    /// <summary>
    /// Returns the score
    /// </summary>
    public int GetScore()
    {
        return _score;
    }

    /// <summary>
    /// Sets the highscore
    /// </summary>
    /// /// <param name="score">the score value</param>
    public void SetHighScore(int score)
    {
        PlayerPrefs.SetInt("HighScore", score);
    }

    // <summary>
    /// Returns the highscore from PlayerPrefs
    /// </summary>
    public int GetHighScore()
    {
        return PlayerPrefs.GetInt("HighScore");
    }
}