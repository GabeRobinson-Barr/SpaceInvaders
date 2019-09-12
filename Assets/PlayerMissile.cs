using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissile : MonoBehaviour
{
    public Vector3 thrust;
    public GameObject playerBase; // Keeps track of the object that fired this missile

    Rigidbody rb;

    bool live;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Renderer>().material.color = new Color(0.8f, 0, 0);
        thrust.z = 800.0f;
        rb = GetComponent<Rigidbody>();
        rb.drag = 0;
        rb.AddRelativeForce(thrust);
        live = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        rb.AddForce(new Vector3(0,0,-10));
    }

    void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;
        PlayerBase pb = playerBase.gameObject.GetComponent<PlayerBase>();
        if (live)
        {
            if(collider.CompareTag("Invader"))
            {
                Invader invader = collider.gameObject.GetComponent<Invader>();
                if(invader.live)
                {
                    invader.Die();
                }
            }
            else if (collider.CompareTag("InvaderMissile"))
            {
                InvaderMissile i_missile = collider.gameObject.GetComponent<InvaderMissile>();
                // The invader missile's collision function will handle its own death
            }
            else if (collider.CompareTag("BaseShield"))
            {
                Destroy(collider.gameObject);
            }
            live = false;
            gameObject.GetComponent<Renderer>().material.color = new Color(0.2f,0,0);
        }
    }
}
