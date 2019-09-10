using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    bool rotated = false;
    Vector3 topdown;
    // Start is called before the first frame update
    void Start()
    {
        topdown = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            if (!rotated)
            {
                gameObject.transform.Rotate(-75, 0, 0, Space.Self);
                gameObject.transform.position = new Vector3(0, 2.0f, -6.0f);
                rotated = true;
            }
            else{
                gameObject.transform.Rotate(75, 0, 0, Space.Self);
                gameObject.transform.position = topdown;
                rotated = false;
            }
            
        }   
    }
}
