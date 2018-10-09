using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float damp = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Vector3 oldPosition= new Vector3(transform.position.x, transform.position.y, -10f);
        Vector3 newPosition = new Vector3(target.position.x, target.position.y, -10f);

        transform.position = Vector3.Lerp(oldPosition, newPosition, damp);
    }
}
