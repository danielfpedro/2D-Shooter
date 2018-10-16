using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    public float speed = 20f;
    public int facingHorizontal;
    public int facingVertical;


    public bool locked = false;
    public Transform weapon;

    private Animator animator;

    public Vector2 currentPosition;

    public Vector3 upPosition = Vector3.zero;
    public Vector3 horizontalPosition;

    public float bodyHorizontalOrientation = 1f;
    public float verticalBodyHorientation = 0f;

    public bool facingRight = true;

    public GameObject verticalWeapon;
    public GameObject horizontalWeapon;

    // Start is called before the first frame update
    void Start()
    {
        horizontalPosition = weapon.transform.position;

        currentPosition = new Vector2(1, 0);

        animator = GetComponent<Animator>();
        facingHorizontal = 1;
        facingVertical = 0;
    }

    // Update is called once per frame
    void Update()
    {
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

        locked = Input.GetButton("LockAim");

        if (!locked)
        {
            if (Input.GetAxisRaw("Horizontal") != 0f)
            {
                // verticalWeapon.SetActive(false);
                // horizontalWeapon.SetActive(true);

                WeaponController weaponController = weapon.GetComponent<WeaponController>();
                // weaponController.muzzle = weaponController.horizontalMuzzle;

                float deslocation = (Input.GetAxisRaw("Horizontal") > 0) ? 1f : -1f;
                float nextPosition = deslocation;

                if ((nextPosition == -1 && facingRight) || (nextPosition == 1 && !facingRight))
                {
                    facingRight = !facingRight;
                }

                if (deslocation > 0)
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                }
                else
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0, -180, 0));
                }
                weapon.transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));
                currentPosition = new Vector3(nextPosition, 0f);


                animator.SetBool("RunningHorizontal", true);
                animator.SetBool("RunningUp", false);
                animator.SetBool("RunningDown", false);
            }
            else if (Input.GetAxisRaw("Vertical") != 0f)
            {
                // verticalWeapon.SetActive(true);
                // horizontalWeapon.SetActive(false);

                float deslocation = (Input.GetAxisRaw("Vertical") > 0) ? 1f : -1f;
                float nextPosition = deslocation;

                // SpriteRenderer weaponSprite = verticalWeapon.GetComponent<SpriteRenderer>();
                if (deslocation > 0)
                {
                    // verticalWeapon.SetActive(false);

                    animator.SetBool("RunningUp", true);
                    animator.SetBool("RunningDown", false);
                } else {
                    animator.SetBool("RunningUp", false);
                    animator.SetBool("RunningDown", true);
                }

                // WeaponController weaponController = weapon.GetComponent<WeaponController>();
                // weaponController.muzzle = weaponController.verticalMuzzle;

                // weapon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90 * nextPosition));
                // weapon.position = transform.position + upPosition;
                currentPosition = new Vector3(0f, nextPosition);

                animator.SetBool("RunningHorizontal", false);
            }
            else {
                animator.SetBool("RunningHorizontal", false);
                animator.SetBool("RunningUp", false);
                animator.SetBool("RunningDown", false);
            }
        }

        weapon.GetComponent<WeaponController>().heroPosition = currentPosition;
    }
}
