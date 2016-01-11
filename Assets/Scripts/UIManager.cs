using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

    public Text moneyText;
    public Text healthText;

    public void UpdateHealthText()
    {
        healthText.text = GameManager.instance.health.ToString();
    }

    public void UpdateMoneyText()
    {
        moneyText.text = "$" + GameManager.instance.money;
    }
}
