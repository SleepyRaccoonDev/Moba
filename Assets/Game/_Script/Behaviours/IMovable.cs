using UnityEngine;

public interface IMovable 
{
    Rigidbody Rigidbody { get; }
    CapsuleCollider Collider { get; }

    void SetMoveDirection(Vector3 direction);
}