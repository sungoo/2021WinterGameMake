using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    double currentTime = 0d;

    [SerializeField] Transform[] tfNoteApear = null;
    [SerializeField] GameObject Note = null;
    // Start is called before the first frame update
 
    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if(currentTime >= 60d / GameManager.instance.BPM)
        {
            int iRan = Random.Range(0,tfNoteApear.Length);
            GameObject t_note = Instantiate(Note,tfNoteApear[iRan].position, Quaternion.identity);
            t_note.transform.SetParent(this.transform);
            // currentTime은 0으로 초기화시키면 안됨 -> 0.000123일 경우도 생김
            currentTime -= 60d / GameManager.instance.BPM;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Note"))
        {
            Debug.Log("충돌충돌");
            Destroy(other.gameObject);
        }
    }
}
