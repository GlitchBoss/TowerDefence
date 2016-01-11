using UnityEngine;
using System.Collections;

public class Snap : MonoBehaviour {

    bool pendingTrap = false;
    bool hasTrap = false;

    Trap trap;
    GameObject trapPic;

	void OnMouseEnter()
    {
        if (hasTrap)
            return;

        if (GameManager.instance.newTrap)
        {
            trapPic = GameManager.instance._trapPic;
            trapPic.transform.position = transform.position;
            pendingTrap = true;
        }
    }

    void OnMouseExit()
    {
        pendingTrap = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0) && pendingTrap)
        {
            Instantiate(GameManager.instance._trap, transform.position, Quaternion.identity);
            Destroy(trapPic);
            hasTrap = true;
        }
        //if (pendingTrap)
        //    trap.readyToFire = false;
        //else
        //    trap.readyToFire = true;
    }
}
