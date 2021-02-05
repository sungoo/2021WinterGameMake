using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteCollecter : MonoBehaviour
{
    public TimingManager theTimingManager = null;
    public NoteManager theNoteManager = null;
    private void Start()
    {
        theTimingManager = FindObjectOfType<TimingManager>();
        theNoteManager = FindObjectOfType<NoteManager>();
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Note")
        {
            theTimingManager.boxNoteList.Remove(collision.gameObject);
            Destroy(collision.gameObject);
        }
    }
}