using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float fSpeed;
    
    public float jumpPower;
    
    Rigidbody2D rigidbody;

    // 모바일 키 입력
    public bool inputJump = false;  // 버튼 입력 변수
    bool isJump = false;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Start()
    {
        ButtonControl buttonControl = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ButtonControl>();
        buttonControl.Initialize();
    }
    // Update is called once per frame
    public void Update()
    {
        if(inputJump)
        {
            rigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            isJump = true;
            inputJump = false;
        }

        
    }

    void FixedUpdate()
    {
        // Landing Platform
        if(rigidbody.velocity.y < 0)
        {
            Debug.DrawRay(rigidbody.position, Vector3.down, new Color(0,1,0));

            RaycastHit2D rayHit = Physics2D.Raycast(rigidbody.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
            if(rayHit.collider != null)
            {
                if(rayHit.distance < 1f)
                {
                    Debug.Log(rayHit.collider.name);
                }
            }
        }
    }

}
