using UnityEngine;

public interface IRotetable 
{
    Rigidbody Rigidbody { get; }
    CapsuleCollider Collider { get; }

    void SetRotateDirection(Vector3 direction);
}