using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{
    public GameController controller;
    Text livesText;
    public PlayerBase pb;
    // Start is called before the first frame update
    void Start()
    {
        livesText = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        livesText.text = "Lives: " + controller.lives.ToString() + '\n';
        string powerText = "Default";
        if (!pb.Equals(null))
        {
            if (pb.rapidFire && !pb.beam)
            {
                powerText = "Rapid Fire";
            }
            else if (!pb.rapidFire && pb.beam)
            {
                powerText = "Beam";
            }
            else if (pb.rapidFire && pb.beam)
            {
                powerText = "Rapid Beam";
            }
        }
        livesText.text += "Weapon Type: " + powerText;
    }
}
