using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    double currentTime = 0d;

    [SerializeField] Transform[] tfNoteApear = null;
    [SerializeField] GameObject Note = null;
    public GameObject t_note;
    public TimingManager theTimingManager = null;

    private void Start()
    {
        theTimingManager = FindObjectOfType<TimingManager>();
    }
 
    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if(currentTime >= 60d / GameManager.instance.musicBPM)
        {
            int iRan = Random.Range(0,tfNoteApear.Length);
            t_note = Instantiate(Note,tfNoteApear[iRan].position, Quaternion.identity);
            t_note.transform.SetParent(this.transform);

            //  노트가 생성된 순간 박스 리스트에 추가
            theTimingManager.boxNoteList.Add(t_note);

            // currentTime은 0으로 초기화시키면 안됨 -> 0.000123일 경우도 생김
            currentTime -= 60d / GameManager.instance.musicBPM;
        }
    }
}
