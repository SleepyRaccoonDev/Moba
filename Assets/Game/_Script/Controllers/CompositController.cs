using UnityEngine;

public class CompositController : Controller
{
    private const int MoverControllerIndex = 0;

    private Controller[] _controllers;

    public CompositController(params Controller[] controllers)
    {
        _controllers = controllers;
    }

    public override Vector3 TargetPoint => _controllers[MoverControllerIndex].TargetPoint;
    public override float StopDistance => _controllers[MoverControllerIndex].StopDistance;

    public override void Enable()
    {
        base.Enable();

        foreach (var controller in _controllers)
            controller.Enable();
    }

    public override void Disable()
    {
        base.Disable();

        foreach (var controller in _controllers)
            controller.Disable();
    }

    protected override void UpdateBehaviourLogic()
    {
        foreach (var controller in _controllers)
            controller.OnFixedUpdate();
    }

    protected override void UpdateInputsLogic(Vector3 targetPosition)
    {
        foreach (var controller in _controllers)
            controller.OnUpdate(targetPosition);
    }
}