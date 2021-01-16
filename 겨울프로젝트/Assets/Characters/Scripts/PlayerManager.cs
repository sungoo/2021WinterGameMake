using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    private GameObject player;
    public int score = 0;
    public float BPM = 100;
    public float floorSpeed = 1;

    private void Awake()
    {
        if (PlayerManager.instance == null)
        {
            PlayerManager.instance = this;
        }
    }

    //initialize
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        Invoke("gameStart", 3f);
    }

    public void gameStart()
    {

    }
}
