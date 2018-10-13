using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidObject : MonoBehaviour
{
    public ParticleSystem hitEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hit(Vector3 hitPoint)
    {
        ParticleSystem hit = Instantiate(hitEffect, new Vector3(hitPoint.x - .05f, hitPoint.y, hitPoint.z), Quaternion.Euler(-150f, 90f, -90f));
        hit.Play();
    }
}
