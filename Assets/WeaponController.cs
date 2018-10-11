using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public HeroController hero;
    public GameObject bullet;

    public LayerMask canBeShoted;

    public ParticleSystem impactEffect;

    public LineRenderer lineRenderer;

    // private AudioSource audioSource;
    public AudioClip audioClip;

    [Range(0.1f, 30f)]
    public float fireRate = 10f;
    private float nextFire;

    void Start()
    {
    }

    void Update()
    {
 
        Debug.DrawRay(transform.position, transform.right * 100f);

        if (Input.GetButton("Fire1") && Time.time >= nextFire)
        {
            nextFire = Time.time + 1f / fireRate;
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.volume = .5f;
        audioSource.pitch = Random.Range(.6f, 1f);
        audioSource.playOnAwake = false;
        audioSource.Play();
        Destroy(audioSource, audioClip.length);

        Debug.Log("Shootin");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, canBeShoted);

        if (hit)
        {
        } else {
        }

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position + transform.right * 1f);

        lineRenderer.enabled = true;

        yield return 0;

        lineRenderer.enabled = false;
    }
}
