using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    bool gameStarted = false;
    public int lives;
    public int invaders;
    public int score;
    public int level;

    public Vector3 spawnpos;
    public Vector3 upperBoundPos;
    public Vector3 lowerBoundPos;
    public GameObject boundary;

    public GameObject playerBase;
    PlayerBase pb;

    public GameObject baseShield;
    public GameObject smallInvader;
    public GameObject medInvader;
    public GameObject largeInvader;
    public GameObject mystInvader;
    Invader[,] invaderArr;

    bool invadersMoveLeft = true;

    bool mysteryActive = false;
    GameObject currentMystery;
    public GameObject scoreUI;
    public GameObject livesUI;
    public GameObject startScreen;
    public GameObject gameOverUI;
    // Start is called before the first frame update
    public int[] highscores;

    Camera cam;
    public float leftLimit;
    public float rightLimit;
    public float topLimit;
    public float botLimit;
    void Start()
    {
        //Screen.SetResolution(1280, 720, false);
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Vector3 botleft = cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector3 topright = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, cam.nearClipPlane));
        leftLimit = botleft.x;
        botLimit = botleft.z;
        topLimit = topright.z;
        rightLimit = topright.x;
        spawnpos.z = botLimit + 0.5f;
        highscores = new int[10];
        for(int i = 0; i < 10; i++) {
            highscores[i] = 1000 * (i + 1);
        }
        newGame();
    }

    public void newGame()
    {
        GameObject startScreenobj = Instantiate(startScreen, new Vector3(0,0,0), Quaternion.identity) as GameObject;
        StartScreen ss = startScreenobj.gameObject.GetComponent<StartScreen>();
        ss.controller = gameObject.GetComponent<GameController>();
    }

    public void startGame()
    {
        gameStarted = true;
        lives = 3;
        score = 0;
        level = 0;
        //upperBoundPos.z = topLimit + 0.5f;
        lowerBoundPos.z = botLimit - 0.5f;
        //GameObject upperBound = Instantiate(boundary, upperBoundPos, Quaternion.identity) as GameObject;
        GameObject lowerbound = Instantiate(boundary, lowerBoundPos, Quaternion.identity) as GameObject;
        setInvaders();
        GameObject pbObj = Instantiate(playerBase, spawnpos, Quaternion.identity) as GameObject;
        pb = pbObj.GetComponent<PlayerBase>();
        pb.controller = gameObject;
        GameObject scoreUIobj = Instantiate(scoreUI, new Vector3(0,0,0), Quaternion.identity) as GameObject;
        ScoreUI s_UI = scoreUIobj.gameObject.GetComponent<ScoreUI>();
        s_UI.controller = gameObject.GetComponent<GameController>();
        GameObject livesUIobj = Instantiate(livesUI, new Vector3(0,0,0), Quaternion.identity) as GameObject;
        LivesUI l_UI = livesUIobj.gameObject.GetComponent<LivesUI>();
        l_UI.controller = gameObject.GetComponent<GameController>();
        createBases();
    }

    // Update is called once per frame
    void Update() // This handles checking the boundaries for the invaders
    {
        if (gameStarted)
        {
            if(mysteryActive)
            {
                if (!currentMystery.Equals(null))
                {
                    if(currentMystery.transform.position.x <= leftLimit)
                    {
                        mysteryActive = false;
                        Destroy(currentMystery);
                    }
                }
                else{
                    mysteryActive = false;
                }
            }
            else {
                if(Random.Range(0.0f, 10.0f) > 9.99f && invaders > 5)
                {
                    spawnMystery();
                }
            }
            float minPos = 10.0f;
            float maxPos = -10.0f;
            for(int row = 0; row < 5; row++)
            {
                for(int col = 0; col < 11; col++)
                {
                    if (invaderArr[row,col].live)
                    {
                        Vector3 pos = invaderArr[row,col].transform.position;
                        minPos = Mathf.Min(pos.x, minPos);
                        maxPos = Mathf.Max(pos.x, maxPos);
                    }
                }
            }
            if ((minPos <= (leftLimit + 0.5f) && invadersMoveLeft) || (maxPos >= (rightLimit - 0.5f) && !invadersMoveLeft))
            {
                invadersMoveLeft = !invadersMoveLeft;
                for(int row = 0; row < 5; row++)
                {
                    for(int col = 0; col < 11; col++)
                    {
                        if (invaderArr[row,col].live)
                        {
                            invaderArr[row,col].transform.Translate(0,0,-((topLimit - botLimit) / 20.0f));
                            invaderArr[row,col].moveSpeed *= -1;
                        }
                    }
                }
            }
            if (Random.Range(0.0f, 1.0f) <= (0.002f * ((invaders / 10) + 1.0f)))
            {
                int randRow = (int)Random.Range(0.0f, 4.0f);
                int randCol = (int)Random.Range(0.0f, 10.0f);
                while(!invaderArr[randRow,randCol].live)
                {
                    randCol++;
                    if(randCol >= 11)
                    {
                        randRow++;
                        randCol = 0;
                        if (randRow >= 5)
                        {
                            randRow = 0;
                        }
                    }
                }
                invaderArr[randRow,randCol].Fire();
            }
        }
        
    }

    void spawnMystery()
    {
        currentMystery = Instantiate(mystInvader, new Vector3(rightLimit, 0, topLimit - 0.1f), Quaternion.identity) as GameObject;
        Invader mysteryInvader = currentMystery.GetComponent<Invader>();
        mysteryInvader.moveSpeed = -0.05f;
        mysteryInvader.pointVal = (int)Random.Range(50, 300);
        mysteryInvader.gameController = gameObject.GetComponent<GameController>();
        mysteryActive = true;
    }

    void createBases()
    {
        float offset = ((rightLimit - leftLimit) - 6.0f) / 3.0f;
        Vector3 basePos = new Vector3(leftLimit + 2.0f, 0, botLimit + 1.5f);
        for(int i = 0; i < 4; i++)
        {
            for(int row = 0; row < 10; row++)
            {
                for(int col = 0; col < 10; col++)
                {
                    Vector3 thisPos = basePos;
                    thisPos.z += 0.1f * row;
                    thisPos.x += 0.2f * col;
                    if(!((row == 9 && (col < 3 || col > 6)) || (row == 8 && (col < 2 || col > 7)) ||
                        (row == 7 && (col < 1 || col > 8)) || (row < 4 && col > 2 && col < 7)))
                        {
                            Instantiate(baseShield, thisPos, Quaternion.identity);
                        }
                }
            }
            basePos.x += offset;
        }
        
    }

    public void newLife() { // This gets called by playerBase when you die
        GameObject[] i_missiles;
        i_missiles = GameObject.FindGameObjectsWithTag("InvaderMissile");
        foreach(GameObject i_m in i_missiles)
        {
            Destroy(i_m);
        }
        GameObject[] p_missiles;
        p_missiles = GameObject.FindGameObjectsWithTag("PlayerMissile");
        foreach(GameObject p_m in p_missiles)
        {
            Destroy(p_m);
        }
        if(lives == 0) {
            gameOver();
        }
        else
        {
            lives--;
            GameObject obj = Instantiate(playerBase, spawnpos, Quaternion.identity) as GameObject;
            pb = obj.GetComponent<PlayerBase>();
            pb.controller = gameObject;
        }
    }

    public void gameOver() {
        // transition to gameover screen
        gameStarted = false;
        GameObject[] uis = GameObject.FindGameObjectsWithTag("UI");
        foreach(GameObject u in uis)
        {
            Destroy(u);
        }
        GameObject[] baseshields;
        baseshields = GameObject.FindGameObjectsWithTag("BaseShield");
        foreach(GameObject bs in baseshields)
        {
            Destroy(bs);
        }
        if (!currentMystery.Equals(null))
        {
            Destroy(currentMystery);
            mysteryActive = false;
        }
        GameObject gameOverobj = Instantiate(gameOverUI, new Vector3(0,0,0), Quaternion.identity) as GameObject;
        GameOverUI go_UI = gameOverobj.GetComponent<GameOverUI>();
        go_UI.controller = gameObject.GetComponent<GameController>();
        for(int row = 0; row < 5; row++)
        {
            for(int col = 0; col < 11; col++)
            {
                Destroy(invaderArr[row,col].gameObject);
            }
        }
    }

    public void increaseSpeed() // invaders call this when they die to increase the speed of the others
    {
        if(invaders > 1)
        {
            invaders--;
            for(int row = 0; row < 5; row++)
            {
                for(int col = 0; col < 11; col++)
                {
                    if (!invaderArr[row,col].Equals(null))
                    {
                        if (invadersMoveLeft)
                        {
                            invaderArr[row,col].moveSpeed -= 0.015f / invaders;
                        }
                        else
                        {
                            invaderArr[row,col].moveSpeed += 0.015f / invaders;
                        }
                        
                    }
                }
            }
        }
        else {
            for(int i = 0; i < 5; i++)
            {
                for(int j = 0; j < 11; j++)
                {
                    GameObject[] invs = GameObject.FindGameObjectsWithTag("Invader");
                    foreach(GameObject inv in invs)
                    {
                        Destroy(inv);
                    }
                }
            }
            level = (level + 1) % 10;
            setInvaders();
        }
    }

    void setInvaders()
    {
        invadersMoveLeft = true;
        invaderArr = new Invader[5,11];
        float offsetX = (rightLimit - leftLimit) / 25;
        float offsetZ = (topLimit - botLimit) / 20;
        Vector3 invaderPos = new Vector3(0.0f, 0.0f, (topLimit - (5 + level)* offsetZ));
        for(int row = 0; row < 5; row++)
        {
            for(int col = 0; col < 11; col++)
            {
                if(row < 2)
                {
                    GameObject invader = Instantiate(largeInvader, invaderPos, Quaternion.identity) as GameObject;
                    Invader i = invader.GetComponent<Invader>();
                    i.gameController = gameObject.GetComponent<GameController>();
                    i.pointVal = 10;
                    i.moveSpeed = -0.01f;
                    invaderArr[row,col] = i;
                }
                else if (row < 4)
                {
                    GameObject invader = Instantiate(medInvader, invaderPos, Quaternion.identity) as GameObject;
                    Invader i = invader.GetComponent<Invader>();
                    i.gameController = gameObject.GetComponent<GameController>();
                    i.pointVal = 20;
                    i.moveSpeed = -0.01f;
                    invaderArr[row,col] = i;
                }
                else {
                    GameObject invader = Instantiate(smallInvader, invaderPos, Quaternion.identity) as GameObject;
                    Invader i = invader.GetComponent<Invader>();
                    i.gameController = gameObject.GetComponent<GameController>();
                    i.pointVal = 30;
                    i.moveSpeed = -0.01f;
                    invaderArr[row,col] = i;
                }
                invaderPos.x += offsetX;
            }
            invaderPos.x = 0.0f;
            invaderPos.z += offsetZ;
        }
        invaders = 55;
        invaderPos.x = 0.0f; // Reset the invader positions
        invaderPos.z = 1.5f - (level / 2.0f);
    }
}
