using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int hp = 100;
    public float jumpPower = 100;

    Animator animator;
    Rigidbody2D rigid;
    SpriteRenderer renderer;

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
        renderer = gameObject.GetComponentInChildren<SpriteRenderer>();
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

        Slide();
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

        //animator.SetBool("Jump", true);
    }

    public void Slide()
    {
        if (!isSlide)
            return;
        if (inAir)
        {
            //공중 공격
            renderer.flipX = true;
            StartCoroutine(AnimationBack());
        }
        else
        {
            //지상 공격
            renderer.flipY = true;
        }
    }

    public void SlidePress()
    {
        if (isSlide)
            return;
        isSlide = true;
    }

    public void SlideOut()
    {
        isSlide = false;

        renderer.flipX = false;
        renderer.flipY = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Attatch : " + collision.gameObject.tag);
        inAir = false;

        if (collision.tag == "Ground" && rigid.velocity.y < 0)
        {
            if (inAir)
                return;
            else
            {
                isJump = false;
                //animator.SetBool("Jump", false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Detatch : " + collision.tag);

        if (collision.tag == "Ground")
            inAir = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Ground" && (isJump||inAir))
        {
            isJump = false;
            inAir = false;
        }
    }

    IEnumerator AnimationBack()
    {
        yield return new WaitForSeconds(0.5f);
        renderer.flipX = false;
        renderer.flipY = false;
        isSlide = false;
    }
}
