using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMovement : MonoBehaviour
{
    public float noteSpeed = 400;

    private void Update()
    {
        transform.localPosition += Vector3.left * noteSpeed * Time.deltaTime;
    }
}
