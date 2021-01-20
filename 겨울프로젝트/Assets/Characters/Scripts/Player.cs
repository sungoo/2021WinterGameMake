using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    int hp = 100;
    float jumpPower = 4;

    Animator animator;
    Rigidbody2D rigid;

    bool isJump = false;
    bool isSlide = false;
    bool overcharge = false;
    bool isHit = false;
    bool isDischarge = false;
    bool inAir = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
        rigid = gameObject.GetComponentInChildren<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            if (!isDischarge)
                Discharge();
            return;
        }
    }

    //game over
    void Discharge()
    {

    }

    public void Jump()
    {
        if (isJump)
            return;

        isJump = true;

        rigid.velocity = Vector2.zero;

        Vector2 jumpVelocity = new Vector2(0, jumpPower);
        rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);

        animator.SetBool("Jump", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Attatch : " + collision.gameObject.tag);

        if(collision.tag == "Ground" && rigid.velocity.y < 0)
        {
            if (inAir)
                return;
            isJump = false;
            animator.SetBool("Jump", false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Detatch : " + collision.tag);

        if (collision.tag == "Ground")
            inAir = true;
    }
}
