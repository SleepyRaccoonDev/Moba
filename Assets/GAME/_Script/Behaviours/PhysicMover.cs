using UnityEngine;

public class PhysicMover : IPhysicBehaviour
{
    private Rigidbody _rigidbody;
    private float _forceMove;

    public PhysicMover(float speedRotation)
    {
        _forceMove = speedRotation;
    }

    public void Perform(Vector3 direction)
    {
        _rigidbody.velocity = direction * _forceMove;
    }

    public void SetRigitbody(Rigidbody rigidbody) => _rigidbody = rigidbody;
}