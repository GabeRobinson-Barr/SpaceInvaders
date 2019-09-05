using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    // Add 
    public int pointVal;
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = -0.01f;
    }

    public GameObject missile; // TODO implement random firing logic
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(moveSpeed,0,0);
    }


    public void Die()
    {
        /*TODO
        add score increase
        decrease number of live invaders
        play animation and sound byte
         */
        Destroy(gameObject);
    }
}
