using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPot : Item {

    public void Interact(Stats s)
    {
        s.RecoverDamage(10);
        Destroy(gameObject);
        Debug.Log("Destroying self.");
    }

}
