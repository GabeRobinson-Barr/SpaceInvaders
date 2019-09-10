using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    public GameController controller;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") || Input.GetAxisRaw("Horizontal") != 0 || Input.GetButtonDown("Fire2"))
        {
            controller.startGame();
            Destroy(gameObject);
        }
    }
}
