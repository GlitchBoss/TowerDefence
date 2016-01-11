using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public float speed;
    public int health;
    public int level;
    public float flashTime;
    public int castleDamage;
    public int value;
    public Trap trap;
    public Transform target;
    public int nextWaypoint;
    public bool spawnedByEnemy;
    public Color[] colors;

    GameObject[] waypoints;
    int maxWaypoints;
    SpawnEnemies spawner;
    GameObject[] enemyList;
    SpriteRenderer renderer;
    GameObject go;
    Enemy next;
    bool dead = false;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        colors[1] = renderer.material.color;
        waypoints = GameObject.FindGameObjectWithTag("Waypoints")
            .GetComponent<WaypointController>().waypoints;
        maxWaypoints = waypoints.Length;
        spawner = GameObject.Find("SpawnPoint").GetComponent<SpawnEnemies>();
        enemyList = spawner.enemies;
        if (spawnedByEnemy)
            return;
        nextWaypoint = 1;
        target = waypoints[0].transform;
    }

    void Remove()
    {
        trap.RemoveEnemy(this);
        if(level != 1)
        {
            go = Instantiate(enemyList[level - 2], transform.position, Quaternion.identity) as GameObject;
            next = go.GetComponent<Enemy>();
            next.spawnedByEnemy = true;
            next.target = target;
            next.nextWaypoint = nextWaypoint;
        }
        GameManager.instance.ChangeMoney(value);
        dead = true;
        Destroy(gameObject);
    }

    public void Damage(int damage)
    {
        health -= damage;
        StartCoroutine("Flash");
    }

    IEnumerator Flash()
    {
        renderer.material.color = colors[0];
        yield return new WaitForSeconds(flashTime);
        renderer.material.color = colors[1];
    }

    void Update()
    {
        if (dead)
            return;
        if (health <= 0)
            Remove();

        if(nextWaypoint == maxWaypoints && transform.position == target.position)
        {
            GameManager.instance.LoseHealth(castleDamage);
            Destroy(gameObject);
        }

        if(transform.position == target.position && nextWaypoint != maxWaypoints)
        {
            target = waypoints[nextWaypoint].transform;
            nextWaypoint++;
        }
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target.position, step);
    }
}
