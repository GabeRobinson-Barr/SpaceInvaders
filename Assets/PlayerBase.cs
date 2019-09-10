using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public AudioClip deathKnell;
    public AudioClip pewpew;
    public float moveSpeed;
    public bool fired; // Tracks whether or not a player missile is still live
    public GameObject controller;

    public GameObject playerGun;
    GameObject myGun;

    // Start is called before the first frame update
    void Start()
    {
        fired = false;
        moveSpeed = 0.05f;
        Vector3 gunPos = gameObject.transform.position;
        gunPos.z += 0.5f;
        myGun = Instantiate(playerGun, gunPos, Quaternion.identity) as GameObject;
    }

    public GameObject missile;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && !fired)
        {
            AudioSource.PlayClipAtPoint(pewpew, gameObject.transform.position);
            fired = true;
            Vector3 spawnPos = gameObject.transform.position;
            spawnPos.z += 0.75f;

            GameObject obj = Instantiate(missile, spawnPos, Quaternion.identity) as GameObject;
            PlayerMissile m = obj.GetComponent<PlayerMissile>();
            m.playerBase = gameObject;

        }

        if (Input.GetAxisRaw("Horizontal") > 0 && gameObject.transform.position.x < controller.GetComponent<GameController>().rightLimit) {
            gameObject.transform.Translate(moveSpeed,0,0);
            myGun.transform.Translate(moveSpeed,0,0);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0 && gameObject.transform.position.x > controller.GetComponent<GameController>().leftLimit) {
            gameObject.transform.Translate(-moveSpeed,0,0);
            myGun.transform.Translate(-moveSpeed,0,0);
        }
    }

    public void Die()
    {
        // todo fill this with the death stuff
        AudioSource.PlayClipAtPoint(deathKnell, gameObject.transform.position);
        GameController gc = controller.GetComponent<GameController>();
        gc.newLife();
        Destroy(myGun);
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;
        if(collider.CompareTag("Invader")) {
            controller.GetComponent<GameController>().gameOver();
            Destroy(myGun);
            Destroy(gameObject);
        }
    }

}
