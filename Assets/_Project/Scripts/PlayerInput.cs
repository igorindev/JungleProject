using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IPlayerInput
{
    bool IsPointerOverUIObject();

    Action LeftMouseDown { get; set; }
    Action RightMouseDown { get; set; }
    Action RightMouseHold { get; set; }
}

public class PlayerInput : MonoBehaviour, IPlayerInput
{
    static readonly List<RaycastResult> _touchResults = new List<RaycastResult>();

    public Action LeftMouseDown { get; set; }
    public Action RightMouseDown { get; set; }
    public Action RightMouseHold { get; set; }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LeftMouseDown?.Invoke();
        }

        if (Input.GetMouseButtonDown(1))
        {
            RightMouseDown?.Invoke();
        }

        if (Input.GetMouseButton(1))
        {
            RightMouseHold?.Invoke();
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
