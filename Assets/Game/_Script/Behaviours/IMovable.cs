using UnityEngine;
using UnityEngine.AI;

public interface IMovable 
{
    Rigidbody Rigidbody { get; }
    CapsuleCollider Collider { get; }
    NavMeshAgent NavMeshAgent { get; }

    void SetMoveDirection(Vector3 direction);
}