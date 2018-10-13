using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public float damage = 4.5f;
    public LayerMask canBeShoted;

    public SpriteRenderer verticalMuzzle;
    public SpriteRenderer horizontalMuzzle;
    public SpriteRenderer muzzle;

    // private AudioSource audioSource;
    public AudioClip audioClip;

    [Range(0.1f, 30f)]
    public float fireRate = 10f;
    public float nextFire;

    public bool mute = false;

    void Start()
    {
        verticalMuzzle.enabled = false;
        horizontalMuzzle.enabled = false;
        muzzle = horizontalMuzzle;
    }

    void Update()
    {
 
        Debug.DrawRay(transform.position, transform.right * 100f);
        if (Input.GetButtonUp("Fire1"))
        {
            nextFire = Time.time;
        }
        if (Input.GetButton("Fire1") && Time.time >= nextFire)
        {
            nextFire = Time.time + 1f / fireRate;
            muzzle.enabled = false;
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        if (!mute)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = audioClip;
            audioSource.volume = .5f;
            audioSource.pitch = Random.Range(.6f, 1f);
            audioSource.playOnAwake = false;
            audioSource.Play();
            Destroy(audioSource, audioClip.length);
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, canBeShoted);

        if (hit)
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                hit.transform.gameObject.GetComponent<HealthManager>().DoDamage(damage, hit.point);
            } else if(hit.transform.CompareTag("SolidObject")) 
            {
                hit.transform.gameObject.GetComponent<SolidObject>().Hit(hit.point);
            }
        } else {
        }

        muzzle.enabled = true;

        yield return .5f;

        muzzle.enabled = false;
    }
}
