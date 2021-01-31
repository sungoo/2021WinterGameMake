using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private GameObject player;
    private GameObject camera;
    private GameObject noteSpawner;
    private GameObject noteCollecter;

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

    //판정별 점수
    private int score_miss = 0;
    private int score_bad = 10;
    private int score_good = 100;
    private int score_cool = 300;
    private int score_perpect = 500;

    private void Awake()
    {
        if (GameManager.instance == null)
        {
            GameManager.instance = this;
        }

        var manager = FindObjectsOfType<GameManager>();
        if(manager.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //initialize
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        camera = GameObject.FindWithTag("MainCamera");
        noteSpawner = GameObject.FindWithTag("Spawner");
        noteCollecter = GameObject.FindWithTag("Collecter");

        Invoke("gameStart", 3f);
    }

    public void gameStart()
    {

    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        player.transform.Translate(Vector3.right * floorSpeed * Time.deltaTime);
        camera.transform.Translate(Vector3.right * floorSpeed * Time.deltaTime);
        noteSpawner.transform.Translate(Vector3.right * floorSpeed * Time.deltaTime);
        noteCollecter.transform.Translate(Vector3.right * floorSpeed * Time.deltaTime);
    }
}
