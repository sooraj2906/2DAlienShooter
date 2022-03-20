using TMPro;
using UnityEngine;

// <summary>
/// UiManager class manages all the UI updates
/// </summary>
public class UiManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text enemyCountText;
    public TMP_Text highScoreText;
    public GameObject gameOverScreen;

    private void Start()
    {
        GameManager.PInstance.onScoreUpdated += UpdateScore;
        GameManager.PInstance.onGameEnd += GameEnd;
        GameManager.PInstance.onGameStart += GameStart;
        GameManager.PInstance.onEnemiesToDefeatUpdated += UpdateEnemyDefeatedCount;

        UpdateEnemyDefeatedCount(GameManager.PInstance.GetEnemiesToDefeatCount());
    }

    // <summary>
    /// Updates the score on the UI
    /// </summary>
    private void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    // <summary>
    /// Updates the remaining enemy count on the UI
    /// </summary>
    private void UpdateEnemyDefeatedCount(int enemyCount)
    {
        enemyCountText.text = enemyCount.ToString();
    }

    // <summary>
    /// Shows the game over screen and set the highscore on the UI
    /// </summary>
    private void GameEnd()
    {
        gameOverScreen.SetActive(true);
        highScoreText.text = GameManager.PInstance.GetHighScore().ToString();
    }

    // <summary>
    /// Resets the score and enemy count on restart
    /// </summary>
    private void GameStart()
    {
        if (gameOverScreen.activeInHierarchy) gameOverScreen.SetActive(false);
        UpdateScore(GameManager.PInstance.GetScore());
        UpdateEnemyDefeatedCount(GameManager.PInstance.GetEnemiesToDefeatCount());
    }


    private void OnDestroy()
    {
        GameManager.PInstance.onScoreUpdated -= UpdateScore;
        GameManager.PInstance.onGameEnd -= GameEnd;
    }
}