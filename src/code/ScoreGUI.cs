using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreGUI : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI scoreText;
    void Start()
    {
        scoreText = gameObject.GetComponent<TextMeshProUGUI>();
    }

    public void UpdateScore(int s)
    {
        scoreText.SetText("Score: " + s.ToString());
    }

}
