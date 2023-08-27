using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    static public GameInput Instance { get; private set; }
    static public event EventHandler OnShoot;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.LogError("More than one GameInput");
        }
    }
    
    private void Update() {
        OnShootPressed();
    }

    public Vector2 GetMovementVectorNormalized() {
        Vector2 inputVector = Vector2.zero;

        if (Input.GetKey(KeyCode.W)) {
            inputVector.y = 1;
        }

        if (Input.GetKey(KeyCode.A)) {
            inputVector.x = -1;
        }

        if (Input.GetKey(KeyCode.S)) {
            inputVector.y = -1;
        }

        if (Input.GetKey(KeyCode.D)) {
            inputVector.x = 1;
        }

        return inputVector.normalized;
    }

    private void OnShootPressed() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            OnShoot?.Invoke(this, EventArgs.Empty );
        }
    }

}
