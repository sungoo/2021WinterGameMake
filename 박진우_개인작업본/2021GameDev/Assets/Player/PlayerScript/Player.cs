using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int hp = 100;
    public float jumpPower = 100;

    public TimingManager theTimingManager;
    ScoreManager theScoreManager;

    Animator animator;
    Rigidbody rigid;
    public SpriteRenderer renderer;

    bool isJump = false;
    bool isSlide = false;
    bool overcharge = false;
    bool isHit = false;
    bool isDischarge = false;
    bool inAir = false;

    bool isCheck = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
        rigid = gameObject.GetComponentInChildren<Rigidbody>();
        renderer = gameObject.GetComponentInChildren<SpriteRenderer>();

        theScoreManager = FindObjectOfType<ScoreManager>();
        theTimingManager = FindObjectOfType<TimingManager>();
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

        rigid.velocity = Vector3.zero;

        Vector3 jumpVelocity = new Vector3(0, jumpPower, 0);
        rigid.AddForce(jumpVelocity, ForceMode.Impulse);

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

    public void CheckCollider() // 충돌 판정 체크용 함수 (임시)
    {
        isCheck = true;

        theTimingManager.CheckTiming();
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

    private void OnTriggerEnter(Collider collision)
    {
        // Debug.Log("Attatch : " + collision.gameObject.tag);

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
        if(collision.tag == "Note")
        {
            if(Vector3.Distance(collision.transform.position, transform.position) < 0.3f)
            {
                // Debug.Log("Miss!");
                StartCoroutine("blinkInRed");
                Destroy(collision);
            }
            else if(isJump && (Vector3.Distance(collision.transform.position, transform.position) < 0.5f 
                && Vector3.Distance(collision.transform.position, transform.position) > 0.3f))
            {
                // Debug.Log("Hit!");
                StartCoroutine("blinkInBlue");
                Destroy(collision);
            }
            // theTimingManager.CheckTiming();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        // Debug.Log("Detatch : " + collision.tag);

        if (collision.tag == "Ground")
            inAir = true;
    }

    private void OnTriggerStay(Collider collision)
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
