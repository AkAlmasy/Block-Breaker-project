using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStatus : MonoBehaviour {

    [Range(0f,10f)] [SerializeField] float gameSpeed = 1f;
    [SerializeField] int pointsPerBlockDestroyed = 5;
    [SerializeField] int gameScore = 0;
    [SerializeField] Text scoreText;


    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameStatus>().Length;
        if (gameStatusCount > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
       // scoreText.text = gameScore.ToString();
	}

	// Update is called once per frame
	void Update () {
        Time.timeScale = gameSpeed;
	}


    public void AddToScore()
    {
        gameScore += pointsPerBlockDestroyed;
        scoreText.text = gameScore.ToString();
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
