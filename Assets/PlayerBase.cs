using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public GameObject deathExplosion;
    public AudioClip deathKnell;
    public AudioClip pewpew;
    public AudioClip powerupSound;
    public float moveSpeed;
    public bool fired; // Tracks whether or not a player missile is still live
    public GameObject controller;

    public GameObject playerGun;
    GameObject myGun;

    public GameObject missile;
    public GameObject beamObj;

    float timer;
    float fireRate;

    public bool rapidFire;
    public bool beam;

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
        beam = false;
    }


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
        bool touchMoveLeft = false;
        bool touchMoveRight = false;
        bool touchFire = false;
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                Vector2 touchPos = touch.position;
                Camera cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
                Vector2 camDims = new Vector2(cam.pixelWidth, cam.pixelHeight);
                if(touchPos.x < (camDims.x * 0.2f))
                {
                    touchMoveLeft = true;
                }
                else if (touchPos.x > (camDims.x * 0.8f))
                {
                    touchMoveRight = true;
                }
                else if (touchPos.y < (camDims.y * 0.5f))
                {
                    touchFire = true;
                }
            }
        }
        if(((Input.GetButton("Fire1") && rapidFire) || Input.GetButtonDown("Fire1") || touchFire) && !fired)
        {
            AudioSource.PlayClipAtPoint(pewpew, gameObject.transform.position);
            fired = true;
            Vector3 spawnPos = gameObject.transform.position;
            spawnPos.z += 0.75f;
            if(beam)
            {
                spawnPos.z += 5.0f;
                GameObject obj = Instantiate(beamObj, spawnPos, Quaternion.identity) as GameObject;
                PlayerMissile m = obj.GetComponent<PlayerMissile>();
                m.playerBase = gameObject;
                m.isBeam = true;
            }
            else{
                GameObject obj = Instantiate(missile, spawnPos, Quaternion.identity) as GameObject;
                PlayerMissile m = obj.GetComponent<PlayerMissile>();
                m.playerBase = gameObject;
                m.isBeam = false;
            }

        }

        if ((Input.GetAxisRaw("Horizontal") > 0 || touchMoveRight) && gameObject.transform.position.x < controller.GetComponent<GameController>().rightLimit) {
            gameObject.transform.Translate(moveSpeed,0,0);
        }
        else if ((Input.GetAxisRaw("Horizontal") < 0 || touchMoveLeft) && gameObject.transform.position.x > controller.GetComponent<GameController>().leftLimit) {
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
            AudioSource.PlayClipAtPoint(powerupSound, gameObject.transform.position);
            if (!rapidFire && !beam)
            {
                rapidFire = true;
                fireRate = 0.25f;
                gameObject.GetComponent<Renderer>().material.color = new Color(0.8f, 0.8f, 0.2f);
            }
            else if(rapidFire && !beam)
            {
                rapidFire = false;
                beam = true;
                fireRate = 2.0f;
                gameObject.GetComponent<Renderer>().material.color = new Color(0.8f, 0.5f, 0);
            }
            else
            {
                rapidFire = true;
                fireRate = 1.0f;
                gameObject.GetComponent<Renderer>().material.color = new Color(0.8f, 0.1f, 0);
            }
            GameObject o = collider.gameObject;
            Destroy(o);
        }
    }

}
