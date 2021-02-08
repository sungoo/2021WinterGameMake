using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int hp = 100;
    public float jumpPower = 100;

    Animator animator;
    Rigidbody rigid;
    SpriteRenderer renderer;

    bool isJump = false;
    bool isSlide = false;
    bool overcharge = false;
    bool isHit = false;
    bool isDischarge = false;
    bool inAir = false;

    GameObject note;
    int state = 0; //0 : miss, 1 : bad, 2 : good, 3 : cool, 4 : perpect

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
        rigid = gameObject.GetComponentInChildren<Rigidbody>();
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
        if (isJump || inAir)
            return;

        isJump = true;

        rigid.velocity = Vector3.zero;

        Vector3 jumpVelocity = new Vector3(0, jumpPower, 0);
        rigid.AddForce(jumpVelocity, ForceMode.Impulse);

        animator.SetBool("isJump", true);
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

        if(state == 1)
        {
            Debug.Log("Hit!");
            StartCoroutine("blinkInBlue");
            Destroy(note.gameObject);
        }
        if(state == 0)
        {
            Debug.Log("Miss!");
            StartCoroutine("blinkInRed");
        }
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

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Attatch : " + collision.gameObject.tag);

        //Ground Check
        ////////////////////////////////////////////////////////
        if (collision.tag == "Ground" && rigid.velocity.y < 0)
        {
            inAir = false;

            if (inAir)
                return;
            else
            {
                isJump = false;
            }
        }
        ////////////////////////////////////////////////////////
        ///
        if(collision.tag == "Enemy")
        {
            note = collision.GetComponent<GameObject>();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        Debug.Log("Detatch : " + collision.tag);

        if (collision.tag == "Ground")
            inAir = true;
    }

    private void OnTriggerStay(Collider collision)
    {
        //Ground Check
        if (collision.tag == "Ground" && (isJump||inAir))
        {
            isJump = false;
            inAir = false;
            animator.SetBool("isJump", false);
            animator.SetTrigger("Ground");
        }

        //Note Check
        if (collision.tag == "Enemy")
        {
            if ((-(collision.transform.position.x - transform.position.x) < 1.1f
                && -(collision.transform.position.x - transform.position.x) >= 0.6f))
            {
                state = 1;
            }
            else if (-(collision.transform.position.x - transform.position.x) < 0.6f)
            {
                state = 0;
                Debug.Log("Miss!");
                StartCoroutine("blinkInRed");
                Destroy(collision.gameObject);
            }
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