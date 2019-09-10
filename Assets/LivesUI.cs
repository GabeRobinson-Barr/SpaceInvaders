using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{
    public GameController controller;
    Text livesText;
    // Start is called before the first frame update
    void Start()
    {
        livesText = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        livesText.text = "Lives: " + controller.lives.ToString();   
    }
}
