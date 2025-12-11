using UnityEngine;

public class PhysicRotator
{
    private Rigidbody _rigidbody;
    private float _forceRotation;

    public PhysicRotator(Rigidbody rigidbody, float speedRotation)
    {
        _rigidbody = rigidbody;
        _forceRotation = speedRotation;
    }

    public void Rotate(Vector3 moveDirection)
    {
        _rigidbody.angularVelocity = Vector3.zero;

        Vector3 targetDir = Vector3.ProjectOnPlane(moveDirection, _rigidbody.transform.up).normalized;
        Vector3 forward = _rigidbody.transform.forward;

        float angle = Vector3.Angle(forward, targetDir);
        float sign = Mathf.Sign(Vector3.Cross(forward, targetDir).y);

        float signedAngle = angle * sign;
        float normalized = signedAngle / 180f;

        _rigidbody.AddTorque(_rigidbody.transform.up * normalized * _forceRotation, ForceMode.Acceleration);
    }
}