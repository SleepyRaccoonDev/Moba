using UnityEngine;

public class MovableController : NavMeshController
{
    private IMovable _movable;

    private const string KeyForRayMask = "Floor";

    private readonly int _maxDistanceForRay = 200;

    private Camera _camera;

    private RaycastHit _hit;
    private LayerMask _mask;

    public MovableController(IMovable movable, int agentTypeID) : base (movable, agentTypeID)
    {
        _movable = movable;

        StopDistance = movable.Collider.radius + .1f;

        _camera = Camera.main;

        _mask = LayerMask.GetMask(KeyForRayMask);
    }

    protected override void UpdateInputsLogic(Vector3 targetPosition)
    {
        SetTatgetPoint(targetPosition);
    }

    protected override void UpdateBehaviourLogic()
    {
        _movable.SetMoveDirection(GetDirection());
    }

    protected override void SetTatgetPoint(Vector3 targetPosition)
    {
        Ray ray = _camera.ScreenPointToRay(targetPosition);

        if (Physics.Raycast(ray, out _hit, _maxDistanceForRay, _mask) == false)
        {
            TargetPoint = Vector3.zero;
        }
        else
        {
            TargetPoint = _hit.point;
        }
    }
}