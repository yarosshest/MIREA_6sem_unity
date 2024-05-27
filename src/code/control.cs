using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

[Serializable]
public class GameData
{
    public int highScore;
}

public class control : MonoBehaviour
{
    private Ray debugRay;
    [SerializeField] private GameObject ball;
    public int forse;
    private int score = 0;
    private float timeMax = 15;
    private float time = 0;

    [SerializeField] ScoreGUI scoreGUI;
    [SerializeField] SaveLoadManager sLM;
    public Slider timeGUI;
    private void Start()
    {
        time = timeMax;
        timeGUI.maxValue = timeMax;
    }

    public void AddInScore(int p)
    {
        score += p;
        scoreGUI.UpdateScore(score);
    }
    void Update()
    {
        time -= Time.deltaTime;
        timeGUI.value=time;

        if (time < 0)
        {
            GameData gameData = sLM.LoadGame();
            if(gameData.highScore < score) {
                gameData.highScore = score;
                sLM.SaveGame(gameData);
            }
            SceneManager.LoadScene("menu");
        }

        Debug.DrawRay(debugRay.origin, debugRay.direction, Color.red);
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag("Trump"))
            {
                debugRay = ray;
                float h = 0.1f;
                Vector3 pointSpawn = new Vector3(hit.point.x, hit.point.y + h, hit.point.z);
                GameObject realBall = Instantiate(ball, pointSpawn, new Quaternion());
                realBall.GetComponent<Rigidbody>().velocity = new Vector3(-forse, 0,0);
            }
        }
    }
}
