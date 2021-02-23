using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    public List<GameObject> boxNoteList = new List<GameObject>();

    [SerializeField] Transform Center = null;
    [SerializeField] Transform[] timingRect = null;
    Vector3[] timingBoxs = null;

    ScoreManager theScoreManager;
    
    // Start is called before the first frame update
    void Start()
    {
        // 충돌 판정 박스 설정
        timingBoxs = new Vector3[timingRect.Length];

        for(int i = 0; i < timingRect.Length; ++i)
        {
            // 각각 판정 범위 => 최소값 = 중심 - (이미지 너비/2) // 최대값 = 중심 + (이미지 너비/2)
            timingBoxs[i].Set(Center.localPosition.x - timingRect[i].localScale.x / 2, 
                              Center.localPosition.y + timingRect[i].localScale.y / 2,
                              Center.localPosition.z);
        }

        theScoreManager = FindObjectOfType<ScoreManager>();
    }

    private void Update()
    {
        for(int i = 0; i < boxNoteList.Count; ++i)
        {
            if(boxNoteList[i] == null)
            {
                boxNoteList.Remove(boxNoteList[i]);
            }            
        }
    }

    public void CheckTiming()
    {
        for(int i = 0; i < boxNoteList.Count; ++i)
        {
            float t_notePosX = boxNoteList[i].transform.localPosition.x;

            for(int j = 0; j < timingBoxs.Length; ++j)
            {
                if(timingBoxs[j].x <= t_notePosX && t_notePosX <= timingBoxs[j].y)
                {
                    Destroy(boxNoteList[i]);
                    boxNoteList.RemoveAt(i);
                    Debug.Log("Hit" + j);

                    theScoreManager.IncreaseScore(j);
                    return;
                }
            }
        }

        Debug.Log("Miss");
    }
}
