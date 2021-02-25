using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
   public float speed;
   public int startIndex;
   public int endIndex;
   public Transform[] backgrounds;

   float viewHeight;

   private void Awake()
   {
       viewHeight = Camera.main.orthographicSize * 2;
   }

   private void Update()
   {
       Vector3 currentPos = transform.position;
       Vector3 nextPos = Vector3.left * speed * Time.deltaTime;
       transform.position = currentPos + nextPos;
        //Debug.Log(endIndex);

       for(int i = 0; i < backgrounds.Length; ++i)
       {
            if(backgrounds[endIndex].position.y < 10)
            {
                Vector3 startBackgroundPos = backgrounds[startIndex].localPosition;
                Vector3 endBackgroundPos = backgrounds[endIndex].localPosition;
                backgrounds[endIndex].transform.localPosition = startBackgroundPos + Vector3.left * 10;
            }
       }

       
   }
}