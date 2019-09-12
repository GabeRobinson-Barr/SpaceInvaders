using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public GameObject deathExplosion;
    public AudioClip deathKnell;
    public AudioClip pewpew;
    public float moveSpeed;
    public bool fired; // Tracks whether or not a player missile is still live
    public GameObject controller;

    public GameObject playerGun;
    GameObject myGun;

    float timer;
    float fireRate;

    bool rapidFire;
    bool beam;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        fired = false;
        moveSpeed = 0.05f;
        Vector3 gunPos = gameObject.transform.position;
        gunPos.z += 0.5f;
        myGun = Instantiate(playerGun, gunPos, Quaternion.identity) as GameObject;
        fireRate = 0.5f;
        rapidFire = false;
        beam = true;
    }

    public GameObject missile;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= fireRate && fired)
        {
            fired = false;
        }
        if (!fired)
        {
            timer = 0;
        }
        if(Input.GetButtonDown("Fire1") && !fired)
        {
            AudioSource.PlayClipAtPoint(pewpew, gameObject.transform.position);
            fired = true;
            Vector3 spawnPos = gameObject.transform.position;
            spawnPos.z += 0.75f;
            if(beam)
            {
                //fire beam ammo instead
            }
            else{
                GameObject obj = Instantiate(missile, spawnPos, Quaternion.identity) as GameObject;
                PlayerMissile m = obj.GetComponent<PlayerMissile>();
                m.playerBase = gameObject;
            }

        }

        if (Input.GetAxisRaw("Horizontal") > 0 && gameObject.transform.position.x < controller.GetComponent<GameController>().rightLimit) {
            gameObject.transform.Translate(moveSpeed,0,0);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0 && gameObject.transform.position.x > controller.GetComponent<GameController>().leftLimit) {
            gameObject.transform.Translate(-moveSpeed,0,0);
        }
        Vector3 gunVec = gameObject.transform.position;
        gunVec.z += 0.5f;
        myGun.transform.position = gunVec;
    }

    public void Die()
    {
        // todo fill this with the death stuff
        AudioSource.PlayClipAtPoint(deathKnell, gameObject.transform.position);
        Instantiate(deathExplosion, gameObject.transform.position, Quaternion.AngleAxis(-90, Vector3.right));
        GameController gc = controller.GetComponent<GameController>();
        gc.newLife();
        Destroy(myGun);
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;
        if(collider.CompareTag("Invader")) {
            Invader invader = collider.GetComponent<Invader>();
            if(invader.live)
            {
                controller.GetComponent<GameController>().gameOver();
                Destroy(myGun);
                Destroy(gameObject);
            }
        }
        else if (collider.CompareTag("Powerup"))
        {
            if(!rapidFire && !beam)
            {
                rapidFire = true;
                fireRate = 0.1f;
            }
            else if(rapidFire && !beam)
            {
                rapidFire = false;
                beam = true;
                fireRate = 1.0f;
            }
            else
            {
                rapidFire = true;
                fireRate = 0.2f;
            }

        }
    }

}
