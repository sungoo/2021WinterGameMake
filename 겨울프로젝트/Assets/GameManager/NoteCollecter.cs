using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteCollecter : MonoBehaviour
{
    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            Destroy(collision);
        }
    }
}
