using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public GameController controller;
    Text gameOverText;
    bool displayHighScores;
    // Start is called before the first frame update
    void Start()
    {
        gameOverText = gameObject.GetComponent<Text>();
        displayHighScores = false;
        dispGameOver();
    }

    // Update is called once per frame
    void Update()
    {
        bool next = false;
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                next = true;
            }
        }
        if (Input.GetButtonDown("Fire1") || next)
        {
            if (!displayHighScores)
            {
                displayHighScores = true;
                dispHighScores();
            }
            else
            {
                displayHighScores = false;
                controller.newGame();
                Destroy(gameObject);
            }
        }
        
    }

    void dispGameOver()
    {
        gameOverText.text = "Game Over" + '\n' + '\n' + "Final Score: " + controller.score + '\n' + '\n' + "Press Left Ctrl to continue";
        int i = 0;
        while(controller.score >= controller.highscores[i] && i < 10)
        {
            if (i != 0)
            {
                controller.highscores[i - 1] = controller.highscores[i];
            }
            controller.highscores[i] = controller.score;
            i++;
        }
    }

    void dispHighScores()
    {
        gameOverText.text = "High Scores:" + '\n';
        for(int i = 9; i >= 0; i--)
        {
            gameOverText.text += controller.highscores[i].ToString() + '\n';
        }

        gameOverText.text += '\n' + "Press Left Ctrl to continue";
    }

}
