using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody),typeof(CapsuleCollider))]
public class Character : MonoBehaviour, IDamageable, IMovable, IRotetable
{
    [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
    [field: SerializeField] public CapsuleCollider Collider { get; private set; }
    [field: SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }
    public Animator Animator { get; private set; }
    public float Health { get; private set; }
    public bool IsTakingDamage { get; set; }

    private PhysicMover _mover;
    private PhysicRotator _rotator;

    private Vector3 _moveDirection;
    private Vector3 _rotateDirection;

    private void Awake()
    {
        Collider = GetComponent<CapsuleCollider>();
        Rigidbody = GetComponent<Rigidbody>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void Initialize(float maxHP, PhysicMover mover, PhysicRotator rotator)
    {
        Animator = GetComponentInChildren<Animator>();
 

        Health = maxHP;

        _mover = mover;
        _rotator = rotator;
    }

    private void FixedUpdate()
    {
        if (IsTakingDamage)
            return;

        _mover.Move(_moveDirection);
        _rotator.Rotate(_rotateDirection);
    }

    public void SetMoveDirection(Vector3 direction) => _moveDirection = direction;
    public void SetRotateDirection(Vector3 direction) => _rotateDirection = direction;

    public void TakeDamage(float damage)
    {
        if (damage < 0)
            return;

        IsTakingDamage = true;

        Health -= damage;

        if (Health <= 0)
        {
            SetDeadState();
            Health = 0;
        }
    }

    private void SetDeadState()
    {
        Rigidbody.isKinematic = true;
        Collider.isTrigger = true;
    }
}