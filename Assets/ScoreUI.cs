using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public GameController controller;
    Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + controller.score.ToString();
    }
}
