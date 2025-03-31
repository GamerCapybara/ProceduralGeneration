using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction movementAction;
    private float _speed = 5f;
    
    private void OnEnable()
    {
        movementAction.Enable();
    }

    private void OnDisable()
    {
        movementAction.Disable();
    }

    private void Update()
    {
        Vector2 dir = movementAction.ReadValue<Vector2>();
        transform.position += _speed * Time.deltaTime * new Vector3(dir.x, dir.y, 0);
    }
}
