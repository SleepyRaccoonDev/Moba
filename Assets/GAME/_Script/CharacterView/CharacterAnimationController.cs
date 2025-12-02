using UnityEngine;

public class CharacterAnimationController : IViewBehaviour
{
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
        var state = _animator.GetCurrentAnimatorStateInfo(0);

        if (ProcessDeathState())
            return;

        if (ProcessHitAnimation(state))
            return;

        UpdateLowHPLayer();

        ProcessMovementAnimation();
    }

    private void SetDeadState()
    {
        _character.Rigidbody.isKinematic = true;
    }

    private bool ProcessHitAnimation(AnimatorStateInfo state)
    {
        if (_isHitPlaying)
        {
            if (state.IsName("Zombie Reaction Hit") && state.normalizedTime >= 1f)
            {
                _isHitPlaying = false;
                _character.ResetDamageFlag();
            }

            return true;
        }

        if (_character.IsTakedDamage)
        {
            _animator.SetTrigger("IsHeated");
            _isHitPlaying = true;
            return true;
        }

        return false;
    }

    private bool ProcessDeathState()
    {
        if (_character.CurrentHP <= 0)
        {
            _animator.SetBool("IsDied", true);
            SetDeadState();
            return true;
        }

        return false;
    }

    private void UpdateLowHPLayer()
    {
        bool lowHP = _character.CurrentHP / _maxHP <= .3f;
        _animator.SetLayerWeight(1, lowHP ? 1 : 0);
    }

    private void ProcessMovementAnimation()
    {
        var velocity = _character.Rigidbody.velocity;

        float angle = Vector3.Dot(_character.transform.forward, velocity.normalized);
        _animator.SetFloat("VelocityX", velocity.magnitude * angle);

        var cross = Vector3.Cross(_character.transform.forward, velocity);
        var sign = Mathf.Sign(cross.y);

        _animator.SetFloat("VelocityZ", cross.magnitude * -sign, 0.6f, Time.deltaTime);
    }
}