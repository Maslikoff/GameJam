using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float airWalkSpeed = 3f;
    public float jumpImpulse = 5f;
    public float attackImpulse = 2f;
    public float rotateSpeed = 50f;
    public float timerShoot = 1f;

    [SerializeField] private bool isMoving;
    [SerializeField] private bool isRunning;
    [SerializeField] private bool isFacingRight;
    [SerializeField] private bool isGround;

    private Rigidbody _rb;
    private Animator _animator;
    private Vector3 _moveInput;
    private Damageable _damageable;
    private Quaternion targetRotation;
    private Shooter _shooter;

    private bool canShoot = true;
    private bool isFalling = true;

    public float CurrentMoveSpeed
    {
        get
        {
            if (CanMove)
            {
                if (IsMoving)
                {
                    if (isGround)
                    {
                        if (IsRunning)
                            return runSpeed;
                        else
                            return walkSpeed;
                    }
                    else
                    {
                        return airWalkSpeed;
                    }
                }
                else
                {
                    return 0;
                }
            }
            else return 0;
        }
    }


    public bool IsMoving
    {
        get
        {
            return isMoving;
        }
        private set
        {
            isMoving = value;

            if (isGround)
                _animator.SetBool(AnimatinsStrings.isMoving, value);
        }
    }

    public bool IsRunning
    {
        get
        {
            return isRunning;
        }
        set
        {
            isRunning = value;
            _animator.SetBool(AnimatinsStrings.isRunning, value);
        }
    }

    public bool IsFacingRight
    {
        get
        {
            return isFacingRight;
        }
        private set
        {
            //if (isFacingRight != value)
            //transform.rotation *= Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);

            isFacingRight = value;
        }
    }

    public bool CanMove
    {
        get { return _animator.GetBool(AnimatinsStrings.canMove); }
    }

    public bool IsAlive
    {
        get { return _animator.GetBool(AnimatinsStrings.isAlive); }
    }

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _damageable = GetComponent<Damageable>();
    }

    void FixedUpdate()
    {
        //_rb.velocity = new Vector3(_moveInput.x * CurrentMoveSpeed, _rb.velocity.y);

        Vector3 moveDirection = new Vector3(-_moveInput.x * CurrentMoveSpeed, _rb.velocity.y, 0);
        _rb.velocity = moveDirection;

        targetRotation = Quaternion.LookRotation(-_moveInput);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);

        if (isFalling && !isGround)
            _animator.SetBool(AnimatinsStrings.isFalling, isFalling);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
        isRunning = false;
        if (IsAlive)
        {
            IsMoving = _moveInput != Vector3.zero;
            //SetFacingDirection(_moveInput);
        }
        else
        {
            IsMoving = false;
            IsRunning = false;
        }
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x < 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if (moveInput.x > 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
            IsRunning = true;
        else if (context.canceled)
            IsRunning = false;

        if (!isGround)
            IsRunning = false;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && CanMove && isGround)
        {
            _animator.SetTrigger(AnimatinsStrings.jumpTrigger);
            _rb.velocity = new Vector2(_rb.velocity.x, jumpImpulse);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started && !isGround && canShoot)
        {
            StartCoroutine(ShootWithDelay());
            _animator.SetTrigger(AnimatinsStrings.attackTrigger);
            _rb.velocity = new Vector2(_rb.velocity.x, attackImpulse);
        }
    }

    private IEnumerator ShootWithDelay()
    {

        canShoot = false;
        yield return new WaitForSeconds(timerShoot);
        canShoot = true;
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        _rb.velocity = new Vector2(knockback.x, _rb.velocity.y + knockback.y);
    }

    private void OnCollisionEnter(Collision collision)
    {
        IsGroundedUpate(collision, true);
    }

    private void OnCollisionExit(Collision collision)
    {
        IsGroundedUpate(collision, false);
    }

    private void IsGroundedUpate(Collision collision, bool value)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = value;
            _animator.SetBool(AnimatinsStrings.isGround, value);
        }
        if (collision.gameObject.CompareTag("DestroyGround"))
        {
            isGround = value;
            _animator.SetBool(AnimatinsStrings.isGround, value);
        }
    }
}
