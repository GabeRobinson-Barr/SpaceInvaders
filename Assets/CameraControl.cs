using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    bool rotated = false;
    Vector3 topdown;
    Vector3 backview;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        topdown = gameObject.transform.position;
        Camera cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        backview = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth / 2.0f, 0, cam.nearClipPlane));
        backview.y = 5.0f;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        bool rotate = false;
        if(Input.touchCount > 2 && timer <= 0.01f)
        {
            rotate = true;
            timer = 1.0f;
        }
        if(timer > 0.01f) {
            timer -= Time.deltaTime;
        }
        

        if(Input.GetButtonDown("Fire2") || rotate)
        {
            if (!rotated)
            {
                gameObject.transform.Rotate(-80, 0, 0, Space.Self);
                
                gameObject.transform.position = backview;
                rotated = true;
            }
            else{
                gameObject.transform.Rotate(80, 0, 0, Space.Self);
                gameObject.transform.position = topdown;
                rotated = false;
            }
            
        }   
    }
}
