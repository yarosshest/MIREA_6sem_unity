using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    [SerializeField] SaveLoadManager sLM;
    public TextMeshProUGUI scoreText;
    void Start()
    {
        GameData gameData = sLM.LoadGame();
        if (gameData == null)
        {
            scoreText.SetText("Max score: 0");
        } else
        {
            scoreText.SetText("Max score: " + gameData.highScore.ToString());
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("game");
    }
}
