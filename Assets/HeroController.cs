using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    public float speed = 20f;
    public int facingHorizontal;
    public int facingVertical;

    public Transform weapon;

    private Animator animator;

    public Vector2 currentPosition;



    public float bodyHorizontalOrientation = 1f;
    public float verticalBodyHorientation = 0f;

    public bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {

        Vector3 theScale = transform.localScale;
        theScale.x = -1;
        transform.localScale = theScale;

        currentPosition = new Vector2(1, 0);

        animator = GetComponent<Animator>();
        facingHorizontal = 1;
        facingVertical = 0;
    }

    // Update is called once per frame
    void Update()
    {
        /**
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            facingHorizontal = 1;
            facingVertical = 0;
        }
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            facingHorizontal = -1; facingVertical = 0;
            facingVertical = 0;
        }
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
            facingVertical = -1;
        }
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
            facingVertical = 1;
        }

        if (Input.GetAxisRaw("Vertical") > 0)
        {
            animator.SetBool("RunningUp", true);
        }
        else
        {
            animator.SetBool("RunningUp", false);
        }

        if (Input.GetAxisRaw("Vertical") < 0)
        {
            animator.SetBool("RunningDown", true);
        }
        else
        {
            animator.SetBool("RunningDown", false);
        }

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            animator.SetBool("Running", true);
            animator.SetBool("RunningUp", false);
        }
        else
        {
            animator.SetBool("Running", false);
        }**/

        if (Input.GetAxisRaw("Horizontal") != 0f)
        {
            float deslocation = (Input.GetAxisRaw("Horizontal") > 0) ? 1f : -1f;
            transform.position += (Vector3.right * deslocation) * speed * Time.deltaTime;
        }
        if (Input.GetAxisRaw("Vertical") != 0f)
        {
            float deslocation = (Input.GetAxisRaw("Vertical") > 0) ? 1f : -1f;
            transform.position += (Vector3.up * deslocation) * speed * Time.deltaTime;
        }

        if (Input.GetAxisRaw("Horizontal") != 0f)
        {
            float deslocation = (Input.GetAxisRaw("Horizontal") > 0) ? 1f : -1f;
            float nextPosition = deslocation;
            bool flipou = false;
            if ((nextPosition == -1 && facingRight) || (nextPosition == 1 && !facingRight))
            {
                FlipBodyHorizontaly();
                facingRight = !facingRight;
                flipou = true;
            }

            if (currentPosition.y != 0)
            {
                FlipWeaponHorizontal(nextPosition, flipou);
            }
            currentPosition = new Vector3(nextPosition, 0f);
        }
        else if (Input.GetAxisRaw("Vertical") != 0f)
        {
            float deslocation = (Input.GetAxisRaw("Vertical") > 0) ? 1f : -1f;
            float nextPosition = deslocation;
            if (nextPosition != currentPosition.y)
            {
                FlipWeaponVertical(nextPosition);
            }
            
            currentPosition = new Vector3(0f, nextPosition);
        }
    }

    void FlipBodyHorizontaly()
    {
        transform.Rotate(0f, 180f, 0f);
    }
    void FlipWeaponHorizontal(float nextPosition, bool flipou)
    {
        if (currentPosition.y == 1)
        {
            if (flipou)
            {
                weapon.Rotate(0f, 0f, 90f);
            }
            else {
                weapon.Rotate(0f, 0f, 90f * nextPosition);
            }
        }
        else {
            if (flipou)
            {
                weapon.Rotate(0f, 0f, -90f);
            }
            else
            {
                weapon.Rotate(0f, 0f, -90f * nextPosition);
            }
        }
        

    }
    void FlipWeaponVertical(float nextPosition)
    {
        if (currentPosition.x != 0)
        {
            weapon.Rotate(0f, 0f, -90f * nextPosition);
        } else
        {
            weapon.Rotate(0f, 0f, 180f * nextPosition);
        }
    }
}
