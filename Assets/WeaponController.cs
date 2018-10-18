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
    private Vector3 rayDestiny;

    public bool canHold = false;

    private AudioSource[] audioSources = new AudioSource[20];

    public AudioSource machineGunStart;
    public AudioSource machineGunLoop;
    public AudioSource machineGunEnd;
    private bool playOutsound = false;

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

    private bool playingSound = false;

    private int currentAudioSourceIndex = 0;

    public float reactivateRate;
    private float nextReactivate;

    public float soundRate;
    private float nextSoundRate;

    public AudioSource oneShotAudio;

    public bool automatic = false;

    public bool firing;

    void Start()
    {
        /**
        if (canHold)
        {
            audioSources[0] = gameObject.AddComponent<AudioSource>();
            audioSources[0].clip = audioClip;
            audioSources[0].loop = true;
        }
        else {

        }
    **/

        // instantiate audiosources on pool
        /**for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i] = gameObject.AddComponent<AudioSource>();
            audioSources[i].clip = audioClip;
            audioSources[i].loop = false;
            audioSources[i].pitch = .6f;
            audioSources[i].priority = Random.Range(1, 100);
        }**/

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

        rayDestiny = transform.right;
        if (currentVisualWeaponIndex == 1)
        {
            rayDestiny = transform.up;
        } else if (currentVisualWeaponIndex == 2)
        {
            rayDestiny = transform.up * -1f;
        }
        Debug.DrawRay(weaponsVisuals[currentVisualWeaponIndex].transform.position, rayDestiny * 100f);

        // bool theInput = (automatic) ? Input.GetButton("Fire1") : Input.GetButtonDown("Fire1");

        if (!automatic)
        {
            if (Input.GetButtonDown("Fire1") && Time.time >= nextReactivate)
            {
                nextReactivate = Time.time + 1f / reactivateRate;
                firing = true;
                oneShotAudio.PlayOneShot(oneShotAudio.clip);
                StartCoroutine(Shoot());
            }
            else
            {
                firing = false;
            }
        }
        else
        {
            if (Input.GetButton("Fire1"))
            {
                nextReactivate = Time.time + 1f / reactivateRate;
                if (!machineGunLoop.isPlaying)
                {
                    machineGunLoop.loop = true;
                    machineGunLoop.Play();
                }

                if (Time.time >= nextFire)
                {
                    nextFire = Time.time + 1f / fireRate;
                    StartCoroutine(Shoot());
                }
                firing = true;
            }

            if (Input.GetButtonUp("Fire1") && firing)
            {
                machineGunEnd.PlayOneShot(machineGunEnd.clip);
                machineGunLoop.loop = false;
                firing = false;
            }
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
        }

        RaycastHit2D hit = Physics2D.Raycast(weaponsVisuals[currentVisualWeaponIndex].transform.position, rayDestiny, canBeShooted);

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
