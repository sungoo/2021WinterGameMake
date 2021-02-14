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

    //레벨 데이터
    public TextAsset levelData;

    public struct SheetInfo
    {
        public string fileNamme;
        public int viewTime;
        public int BPM;
        public int musicTempo;
        public int standardTempo;
        public int bit;
    }

    public struct ContentInfo
    {
        public string title;
        public string artist;
        public string source;
        public string sheet;
        public int difficulty;
    }

    public struct NoteInfo
    {
        public int kind;        //노트 종류
        public int line;        //노트 위치
        public int spawnTime;   //출현 시간
        public int holdTime;    //롱노트 길이
    }

    SheetInfo sheet = new SheetInfo();
    ContentInfo content = new ContentInfo();
    List<NoteInfo> note = new List<NoteInfo>();

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

        ParseSheet();
    }

    //initialize
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        camera = GameObject.FindWithTag("MainCamera");
        noteSpawner = GameObject.FindWithTag("Spawner");
        noteCollecter = GameObject.FindWithTag("Collecter");

        ShowData();

        Invoke("gameStart", 3f);
    }

    public void gameStart()
    {

    }

    private void Update()
    {
        Move();
        //Metronome();
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

    public void ParseSheet()
    {
        //텍스트 데이터 문자열로 옮기기
        string level_texts = levelData.text;

        //개행 문자 마다 분할해서 문자열 배열에 넣기
        string[] lines = level_texts.Split('\n');

        //각 행에 대해 차례로 처리
        foreach (var line in lines)
        {
            if (line == "")//행이 빈 줄이면
            {
                continue;//다음 행으로 건너뛰기
            }
            string[] textSplit = line.Split('=');//'='을 기준으로 문자열 분리

            //본격적인 파싱
            //sheet info
            if (textSplit[0].Equals("AudioFileName"))
                sheet.fileNamme = textSplit[1];
            else if (textSplit[0].Equals("AudioViewTime"))
                sheet.viewTime = int.Parse(textSplit[1]);
            else if (textSplit[0].Equals("BPM"))
                sheet.BPM = int.Parse(textSplit[1]);
            else if (textSplit[0].Equals("MusicTempo"))
                sheet.musicTempo = int.Parse(textSplit[1]);
            else if (textSplit[0].Equals("StdTempo"))
                sheet.standardTempo = int.Parse(textSplit[1]);
            else if (textSplit[0].Equals("Bit"))
                sheet.bit = int.Parse(textSplit[1]);

            //content info
            else if (textSplit[0].Equals("Title"))
                content.title = textSplit[1];
            else if (textSplit[0].Equals("Artist"))
                content.artist = textSplit[1];
            else if (textSplit[0].Equals("Source"))
                content.source = textSplit[1];
            else if (textSplit[0].Equals("Sheet"))
                content.sheet = textSplit[1];

            //note info
            else if (textSplit[0].Equals('0'))
            {
                NoteInfo temp = new NoteInfo();

                temp.kind = int.Parse(textSplit[1]);
                temp.line = int.Parse(textSplit[2]);
                temp.spawnTime = int.Parse(textSplit[3]);
                temp.holdTime = int.Parse(textSplit[4]);

                note.Add(temp);
            }
        }
    }

    public void ShowData()
    {
        Debug.Log("Sheet Info :");
        Debug.Log(sheet.fileNamme);
        Debug.Log(sheet.viewTime);
        Debug.Log(sheet.BPM);
        Debug.Log(sheet.musicTempo + '/' + sheet.standardTempo);
        Debug.Log(sheet.bit);

        Debug.Log("Note Info :");
        foreach(NoteInfo aNote in note)
        {
            Debug.Log(aNote.kind);
            Debug.Log(aNote.line);
            Debug.Log(aNote.spawnTime);
            Debug.Log(aNote.holdTime);
        }
    }

    IEnumerator PlayTik(float tikTime)
    {
        Debug.Log(nextTime);//오차 확인
        yield return new WaitForSeconds(tikTime);//tikTime 만큼 대기
    }
}
