using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNote : MonoBehaviour
{
   public Transform[] spawnPoints;
   public GameObject note;
   public float spawnTime;
   public float curTime;

   public int maxCount;
   public int enemyCount;
   public static SpawnNote _instance;
   private void Start()
   {
       _instance = this;
   }

   private void Update()
   {
       if(curTime >= spawnTime && enemyCount < maxCount)
       {
            int x = Random.Range(0, spawnPoints.Length);
            SpawnNotes(x);
       }

        curTime += Time.deltaTime;
   }

    public void SpawnNotes(int ranNum)
    {
        curTime = 0;
        ++enemyCount;
        Instantiate(note,spawnPoints[ranNum]);
    }
}
