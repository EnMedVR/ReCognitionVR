using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Timers;

public class BalloonGame : MonoBehaviour
{
    public static string targetBalloon = "blue";    // game will end if any other balloon is shot
    public int gameTime = 60;

    private float spawnInterval;  // Number of seconds between balloon spawn 
    public static float balloonRiseRate;   // rate at which balloon rises
    public Transform[] spawnPoints; // Points at which balloons spawn
    public GameObject[] balloons;   // Red, Yellow, and blue floating balloons
    public GameObject[] staticBalloons; // red yellow and blue non-floating ballons
    public static string difficulty;
    private static bool gameLost;
    public GameObject balloonScore;
    public TextMeshProUGUI timeText;


   // game ends if gameLost is ever true (there are functions that turn it true)
    private void Update()
    {
        if (gameLost)
        {
            StopGame();
        }
    }

    // sets difficulty based on which button was pressed on the menu
    public void SetDifficulty(string s)
    {
        difficulty = s;
    }

    public void StartGame()
    {
        ResetGame();

        if (difficulty == "Easy")
        {
            spawnInterval = 3f;
            balloonRiseRate = 0.8f;
            StartCoroutine(Countdown(gameTime));
        }
        else if (difficulty == "Medium")
        {
            spawnInterval = 2f;
            balloonRiseRate = 1.6f;
            StartCoroutine(Countdown(gameTime));
        }
        else if (difficulty == "Hard")
        {
            spawnInterval = 1f;
            balloonRiseRate = 2.4f;
            StartCoroutine(Countdown(gameTime));
        }
        else if (difficulty == "Survival")
        {
            spawnInterval = 3f;
            balloonRiseRate = 1f;

            StartCoroutine(Countup());
            StartCoroutine(IncreaseSpeed());
        }
    }

    // ends the game
    public void StopGame()
    {
        StopAllCoroutines();
        DestroyAllBalloons();
        timeText.text = "Game\nOver";
    }

    // resets the game
    private void ResetGame()
    {
        gameLost = false;

        timeText.text = gameTime + "s";

        for (int i = 0; i < balloonScore.transform.childCount; i++)
        {
            TextMeshProUGUI text;
            text = balloonScore.transform.Find("Blue").Find("BlueText").GetComponent<TextMeshProUGUI>();
            text.text = "x 0";
            text = balloonScore.transform.Find("Yellow").Find("YellowText").GetComponent<TextMeshProUGUI>();
            text.text = "x 0";
            text = balloonScore.transform.Find("Red").Find("RedText").GetComponent<TextMeshProUGUI>();
            text.text = "x 0";
        }

        ShootScript.ResetCount();
    }

    private void DestroyAllBalloons()
    {
        var clones = GameObject.FindGameObjectsWithTag("red balloon");
        foreach (var clone in clones)
        {
            Destroy(clone);
        }

        clones = GameObject.FindGameObjectsWithTag("blue balloon");
        foreach (var clone in clones)
        {
            Destroy(clone);
        }

        clones = GameObject.FindGameObjectsWithTag("yellow balloon");
        foreach (var clone in clones)
        {
            Destroy(clone);
        }
    }

    // Every 4 seconds, will spawn balloons at Point 1, Point 2 and Point 3
    IEnumerator StartSpawning()
    {

        yield return new WaitForSeconds(spawnInterval);

        // Randomly determines whether a balloon will spawn at Points 1, 2 and 3
        bool willSpawn1 = (Random.value > 0.5);
        bool willSpawn2 = (Random.value > 0.5);
        bool willSpawn3 = (Random.value > 0.5);

        // Randomly determines which balloon will spawn at Points 1, 2 and 3
        GameObject balloon1 = balloons[Random.Range(0, 3)];
        GameObject balloon2 = balloons[Random.Range(0, 3)];
        GameObject balloon3 = balloons[Random.Range(0, 3)];


        GameObject clone;
        // Spawn Balloons
        if (willSpawn1)
        {
            clone = Instantiate(balloon1, spawnPoints[0].position, Quaternion.identity);
        }
            
        if (willSpawn2)
        {
            clone = Instantiate(balloon2, spawnPoints[1].position, Quaternion.identity);
        }
            
        if (willSpawn3)
        {
            clone = Instantiate(balloon3, spawnPoints[2].position, Quaternion.identity);
        }
        
        StartCoroutine(StartSpawning());
    }

    IEnumerator Countdown (int seconds)
    {

        timeText.text = "3";
        yield return new WaitForSeconds(1);
        timeText.text = "2";
        yield return new WaitForSeconds(1);
        timeText.text = "1";
        yield return new WaitForSeconds(1);
        timeText.text = "Go!";

        StartCoroutine(StartSpawning());

        int counter = seconds;
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            counter--;
            timeText.text = counter + " s";
        }

        SetGameLost(true);
    }

    IEnumerator Countup()
    {

        timeText.text = "3";
        yield return new WaitForSeconds(1);
        timeText.text = "2";
        yield return new WaitForSeconds(1);
        timeText.text = "1";
        yield return new WaitForSeconds(1);
        timeText.text = "Go!";

        StartCoroutine(StartSpawning());

        int counter = 0;
        while (true)
        {
            yield return new WaitForSeconds(1);
            counter++;
            timeText.text = counter + " s";
        }
    }

    IEnumerator IncreaseSpeed()
    {
        while(true)
        {
            yield return new WaitForSeconds(3);
            balloonRiseRate *= 1.1f;
            if (spawnInterval > 0.2)
            {
                spawnInterval /= 1.15f;
            }
            else
            {
                Debug.Log("Spawn interval at minimum (" + spawnInterval + ")");
            }

            Debug.Log("Float rate increased: " + balloonRiseRate);
            Debug.Log("Spawn interval decreased: " + spawnInterval);
        }
        
    }

    public static void SetGameLost (bool b)
    {
        gameLost = b;
    }
}
