
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderMissile : MonoBehaviour
{
    public Vector3 thrust;
    public float missileStrength;

    bool live;

    // Start is called before the first frame update
    void Start()
    {
        // TODO CHANGE HOW THIS IS IMPLEMENTED FOR FAST/SQUIGGLY MISSILES 
        thrust.z = -300.0f;
        missileStrength = 1;
        GetComponent<Rigidbody>().drag = 0;
        GetComponent<Rigidbody>().AddRelativeForce(thrust);
        Physics.IgnoreLayerCollision(8,9);
        Physics.IgnoreLayerCollision(9,9);
        gameObject.GetComponent<Renderer>().material.color = new Color(0,0,0.8f);
        live = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(!live)
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0,0,-5));
        }
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;
        if(live)
        {
            if(collider.CompareTag("PlayerBase"))
            {
                PlayerBase playerBase = collider.gameObject.GetComponent<PlayerBase>();
                playerBase.Die();
            }
            else if (collider.CompareTag("PlayerMissile"))
            {
                PlayerMissile p_missile = collider.gameObject.GetComponent<PlayerMissile>();
                // The player missile's collision function will handle its own death
            }
            else if (collider.CompareTag("Boundary"))
            {
                Destroy(gameObject);
            }
            else if (collider.CompareTag("BaseShield"))
            {
                Destroy(collider.gameObject);
            }
            live = false;
            gameObject.GetComponent<Renderer>().material.color = new Color(0, 0, 0.2f);
        }
    }
}
