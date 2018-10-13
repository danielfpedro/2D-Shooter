using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public float maxHealth = 10f;
    public float currentHealth;

    public Animator animator;

    public ParticleSystem hitEffect;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    public void DoDamage(float damage, Vector3 hitPosition)
    {
        currentHealth -= damage;
        ParticleSystem effect = Instantiate(hitEffect, new Vector3(hitPosition.x, hitPosition.y, 2f), Quaternion.Euler(-80f, 90f, -90f));
        effect.Play();
        Destroy(effect, 2f);
    }

    public void Kill()
    {
        animator.SetTrigger("Died");
    }
}
