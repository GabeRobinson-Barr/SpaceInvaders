using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public float moveSpeed;
    public bool fired; // Tracks whether or not a player missile is still live

    // Start is called before the first frame update
    void Start()
    {
        fired = false;
        moveSpeed = 0.05f;
    }

    public GameObject missile;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && !fired)
        {
            fired = true;
            Vector3 spawnPos = gameObject.transform.position;
            spawnPos.z += 0.75f;

            GameObject obj = Instantiate(missile, spawnPos, Quaternion.identity) as GameObject;
            PlayerMissile m = obj.GetComponent<PlayerMissile>();
            m.playerBase = gameObject;

        }

        if( Input.GetAxisRaw("Horizontal") > 0) {
            gameObject.transform.Translate(moveSpeed,0,0);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0) {
            gameObject.transform.Translate(-moveSpeed,0,0);
        }
    }

    public void Die()
    {
        // todo fill this with the death stuff
    }

}
