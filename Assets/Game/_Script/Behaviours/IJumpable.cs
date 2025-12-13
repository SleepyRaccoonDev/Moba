using UnityEngine.AI;

public interface IJumpable
{
    void ResetJump();

    void Jump(OffMeshLinkData linkData);
}