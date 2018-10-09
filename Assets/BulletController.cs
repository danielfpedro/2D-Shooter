using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 3f;
    public bool horizontalShot = true;
    public int shotMultiplier = 1;
    
    void Start()
    {
        Destroy(gameObject, 3);

    }

    // Update is called once per frame
    void Update()
    {
        if (horizontalShot)
        {
            transform.position += (Vector3.right * shotMultiplier) * speed * Time.deltaTime;
        } else {
            transform.position += (Vector3.up * shotMultiplier) * speed * Time.deltaTime;
        }
    }
}
