using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject hazard;
    public GUIText guiText;
    public float xmin, xmax;
    public float decrement;
    public int hazardCount;
    public int startWait;
    public float spawnWait;
    public int waveWait;
    private int score;
    private int totalScore;

    private bool gameOver;
    private bool restart;
    public GUIText gameOverText;
    public GUIText restartText;

    void Start ()
    {
        score = 0;
        gameOverText.text = "";
        restartText.text = "";
        totalScore = 0;
        gameOver = false;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    void Update()
    {
        if (restart && Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(xmin, xmax), 0.0f, 16.0f);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                totalScore += 10;
                yield return new WaitForSeconds(spawnWait);

                if (gameOver)
                {
                    yield return new WaitForSeconds(startWait);
                    restartText.text = "Press 'R' to restart...";
                    restart = true;
                    break;
                }
            }
            spawnWait = Mathf.Max(spawnWait - decrement, 0.25f);
            yield return new WaitForSeconds(waveWait);
        }
    }

    void UpdateScore()
    {
        guiText.text = "Score: " + score + "/" + totalScore;
    }

    public void AddScore(int newScore)
    {
        score += newScore;
        UpdateScore();
    }

    public void GameOver()
    {
        gameOver = true;
        gameOverText.text = "Oops you messed up";
    }
}
