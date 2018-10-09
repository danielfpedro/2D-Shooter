using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    public float speed = 20f;
    public int facingHorizontal;
    public int facingVertical;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        facingHorizontal = 1;
        facingVertical = 0;
    }

    // Update is called once per frame
    void Update()
    {
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
        }

        transform.localScale = new Vector3(-facingHorizontal, transform.localScale.y, transform.localScale.z);
    }

    void FixedUpdate()
    {
    }

}
