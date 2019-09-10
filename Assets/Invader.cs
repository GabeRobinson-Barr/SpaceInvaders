using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    // Add 
    public int pointVal;
    public float moveSpeed;
    public AudioClip pew;

    public GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreLayerCollision(8,9);
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
        gameObject.transform.Translate(moveSpeed,0,0);
    }


    public void Die()
    {
        /*TODO
        add score increase
        decrease number of live invaders
        play animation and sound byte
         */
        gameController.score += pointVal;
        if (pointVal <= 30)
        {
            gameController.increaseSpeed();
        }
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;
        if(collider.CompareTag("BaseShield"))
        {
            // do stuff for this case once you do baseshield
            Destroy(collider.gameObject);
        }
    }
}
