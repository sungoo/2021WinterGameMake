using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyControl : MonoBehaviour
{
    public float fSpeed;

    Rigidbody2D rigidbody;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.AddForce(Vector2.right * fSpeed, ForceMode2D.Impulse);
    }
}
