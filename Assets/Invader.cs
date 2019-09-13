using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    // Add 
    public int pointVal;
    public float moveSpeedX;
    public float moveSpeedZ;
    public AudioClip pew;
    public bool live;

    public GameController gameController;
    public GameObject powerup;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreLayerCollision(8, 12);
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
            gameObject.transform.Translate(moveSpeedX,0,-moveSpeedZ);
        }
        else
        {
            rb.AddForce(new Vector3(0, 0, -2));
        }
        
    }


    public void Die()
    {
        gameController.score += pointVal;
        if (pointVal <= 30)
        {
            gameController.increaseSpeed();
        }
        else
        {
            if (Random.Range(0, 1.0f) > 0.5f)
            {
                Vector3 powerVec = gameObject.transform.position;
                powerVec.z -= 0.5f;
                Instantiate(powerup, powerVec, Quaternion.identity);
            }
            gameController.mysteryActive = false;
        }
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezeRotationX;
        rb.constraints = RigidbodyConstraints.FreezeRotationZ;
        rb.constraints = RigidbodyConstraints.FreezePositionY;
        rb.AddForce(new Vector3(0,0,-300));
        gameObject.GetComponent<Renderer>().material.color = new Color(0.1f, 0.1f, 0.1f);
        live = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;
        if (live)
        {
            if(collider.CompareTag("BaseShield"))
            {
                // do stuff for this case once you do baseshield
                Destroy(collider.gameObject);
            }
        }
        else if(collider.CompareTag("Boundary"))
        {
            rb.constraints = RigidbodyConstraints.FreezePositionZ;
        }
    }
}
