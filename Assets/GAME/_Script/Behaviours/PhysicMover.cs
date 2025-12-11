using UnityEngine;

public class PhysicMover
{
    private Rigidbody _rigidbody;
    private float _forceMove;

    public PhysicMover(Rigidbody rigidbody, float forceMove)
    {
        _rigidbody = rigidbody;
        _forceMove = forceMove;
    }

    public Vector3 CurrentVelocity { get; private set; }

    public void Move(Vector3 direction)
    {
        CurrentVelocity = direction * _forceMove;
        _rigidbody.velocity = CurrentVelocity;
    }
}