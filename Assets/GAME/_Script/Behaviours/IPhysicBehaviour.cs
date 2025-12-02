using UnityEngine;

public interface IPhysicBehaviour
{
    void Perform(Vector3 direction);

    void SetRigitbody(Rigidbody rigidbody);
}