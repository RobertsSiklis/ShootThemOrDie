using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;
    private Vector2 inputVector;

    // Update is called once per frame
    void Update()
    {
        inputVector = GameInput.Instance.GetMovementVectorNormalized();
        transform.position += movementSpeed * Time.deltaTime * (Vector3)inputVector;
    }
}
