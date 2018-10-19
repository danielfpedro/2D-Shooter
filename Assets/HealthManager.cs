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

    public GameObject tilemapDebug;

    private GameObject[,] myGrid = new GameObject[30,30];
    public GameObject tile;
    public GameObject tilePath;
    public GameObject tileBlock;

    public Transform target;

    bool[,] tilesmap = new bool[30, 30];

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        
        for (int x = 0; x < 30; x++)
        {
            for (int y = 0; y < 30; y++)
            {
                bool isBlock = Random.Range(1, 10) == 1;
                tilesmap[x, y] = !isBlock;

                myGrid[x, y] = Instantiate(isBlock ? tileBlock : tile) as GameObject;
                myGrid[x, y].transform.position = new Vector3(x, y, 0);

            }
        }

        grid = new NesScripts.Controls.PathFind.Grid(tilesmap);

        _from = new NesScripts.Controls.PathFind.Point(1, 1);
        _to = new NesScripts.Controls.PathFind.Point(15, 15);

        // List<NesScripts.Controls.PathFind.Point> path = NesScripts.Controls.PathFind.Pathfinding.FindPath(grid, _from, _to);



    }

    // Update is called once per frame
    void Update()
    {
        NesScripts.Controls.PathFind.Point theFrom = new NesScripts.Controls.PathFind.Point((int)transform.position.x, (int)transform.position.y);
        NesScripts.Controls.PathFind.Point theTo = new NesScripts.Controls.PathFind.Point((int)target.position.x, (int)target.position.y);

        List<NesScripts.Controls.PathFind.Point> path = NesScripts.Controls.PathFind.Pathfinding.FindPath(grid, theFrom, theTo);
        // Debug.Log("Path Count" + path.Count);

        for (int x = 0; x < 30; x++)
        {
            for (int y = 0; y < 30; y++)
            {
                if (tilesmap[x,y])
                {
                    myGrid[x, y].GetComponent<SpriteRenderer>().sprite = tile.GetComponent<SpriteRenderer>().sprite;
                }
            }
        }
        for (int i = 0; i < path.Count; i++)
        {
            myGrid[path[i].x, path[i].y].GetComponent<SpriteRenderer>().sprite = tilePath.GetComponent<SpriteRenderer>().sprite;
        }
        if (path.Count > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(path[0].x, path[0].y, 0), .05f);
        }
        

        if (Time.time >= nextMove)
        {
            nextMove = Time.time + 1f / moveRate;
            // transform.position = Vector3.Lerp(transform.position, new Vector3(path[nextIndex].x, path[nextIndex].y, transform.position.z), .2f);
            // nextIndex++;
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
