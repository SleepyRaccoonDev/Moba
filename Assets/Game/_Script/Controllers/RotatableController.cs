using UnityEngine;

public class RotatableController : Controller
{
    private IRotetable _rotatable;

    public RotatableController(IRotetable rotatable)
    {
        _rotatable = rotatable;
    }

    protected override void UpdateInputsLogic(Vector3 targetPosition)
    {

    }

    protected override void UpdateBehaviourLogic()
    {
        _rotatable.SetRotateDirection(_rotatable.Rigidbody.velocity);
    }
}