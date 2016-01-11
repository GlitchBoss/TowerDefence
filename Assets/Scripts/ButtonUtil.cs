using UnityEngine;
using System.Collections;

public class ButtonUtil : MonoBehaviour {

	public void SpawnEnemies(int numOfEnemies)
    {
        GameManager.instance.SpawnEnemies(numOfEnemies);
    }
}
