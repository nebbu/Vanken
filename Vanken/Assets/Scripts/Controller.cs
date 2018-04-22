using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float dirX, dirY, movespeed, jumpspeed, MaxVelocity;
    Rigidbody2D rb;
    Animator anim;
    public float TapTime;
    public bool tapping;
    public float TimeSinceLastTap;
    public delegate void printing();
    public static event printing printer;
    // Use this for initializat ion
    public bool isDoubleTapping;
    [SerializeField]
    public DoubleTapDirection firstDirection;
    public DoubleTapDirection doubleTapDirection;
    public enum DoubleTapDirection
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
        TapTime = 0.5f; 
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

        bool tapDownDKey = Input.GetKeyDown("d");
        bool tapDownAKey = Input.GetKeyDown("a");
        
        bool holdDownDKey = Input.GetKey("d");
        bool holdDownAKey = Input.GetKey("a");

        if (tapDownDKey || tapDownAKey)
        {

            if (!tapping)
            {
                tapping = true;

                if (tapDownDKey)
                {
                    firstDirection = DoubleTapDirection.Right;
                }else if (tapDownAKey)
                {  
                    firstDirection = DoubleTapDirection.Left;
                }
                TimeSinceLastTap = Time.time;
            }
            else if ((Time.time - TimeSinceLastTap) < TapTime)
            {
                Debug.Log("Dubble Tab");
                tapping = false;
                if (tapDownDKey && firstDirection == DoubleTapDirection.Right)
                {
                    isDoubleTapping = true;
                    doubleTapDirection = DoubleTapDirection.Right;
                }else if (tapDownAKey && firstDirection == DoubleTapDirection.Left)
                {
                    isDoubleTapping = true;
                    doubleTapDirection = DoubleTapDirection.Left;
                }

            }

        }else if(holdDownDKey || holdDownAKey)
        {

        }
        else
        {
            if ((Time.time - TimeSinceLastTap) > TapTime)
            {
                tapping = false;
            }
        }

        if (doubleTapDirection == DoubleTapDirection.Right && !holdDownDKey) //Right and left unreachable?
        {
            isDoubleTapping = false;
        }
        if (doubleTapDirection == DoubleTapDirection.Left && !holdDownAKey)
        {
            isDoubleTapping = false;
        }

        if (isDoubleTapping == true)
        {
            movespeed = 5f;
        }else if (isDoubleTapping == false)
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



