using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMovement : MonoBehaviour
{
    public float noteSpeed = 10;

    // public TimingManager theTimingManager = null;

    private void Start()
    {
        // theTimingManager = FindObjectOfType<TimingManager>();
    }

    private void Update()
    {
        transform.localPosition += Vector3.left * noteSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }

        // if(other.gameObject.tag == "Collector")
        // {
        //     theTimingManager.boxNoteList.Remove(this.gameObject);
        //     // Destroy(gameObject);
        // }
    }
}
