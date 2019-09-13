using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissile : MonoBehaviour
{
    public Vector3 thrust;
    public GameObject playerBase; // Keeps track of the object that fired this missile

    Rigidbody rb;

    bool live;
    public bool isBeam;
    float beamTimer;

    // Start is called before the first frame update
    void Start()
    {
        // Player Missile layer is 11, powerups are 12
        Physics.IgnoreLayerCollision(11, 11);
        Physics.IgnoreLayerCollision(11, 12);
        gameObject.GetComponent<Renderer>().material.color = new Color(0.8f, 0, 0);
        thrust.z = 800.0f;
        rb = GetComponent<Rigidbody>();
        rb.drag = 0;
        rb.AddRelativeForce(thrust);
        live = true;
        beamTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(isBeam)
        {
            if(beamTimer > 2.0f)
            {
                Destroy(gameObject);
            }
            else
            {
                beamTimer += Time.deltaTime;
            }
        }
    }

    void FixedUpdate()
    {
        if (isBeam)
        {
            rb.AddForce(new Vector3(0, 0, 50));
        }
        else
        {
            rb.AddForce(new Vector3(0, 0, -10));
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;
        PlayerBase pb = playerBase.gameObject.GetComponent<PlayerBase>();
        if (live)
        {
            if(collider.CompareTag("Invader") || collider.CompareTag("MysteryInvader"))
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
            if (!isBeam)
            {
                live = false;
                gameObject.GetComponent<Renderer>().material.color = new Color(0.2f, 0, 0);
            }
        }
    }
}
