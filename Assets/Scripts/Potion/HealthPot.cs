using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPot : Item {

    public new void Interact(GameObject go)
    {
        Stats s = go.GetComponent<Stats>();
        s.RecoverDamage(10);
        Destroy(gameObject);
        Debug.Log("Destroying self.");
    }

}
