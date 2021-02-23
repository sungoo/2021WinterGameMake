using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackObjectScroll : MonoBehaviour
{
    [SerializeField] Transform[] m_tfBackObjects = null;
    [SerializeField] float m_speed = 0f;

    float m_leftPosX = 0f;
    float m_rightPosX = 0f;

    private void Start()
    {
       float t_length = m_tfBackObjects[0].GetComponent<GameObject>().transform.localScale.x;
       m_leftPosX = -t_length;
       m_rightPosX = t_length * m_tfBackObjects.Length;
    }
   
    private void Update()
    {
       for(int i = 0; i < m_tfBackObjects.Length; ++i)
       {
           m_tfBackObjects[i].position -= new Vector3(m_speed,0,0) * Time.deltaTime;

           if(m_tfBackObjects[i].position.x < m_leftPosX)
           {
               Vector3 t_selfPos = m_tfBackObjects[i].position;
               t_selfPos.Set(t_selfPos.x + m_rightPosX, t_selfPos.y, t_selfPos.z);
               m_tfBackObjects[i].position = t_selfPos;
           }            
       }
    }

}
