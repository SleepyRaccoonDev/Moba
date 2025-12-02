using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    private float _maxHP;
    private IPhysicBehaviour[] _physicBehaviours;

    public Rigidbody Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    [field: SerializeField] public float CurrentHP { get; private set; }
    public bool IsTakedDamage { get; private set; }

    public void Initialize(float maxHP, params IPhysicBehaviour[] behaviours)
    {
        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponentInChildren<Animator>();
        _physicBehaviours = behaviours;
        _maxHP = CurrentHP = maxHP;
    }

    public void UpdateBehaviors(Vector3 direction)
    {
        if (IsTakedDamage)
            return;

        foreach (var behaviour in _physicBehaviours)
            behaviour.Perform(direction);
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0)
            return;

        IsTakedDamage = true;

        CurrentHP -= damage;

        if (CurrentHP < 0)
            CurrentHP = 0;
    }

    public void ResetDamageFlag()
    {
        IsTakedDamage = false;
    }
}