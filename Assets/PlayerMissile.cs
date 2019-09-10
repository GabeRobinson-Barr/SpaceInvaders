using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissile : MonoBehaviour
{
    public Vector3 thrust;
    public GameObject playerBase; // Keeps track of the object that fired this missile

    // Start is called before the first frame update
    void Start()
    {
        thrust.z = 600.0f;
        GetComponent<Rigidbody>().drag = 0;
        GetComponent<Rigidbody>().AddRelativeForce(thrust);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;
        PlayerBase pb = playerBase.gameObject.GetComponent<PlayerBase>();
        pb.fired = false;
        if(collider.CompareTag("Invader"))
        {
            Invader invader = collider.gameObject.GetComponent<Invader>();
            invader.Die();
            Destroy(gameObject);
        }
        else if (collider.CompareTag("InvaderMissile"))
        {
            InvaderMissile i_missile = collider.gameObject.GetComponent<InvaderMissile>();
            Destroy(gameObject);
            // The invader missile's collision function will handle its own death
        }
        else if (collider.CompareTag("Boundary"))
        {
            Destroy(gameObject);
        }
        else if (collider.CompareTag("BaseShield"))
        {
            Destroy(collider.gameObject);
            Destroy(gameObject);
        }
    }
}
