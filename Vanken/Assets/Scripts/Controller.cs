using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float dirX, dirY, movespeed, jumpspeed, MaxVelocity;
    Rigidbody2D rb;
    Animator anim;
    float TapTime;
    bool tapping;
    float TimeSinceLastTap;
    public delegate void printing();
    public static event printing printer;
    // Use this for initializat ion
    bool isDoubleTapping;
    DoubleTapDirection theDirection;
    enum DoubleTapDirection
    {
        Right,
        Left
    }
    void Start()
    {
        movespeed = .75f;
        anim = GetComponent<Animator>();
        jumpspeed = 10f;
        rb = GetComponent<Rigidbody2D>();
        rb.drag = 10;
        TapTime = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        // dirX = Input.GetAxisRaw("Horizontal");
        // dirY = Input.GetAxisRaw("Vertical");    
        //rb.AddForce(transform.up*jumpspeed);
        //transform.position = new Vector2(transform.position.x + dirX, transform.position.y);
        if (dirX != 0 && !anim.GetCurrentAnimatorStateInfo(0).IsName("hit"))
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
        if (Input.GetButton("Fire1") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Kicce"))
        {
            anim.SetBool("isWalking", false);
            anim.SetTrigger("hit");
        }
        //the above is irrelevant for the time being
        Inputs();

    }
    void Inputs()
    {
        if (Input.GetKeyDown("d") || Input.GetKeyDown("a"))
        {

            if (!tapping)
            {
                tapping = true;

            }
            if ((Time.time - TimeSinceLastTap) < TapTime)
            {
                Debug.Log("Dubble Tab");
                tapping = false;
                //StartCoroutine(DoubleTap());
                isDoubleTapping = true;
                if (Input.GetKey("d"))
                {
                    theDirection = DoubleTapDirection.Right;
                }
                if (Input.GetKey("a"))
                {  //might need elif or else? Its saying ; expected after ("a")) if I use else
                    theDirection = DoubleTapDirection.Left;
                }

            }
            TimeSinceLastTap = Time.time;

        }
        if (theDirection == DoubleTapDirection.Right && !Input.GetKey("d")) //Right and left unreachable?
        {
            isDoubleTapping = false;
        }
        if (theDirection == DoubleTapDirection.Left && !Input.GetKey("a"))
        {
            isDoubleTapping = false;
        }
        if (isDoubleTapping == true)
        {
            movespeed = 5f;
        }
        if (isDoubleTapping == false)
        {
            movespeed = 1f;
        }
        dirX = Input.GetAxisRaw("Horizontal");
        dirY = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(rb.velocity.x + dirX * movespeed, rb.velocity.y);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, 15);

    }
    /* IEnumerator DoubleTap()
     {



         //yield return new WaitForSecondsRealtime(.5f);
     }
     }*/
}



