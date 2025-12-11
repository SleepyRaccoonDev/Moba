using UnityEngine;
using UnityEngine.AI;

public class NavMeshController : Controller
{
    private const int StartCornerIndex = 0;
    private const int TargetCornerIndex = 1;

    private IMovable _movable;
    private int _agentTypeID;

    private NavMeshPath _path = new NavMeshPath();

    public NavMeshController(IMovable character, int agentTypeID)
    {
        StopDistance = character.Collider.radius + 1.5f;

        _movable = character;
        _agentTypeID = agentTypeID;
    }

    protected override void UpdateInputsLogic(Vector3 targetPosition)
    {
        SetTatgetPoint(targetPosition);
    }

    protected override void UpdateBehaviourLogic()
    {
        _movable.SetMoveDirection(GetDirection());
    }

    protected Vector3 GetDirection()
    {
        if (Vector3.Distance(_movable.Rigidbody.position, TargetPoint) < StopDistance)
        {
            return Vector3.zero;
        }

        if (NavMeshUtils.TryGetPath(_movable.Rigidbody.position, TargetPoint, GetNavMeshQueryFilter(), _path) == false
            || NavMeshUtils.IsPathExist(_path) == false)
            return Vector3.zero;

        return GetCharacterDirection();
    }

    private NavMeshQueryFilter GetNavMeshQueryFilter()
    {
        NavMeshQueryFilter filter = new NavMeshQueryFilter();
        filter.agentTypeID = _agentTypeID;
        filter.areaMask = NavMesh.AllAreas;

        return filter;
    }

    private Vector3 GetCharacterDirection()
    {
        return (_path.corners[TargetCornerIndex] - _path.corners[StartCornerIndex]).normalized;
    }

    protected virtual void SetTatgetPoint(Vector3 targetPosition)
    {
        TargetPoint = targetPosition;
    }
}