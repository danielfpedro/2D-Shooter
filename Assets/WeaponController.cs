using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Layers")]
    public LayerMask canBeShooted;
    [Header("Damge")]
    public float damage = 5f;

    [Header("Visual Weapons")]
    public SpriteRenderer[] weaponsVisuals = new SpriteRenderer[3];
    private SpriteRenderer[] muzzlesVisuals = new SpriteRenderer[3];
    private Light[] lightVisuals = new Light[3];
    public int currentVisualWeaponIndex = 0;
    /**
    public SpriteRenderer weaponUp;
    private SpriteRenderer weaponUpMuzzle;

    public SpriteRenderer weaponDown;
    private SpriteRenderer weaponDownMuzzle;

    public SpriteRenderer weaponHorizontal;
    private SpriteRenderer weaponHorizontalMuzzle;**/

    [HideInInspector]
    public SpriteRenderer muzzle;

    [Header("Audio")]
    public AudioClip audioClip;
    public bool mute = false;

    [Header("Rate")]
    [Range(0.1f, 30f)]
    public float fireRate = 10f;

    private float nextFire;

    public Vector2 heroPosition;

    void Start()
    {
        for (int i = 0; i < weaponsVisuals.Length; i++)
        {
            muzzlesVisuals[i] = weaponsVisuals[i].transform.GetChild(0).GetComponent<SpriteRenderer>();
            muzzlesVisuals[i].enabled = false;

            lightVisuals[i] = weaponsVisuals[i].transform.GetChild(1).GetComponent<Light>();
            lightVisuals[i].enabled = false;
        }
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
            // muzzle.enabled = false;
            StartCoroutine(Shoot());
        }

        for (int i = 0; i < weaponsVisuals.Length; i++)
        {
            weaponsVisuals[i].enabled = false;
            // muzzlesVisuals[i].enabled = false;
        }

        if (heroPosition.x != 0)
        {
            weaponsVisuals[0].enabled = true;
            currentVisualWeaponIndex = 0;
        } else if (heroPosition.y > 0) {
            weaponsVisuals[1].enabled = true;
            currentVisualWeaponIndex = 1;
        } else if (heroPosition.y < 0) {
            weaponsVisuals[2].enabled = true;
            currentVisualWeaponIndex = 2;
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

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, canBeShooted);

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

        // Quando executar o async a index podeter mduado entao e gente faz um cache aqui.
        int toEnd = currentVisualWeaponIndex;
        muzzlesVisuals[toEnd].enabled = true;
        lightVisuals[toEnd].enabled = true;
        Vector3 thePosition = lightVisuals[toEnd].transform.position;
        thePosition.z = -.2f;
        lightVisuals[toEnd].transform.position = thePosition;

        yield return .5f;

        muzzlesVisuals[toEnd].enabled = false;
        lightVisuals[toEnd].enabled = false;
    }
}
