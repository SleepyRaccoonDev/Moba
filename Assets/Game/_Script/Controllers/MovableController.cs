using UnityEngine;

public class MovableController : NavMeshController
{
    private IMovable _movable;

    private const string KeyForRayMask = "Floor";

    private readonly int _maxDistanceForRay = 200;

    private Camera _camera;

    private RaycastHit _hit;
    private LayerMask _mask;

    public MovableController(IMovable movable, IJumpable jumpable, int agentTypeID) :
        base (movable, jumpable, agentTypeID)
    {
        _movable = movable;

        _camera = Camera.main;

        _mask = LayerMask.GetMask(KeyForRayMask);
    }

    protected override void SetTatgetPoint(Vector3 targetPosition)
    {
        Ray ray = _camera.ScreenPointToRay(targetPosition);

        if (Physics.Raycast(ray, out _hit, _maxDistanceForRay, _mask) == false)
        {
            TargetPoint = _movable.Rigidbody.position;
        }
        else
        {
            TargetPoint = _hit.point;
        }
    }
}