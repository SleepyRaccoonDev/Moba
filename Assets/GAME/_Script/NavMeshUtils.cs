using UnityEngine;
using UnityEngine.AI;

public class NavMeshUtils
{
    private static readonly int _minCountPointsInPath = 2;

    public static bool TryGetPath(
        Vector3 position,
        Vector3 target,
        NavMeshQueryFilter filter,
        NavMeshPath path)
    {
        if (NavMesh.CalculatePath(position, target, filter, path) && path.status != NavMeshPathStatus.PathInvalid)
            return true;

        return false;
    }

    public static bool IsPathExist(NavMeshPath path)
    {
        return path.corners.Length >= _minCountPointsInPath;
    }
}