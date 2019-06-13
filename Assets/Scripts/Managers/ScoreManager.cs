using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    private float multiplierCooldown = 10f;

    public static int score;
    public static int waves;
    public int multiplier = 1;
    public int sceneIndex;

    static ScoreManager mInstance;

    Text scoreText;
    Text multiplierText;
    Text restartSceneText;
    Text wavesText;
    Text levelNumberText;
    Text gameOverText;
    Text levelCompleteText;

    Image friendsImage;
    Image multiplierEnemyImage;

    //public Image stHealthImg;

    //public CompletePlayerController player;


    public static ScoreManager Instance
    {
        get
        {
            if (mInstance == null)
            {
                GameObject go = new GameObject();
                mInstance = go.AddComponent<ScoreManager>();
            }
            return mInstance;
        }
    }

    void Start()
    {
        GameObject scoreTextUI = GameObject.Find("ScoreText");
        scoreText = scoreTextUI.GetComponent<Text>();

        GameObject multiplierTextUI = GameObject.Find("MultiplierText");
        multiplierText = multiplierTextUI.GetComponent<Text>();

        //GameObject wavesTextUI = GameObject.Find("Waves");
        //wavesText = wavesTextUI.GetComponent<Text>();

        GameObject restartSceneTextUI = GameObject.Find("RestartSceneText");
        restartSceneText = restartSceneTextUI.GetComponent<Text>();

        GameObject levelCompleteTextUI = GameObject.Find("LevelCompleteText");
        levelCompleteText = levelCompleteTextUI.GetComponent<Text>();

        GameObject gameOverTextUI = GameObject.Find("GameOverText");
        gameOverText = gameOverTextUI.GetComponent<Text>();

        GameObject levelNumberTextUI = GameObject.Find("LevelNumberText");
        levelNumberText = levelNumberTextUI.GetComponent<Text>();
        sceneIndex = SceneManager.GetActiveScene().buildIndex;



        //GameObject stHealthImgUI = GameObject.Find("stHealthImage");
        //stHealthImg = stHealthImgUI.GetComponent<Image>();

        GameObject friendsImageUI = GameObject.Find("FriendsImage");
        friendsImage = friendsImageUI.GetComponent<Image>();

        GameObject multiplierEnemyImageUI = GameObject.Find("MultiplierEnemyImage");
        multiplierEnemyImage = multiplierEnemyImageUI.GetComponent<Image>();
        // Reset the score.

        score = 0;

        StopBlinking();
    }

    void Update()
    {
        levelNumberText.text = "Level " + sceneIndex;
    }

    public void AddScore(int enemyScore)
    {
        if (scoreText == null) { return; }
        score += enemyScore * multiplier;
        scoreText.text = "Score: " + score.ToString();
    }

    public void StartBlinking()
    {
        StopAllCoroutines();
        StartCoroutine("Blink");

    }
    /*  public void WaveCounter(int waveCount)
    {
        waves += waveCount;
        wavesText.text = "Wave:" + waves.ToString();
    }
    */
    public void ResetSceneUI()
    {
        restartSceneText.text = "Press ESC to restart";
    }

    public void GameOverUI()
    {
        gameOverText.text = "Game Over";
    }

    public void WinUI()
    {
        levelCompleteText.text = "Level " + sceneIndex + " Complete!";
    }

    public void AddMultiplier()
    {
        if (multiplier <= 1)
        {
            multiplier = 0;
        }
        multiplier += 2;
        multiplierText.text = "Score X " + multiplier.ToString();
        StartCoroutine("timer");
    }

    private void RemoveMultiplier()
    {
        multiplier -= 2;

        if (multiplier > 0)
        {
            multiplierText.text = "Score X " + multiplier.ToString();
        }
        else
        {
            multiplierText.text = "";
            multiplier = 1;
        }
    }

    public void StopBlinking()
    {
        StopAllCoroutines();
        friendsImage.color = new Color(friendsImage.color.r, friendsImage.color.g, friendsImage.color.b, 0);
        multiplierEnemyImage.color = new Color(multiplierEnemyImage.color.r, multiplierEnemyImage.color.g, multiplierEnemyImage.color.b, 0);
    }

    private IEnumerator timer()
    {
        yield return new WaitForSeconds(multiplierCooldown);
        RemoveMultiplier();
    }

    IEnumerator Blink()
    {
        while (true)
        {
            switch (friendsImage.color.a.ToString())
            {
                case "0":
                    friendsImage.color = new Color(friendsImage.color.r, friendsImage.color.g, friendsImage.color.b, 1);
                    multiplierEnemyImage.color = new Color(multiplierEnemyImage.color.r, multiplierEnemyImage.color.g, multiplierEnemyImage.color.b, 1);
                    //Play sound
                    yield return new WaitForSeconds(0.5f);
                    break;
                case "1":
                    friendsImage.color = new Color(friendsImage.color.r, friendsImage.color.g, friendsImage.color.b, 0);
                    multiplierEnemyImage.color = new Color(multiplierEnemyImage.color.r, multiplierEnemyImage.color.g, multiplierEnemyImage.color.b, 0);
                    //Play sound
                    yield return new WaitForSeconds(0.5f);
                    break;
            }
        }
    }


}
