using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public float maxHealth = 10f;
    public float currentHealth;

    public Animator animator;

    private int nextIndex = 0;

    public ParticleSystem hitEffect;

    public float moveRate = 2f;
    private float nextMove;

    NesScripts.Controls.PathFind.Grid grid;
    NesScripts.Controls.PathFind.Point _from;
    NesScripts.Controls.PathFind.Point _to;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        bool[,] tilesmap = new bool[100, 100];
        for (int i = 0; i < 100; i++)
        {
            for (int y = 0; y < 100; y++)
            {
                tilesmap[i, y] = true;
            }
        }

        grid = new NesScripts.Controls.PathFind.Grid(tilesmap);

        _from = new NesScripts.Controls.PathFind.Point(1, 1);
        _to = new NesScripts.Controls.PathFind.Point(50, 50);
    }

    // Update is called once per frame
    void Update()
    {
        List<NesScripts.Controls.PathFind.Point> path = NesScripts.Controls.PathFind.Pathfinding.FindPath(grid, _from, _to);

        if (Time.time >= nextMove)
        {
            nextMove = Time.time + 1f / moveRate;
            transform.position = Vector3.Lerp(transform.position, new Vector3(path[nextIndex].x, path[nextIndex].y, transform.position.z), .2f);
            nextIndex++;
        }

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
