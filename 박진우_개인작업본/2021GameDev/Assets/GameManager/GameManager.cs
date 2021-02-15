using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private GameObject player;
    private GameObject camera;
    private GameObject noteSpawner;
    private GameObject noteCollecter;

    //박자 계산
    public float musicBPM = 60f;
    public float stdBPM = 60f;
    public int musicTempo = 4;
    public int stdTempo = 4;

    private float tikTime = 0;
    private float nextTime = 0;

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
        noteCollecter = GameObject.FindWithTag("Collector");

        Invoke("gameStart", 3f);
    }

    public void gameStart()
    {

    }

    private void Update()
    {
        Move();
        Metronome();
    }

    private void Move()
    {
        player.transform.Translate(Vector3.right * floorSpeed * Time.deltaTime);
        camera.transform.Translate(Vector3.right * floorSpeed * Time.deltaTime);
        noteSpawner.transform.Translate(Vector3.right * floorSpeed * Time.deltaTime);
        noteCollecter.transform.Translate(Vector3.right * floorSpeed * Time.deltaTime);
    }

    private void Metronome()
    {
        tikTime = (stdBPM / musicBPM) * (musicTempo / stdTempo);

        nextTime += Time.deltaTime;

        if (nextTime > tikTime)
        {
            StartCoroutine(PlayTik(tikTime));
            nextTime = 0;
        }
    }

    IEnumerator PlayTik(float tikTime)
    {
        Debug.Log(nextTime);//오차 확인
        yield return new WaitForSeconds(tikTime);//tikTime 만큼 대기
    }
}
