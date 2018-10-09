using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public HeroController hero;
    public GameObject bullet;

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            GameObject bulletInstance = Instantiate(bullet);

            bulletInstance.transform.position = transform.position;
            
            BulletController bulletController = bulletInstance.GetComponent<BulletController>();

            if (hero.facingVertical != 0)
            {
                bulletController.horizontalShot = false;
                bulletController.shotMultiplier = hero.facingVertical;
                bulletInstance.transform.rotation = Quaternion.Euler(0, 0, 90);

            } else {
                bulletController.horizontalShot = true;
                bulletController.shotMultiplier = hero.facingHorizontal;
            }
        }
    }
}
