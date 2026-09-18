using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GridInputManager : MonoBehaviour
{
    [SerializeField] Camera _camera;
    [SerializeField] LayerMask placementLayermask;
    [SerializeField] private InputActionAsset gridInput;

    Vector3 lastPosition;

    public event Action OnClicked, OnExit;

    private InputAction setAction;
    private InputAction exitAction;

    private void Awake()
    {
        // el input
        setAction = gridInput.FindAction("Placement/Set");
        exitAction = gridInput.FindAction("Placement/Exit");

        if (setAction == null)
            Debug.LogError("No se encontró la acción Placement/Set");

        if (exitAction == null)
            Debug.LogError("No se encontró la acción Placement/Exit");
    }

    public bool IsPointerOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
    public Vector3 GetSelectedMapPosition()
    {
        //raycast de la camara al terreno especifico de la layer
        Vector3 moursePos = Input.mousePosition;
        moursePos.z = _camera.nearClipPlane;

        Ray ray = _camera.ScreenPointToRay(moursePos);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 100, placementLayermask))
        {
            lastPosition = hit.point;
        }
        return lastPosition;
    }
    private void HandleSet(InputAction.CallbackContext ctx)
    {
        OnClicked?.Invoke();
    }

    private void HandleExit(InputAction.CallbackContext ctx)
    {
        OnExit?.Invoke();
    }

    private void OnEnable()
    {
        setAction?.Enable();
        exitAction?.Enable();

        setAction.performed += HandleSet;
        exitAction.performed += HandleExit;
    }

    private void OnDisable()
    {
        setAction.performed -= HandleSet;
        exitAction.performed -= HandleExit;

        setAction?.Disable();
        exitAction?.Disable();
    }
}
