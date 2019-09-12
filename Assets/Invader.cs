using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    // Add 
    public int pointVal;
    public float moveSpeed;
    public AudioClip pew;
    public bool live;

    public GameController gameController;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreLayerCollision(8,9);
        Physics.IgnoreLayerCollision(8,8);
        live = true;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    public GameObject missile; // TODO implement random firing logic
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire() // fires a missile
    {
        AudioSource.PlayClipAtPoint(pew, gameObject.transform.position);
        Vector3 spawnpos = gameObject.transform.position;
        spawnpos.z -= 0.25f;
        Instantiate(missile, spawnpos, Quaternion.identity);
    }

    void FixedUpdate()
    {
        if (live)
        {
            gameObject.transform.Translate(moveSpeed,0,0);
        }
        else
        {
            rb.AddForce(new Vector3(0,0,-2));
        }
        
    }


    public void Die()
    {
        gameController.score += pointVal;
        if (pointVal <= 30)
        {
            gameController.increaseSpeed();
        }
        rb.constraints = RigidbodyConstraints.None;
        rb.AddRelativeForce(new Vector3(0,0,-200));
        gameObject.GetComponent<Renderer>().material.color = new Color(0.1f, 0.1f, 0.1f);
        live = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (live)
        {
            Collider collider = collision.collider;
            if(collider.CompareTag("BaseShield"))
            {
                // do stuff for this case once you do baseshield
                Destroy(collider.gameObject);
            }
        }
    }
}
