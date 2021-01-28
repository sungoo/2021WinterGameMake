using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    private GameObject player;
    public int BPM = 100;
    public float floorSpeed = 1;

    public int score = 0;
    public int highScore = 0;
    public int combo = 0;
    public int maxCombo = 0;

    //판정이 나온 횟수
    public int miss = 0;
    public int bad = 0;
    public int good = 0;
    public int cool = 0;
    public int perpect = 0;

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
