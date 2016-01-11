using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Trap : MonoBehaviour {

    public int damage;
    public int price;
    public float reloadSpeed;
    public bool readyToFire = true;

    List<Enemy> enemies;

    void Start()
    {
        enemies = new List<Enemy>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            enemies.Add(other.GetComponent<Enemy>());
            other.GetComponent<Enemy>().trap = this;
        }
    }

    void Update()
    {
        if(enemies.Count >= 1 && readyToFire)
        {
            DealDamage();
            readyToFire = false;
        }
    }

    void DealDamage()
    {
        foreach (Enemy e in enemies)
        {
            e.Damage(damage);
            StartCoroutine("Reload");
        }
    }

    IEnumerator Reload()
    {
        readyToFire = false;
        yield return new WaitForSeconds(reloadSpeed);
        readyToFire = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            RemoveEnemy(other.GetComponent<Enemy>());
        }
    }

    public void RemoveEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
    }
}
