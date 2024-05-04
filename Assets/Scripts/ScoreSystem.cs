using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private float score;
    [SerializeField] private TextMeshProUGUI scoreText;
    
    void Start()
    {
        
    }

   
    void Update()
    {
        score = score + Time.deltaTime;

        scoreText.text = "Score :" + score.ToString();
    }
}
