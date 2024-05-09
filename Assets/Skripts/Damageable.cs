using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int health;
    [SerializeField] private float timeSinceHit = 0;
    [SerializeField] private float invincibilityTimer = .25f;
    [SerializeField] private bool isAlive = true;

    private Animator _anim;
    private bool isInvicible;

    public int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    public int Health
    {
        get { return health; }
        set
        {
            health = value;
            //healthChanged?.Invoke(health, MaxHealth);

            if (health <= 0)
                IsAlive = false;
        }
    }

    public bool IsAlive
    {
        get { return isAlive; }
        set
        {
            isAlive = value;
            _anim.SetBool(AnimatinsStrings.isAlive, value);
        }
    }

    public bool LockVelocity
    {
        get { return _anim.GetBool(AnimatinsStrings.lockVelocity); }
        set { _anim.SetBool(AnimatinsStrings.lockVelocity, value); }
    }

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isInvicible)
        {
            if (timeSinceHit > invincibilityTimer)
            {
                isInvicible = false;
                timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime;
        }
    }

    public bool Hit(int damage, Vector2 knockback)
    {
        if (IsAlive && !isInvicible)
        {
            Health -= damage;
            isInvicible = true;
            _anim.SetTrigger(AnimatinsStrings.hitTrigger);
            LockVelocity = true;

            return true;
        }
        return false;
    }

    public bool Heal(int healthRestore)
    {
        if (IsAlive && Health < MaxHealth)
        {
            int maxHeal = Mathf.Max(MaxHealth - Health, 0);
            int actualHeal = Mathf.Min(maxHeal, healthRestore);
            Health += actualHeal;
            return true;
        }
        return false;
    }
}
