using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Look : MonoBehaviour
{
    private Controls controls;
    // Base values and variables
    private float MouseSensitivity = 100f;
    private float xRotation = 0f;

    private Vector2 MouseLook;
    private Transform Camera;

    private void Awake() {
        // Startup function
        Camera = transform.parent;
        controls = new Controls();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    // Runtime parent block
    {
        Main();
    }

    private void Main() {
        // Main runtime
        // Fetch raw mouse data
        MouseLook = controls.Player.Look.ReadValue<Vector2>();
        float mouseX = MouseLook.x * MouseSensitivity * Time.deltaTime;
        float mouseY = MouseLook.y * MouseSensitivity * Time.deltaTime;
        // Correspond data to player's rotation
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation,-90f,90);
        // Apply rotation
        transform.localRotation = Quaternion.Euler(xRotation,0,0);
        Camera.Rotate(Vector3.up * mouseX);
    }
    // Obligatory control toggle
    private void OnEnable() {
        controls.Enable();
    }

    private void onDisable() {
        controls.Disable();
    }
}
