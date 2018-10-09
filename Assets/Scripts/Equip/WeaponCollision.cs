using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollision : MonoBehaviour {

    public float attack;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name + " collided with " + gameObject.name + ".");
        //Construct knockback params
        float[] KnockbackAttack = new float[5];

        //Damage
        KnockbackAttack[0] = attack;
        //Pos
        KnockbackAttack[1] = transform.position.x;
        KnockbackAttack[2] = transform.position.y;
        KnockbackAttack[3] = transform.position.z;
        //Angle
        //KnockbackAttack[4] = transform.eulerAngles.y;

        //Send
        collision.transform.SendMessage(("CmdTakeDamage"), KnockbackAttack, SendMessageOptions.DontRequireReceiver);
    }
}
