using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public int money;
    public int health;
    public int startingMoney;

    [HideInInspector]
    public bool newTrap = false;
    [HideInInspector]
    public GameObject _trapPic;
    [HideInInspector]
    public GameObject _trap;

    public static GameManager instance;

    UIManager UIM;
    TrapManager TrapM;

    void Awake()
    {
        if (!instance)
            instance = this;
        if (instance != this)
            Destroy(this);
        StartUp();
    }

    void StartUp()
    {
        DontDestroyOnLoad(this);
        UIM = GameObject.Find("UIManager").GetComponent<UIManager>();
        TrapM = GameObject.Find("TrapManager").GetComponent<TrapManager>();
        money = startingMoney;
        UIM.UpdateMoneyText();
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            newTrap = false;
        }
    }

    public void SpawnEnemies(int numOfEnemies)
    {
        GameObject.Find("SpawnPoint").GetComponent<SpawnEnemies>().StartSpawn(numOfEnemies);
    }

    public void LoseHealth(int amount)
    {
        health -= amount;
        if (health <= 0)
            health = 0;
        UIM.UpdateHealthText();
    }

    public void ChangeMoney(int amount)
    {
        money += amount;
        if(money <= 0)
            money = 0;
        UIM.UpdateMoneyText();
    }

    public void _SpawnTrap(int trap)
    {
        if (money < TrapM.traps[trap].GetComponent<Trap>().price)
            return;
        Vector3 p = Camera.main.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x,
                Input.mousePosition.y,
                -Camera.main.transform.position.z));
        _trapPic = Instantiate(TrapM.trapPics[trap], new Vector3(p.x, p.y, 0.0f), Quaternion.identity) as GameObject;
        newTrap = true;
        _trap = TrapM.traps[trap];
        ChangeMoney(-TrapM.traps[trap].GetComponent<Trap>().price);
    }
}
