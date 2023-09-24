using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IPlayerInput
{
    bool IsPointerOverUIObject();

    Action LeftMouseDown { get; set; }
    Action<Vector3> RightMouseDownWithMousePosition { get; set; }
    Action<Vector3> RightMouseHoldWithMousePosition { get; set; }
    Action<Vector3> RightMouseUpWithMousePosition { get; set; }
}

public class PlayerInput : MonoBehaviour, IPlayerInput
{
    static readonly List<RaycastResult> _touchResults = new List<RaycastResult>();

    public Action LeftMouseDown { get; set; }
    public Action<Vector3> RightMouseDownWithMousePosition { get; set; }
    public Action<Vector3> RightMouseHoldWithMousePosition { get; set; }
    public Action<Vector3> RightMouseUpWithMousePosition { get; set; }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            LeftMouseDown?.Invoke();
        }

        if (Input.GetMouseButtonDown(1))
        {
            RightMouseDownWithMousePosition?.Invoke(mousePos);
        }

        if (Input.GetMouseButton(1))
        {
            RightMouseHoldWithMousePosition?.Invoke(mousePos);
        }

        if (Input.GetMouseButtonUp(1))
        {
            RightMouseUpWithMousePosition?.Invoke(mousePos);
        }
    }

    public bool IsPointerOverUIObject()
    {
        _touchResults.Clear();

        Vector3 activeTouches = Input.mousePosition;

        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = activeTouches;

        EventSystem.current.RaycastAll(eventDataCurrentPosition, _touchResults);
        return _touchResults.Count > 0;
    }
}
