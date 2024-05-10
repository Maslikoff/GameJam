using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Vector2 speed = new Vector3(3f, 0);
    [SerializeField] private int attackDamage = 10;
    [SerializeField] private Vector2 knockback = Vector2.zero;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _rb.velocity = new Vector2(speed.x, speed.y);
    }

    private void Update()
    {
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter(Collider collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null)
        {
            Vector2 deliveredKnockback = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);

            bool goHit = damageable.Hit(attackDamage, deliveredKnockback);

            if (goHit)
                Debug.Log(collision.name + "hit for" + attackDamage);

            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("DestroyGround"))
        {
            Destroy(collision.gameObject);
        }
    }
}
