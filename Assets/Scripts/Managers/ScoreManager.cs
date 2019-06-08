using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private float multiplierCooldown = 10f;

    public static int score;
    public static int waves;
    public int multiplier = 1;

    static ScoreManager mInstance;

    Text scoreText;
    Text multiplierText;
    Text restartSceneText;
    Text wavesText;

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

        //GameObject stHealthImgUI = GameObject.Find("stHealthImage");
        //stHealthImg = stHealthImgUI.GetComponent<Image>();

        // Reset the score.
        score = 0;
    }

    public void AddScore(int enemyScore)
    {
        if (scoreText == null) { return; }
        score += enemyScore * multiplier;
        scoreText.text = "Score: " + score.ToString();
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

    private IEnumerator timer()
    {
        yield return new WaitForSeconds(multiplierCooldown);
        RemoveMultiplier();
    }
}
