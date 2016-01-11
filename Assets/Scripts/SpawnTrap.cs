using UnityEngine;
using System.Collections;

public class SpawnTrap : MonoBehaviour {

    public GameObject panel;

	public void Spawn(int trap)
    {
        GameManager.instance._SpawnTrap(trap);
        panel.SetActive(false);
    }
}
