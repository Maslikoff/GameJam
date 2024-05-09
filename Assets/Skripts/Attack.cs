using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int attackDamage = 10;
    public Vector2 knockback = Vector2.zero;

    private void OnTriggerEnter(Collider collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null)
        {
            Vector2 deliveredKnockback = transform.position.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y); 

            bool goHit = damageable.Hit(attackDamage, deliveredKnockback);

            if (goHit)
                Debug.Log(collision.name + "hit for" + attackDamage);
        }
    }
}
