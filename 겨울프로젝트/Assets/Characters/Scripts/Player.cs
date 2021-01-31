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
    //단타
    public void SlideClick()
    {
        isSlide = true;
    }
    //롱 노트
    public void SlideDown()
    {
        isSlide = true;
    }
    //버튼 땜
    public void SlideUp()
    {
        isSlide = false;

        renderer.flipX = false;
        renderer.flipY = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Attatch : " + collision.gameObject.tag);

        //Ground Check
        ////////////////////////////////////////////////////////
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
        ////////////////////////////////////////////////////////
        
        //Note Check
        if(collision.tag == "Enemy")
        {
            if(Vector3.Distance(collision.transform.position, transform.position) < 0.3f)
            {
                Debug.Log("Miss!");
                StartCoroutine("blinkInRed");
                Destroy(collision);
            }
            else if(isSlide && (Vector3.Distance(collision.transform.position, transform.position) < 0.5f 
                && Vector3.Distance(collision.transform.position, transform.position) > 0.3f))
            {
                Debug.Log("Hit!");
                StartCoroutine("blinkInBlue");
                Destroy(collision);
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

    IEnumerator blinkInRed()
    {
        renderer.color = new Color32(255, 0, 0, 255);
        yield return new WaitForSeconds(0.2f);
        renderer.color = new Color32(255, 255, 255, 255);
    }

    IEnumerator blinkInBlue()
    {
        renderer.color = new Color32(0, 0, 255, 255);
        yield return new WaitForSeconds(0.2f);
        renderer.color = new Color32(255, 255, 255, 255);
    }
}
