using UnityEngine;

public class InputsController
{
    private const int LeftMouseButtonIndex = 0;

    public Vector3 GetMousePosition()
    {
        return Input.mousePosition;
    }

    public bool IsLeftMouseButtonDown()
    {
        return Input.GetMouseButton(LeftMouseButtonIndex);
    }
}