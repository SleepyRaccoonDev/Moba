using UnityEngine;

public class CharacterAnimationController  : IViewBehaviour
{
    private readonly int HitAnimationName = Animator.StringToHash("Zombie Reaction Hit");
    private readonly int HitTriggerName = Animator.StringToHash("IsHeated");
    private readonly int DieBoolName = Animator.StringToHash("IsDied");
    private readonly int VelocityXKey = Animator.StringToHash("VelocityX");
    private readonly int VelocityYKey = Animator.StringToHash("VelocityZ");
    private readonly int InJumpProcessKey = Animator.StringToHash("InJumpProcess");

    private Character _character;
    private Animator _animator;

    private float _maxHP;

    private bool _isHitPlaying = false;

    public CharacterAnimationController(Character character, float maxHP)
    {
        _character = character;
        _animator = character.Animator;
        _maxHP = maxHP;
    }

    public void Perform()
    {
        _animator.SetBool(InJumpProcessKey, _character.IsInJumpProcess);

        var state = _animator.GetCurrentAnimatorStateInfo(0);

        if (ProcessDeathState())
            return;

        if (ProcessHitAnimation(state))
            return;

        UpdateLowHPLayer();

        ProcessMovementAnimation();
    }

    private bool ProcessHitAnimation(AnimatorStateInfo state)
    {
        if (_isHitPlaying)
        {
            if (state.shortNameHash == HitAnimationName && state.normalizedTime >= 1f)
            {
                _isHitPlaying = false;
            }

            return true;
        }

        if (_character.IsTakingDamage)
        {
            _animator.SetTrigger(HitTriggerName);
            _isHitPlaying = true;
            return true;
        }

        return false;
    }

    private bool ProcessDeathState()
    {
        if (_character.Health <= 0)
        {
            _animator.SetBool(DieBoolName, true);
            return true;
        }

        return false;
    }

    private void UpdateLowHPLayer()
    {
        bool lowHP = _character.Health / _maxHP <= .3f;
        _animator.SetLayerWeight(1, lowHP ? 1 : 0);
    }

    private void ProcessMovementAnimation()
    {
        var velocity = _character.Rigidbody.velocity;

        float angle = Vector3.Dot(_character.transform.forward, velocity.normalized);
        _animator.SetFloat(VelocityXKey, velocity.magnitude * angle);

        var cross = Vector3.Cross(_character.transform.forward, velocity);
        var sign = Mathf.Sign(cross.y);

        _animator.SetFloat(VelocityYKey, cross.magnitude * -sign, 0.6f, Time.deltaTime);
    }
}