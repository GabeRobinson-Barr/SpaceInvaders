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
        bool start = false;
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                start = true;
            }
        }
        if (Input.GetButtonDown("Fire1") || Input.GetAxisRaw("Horizontal") != 0 || Input.GetButtonDown("Fire2") || start)
        {
            controller.startGame();
            Destroy(gameObject);
        }
    }
}
