using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 targetDirection;
    [SerializeField] private float bulletSpeed = 30f;
    private void Start() {
        targetDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetDirection = DirectionFromMousePositionToObjectPosition(transform);
        targetDirection = targetDirection.normalized;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, targetDirection);
    }

    private void Update() {
        OnCollision();
        transform.position += bulletSpeed * Time.deltaTime * (Vector3)targetDirection;
    }

    private Vector2 DirectionFromMousePositionToObjectPosition(Transform transform) {
        return targetDirection - (Vector2)transform.position;
    }
    
    private bool DrawBulletCollisionRay() {
        float rayDistance = 0.25f;
        return Physics2D.Raycast(transform.position, targetDirection, rayDistance);
    }

    private void OnCollision() {
        if (DrawBulletCollisionRay()) {
            Destroy(gameObject);
        }
    }
    private void OnDrawGizmos() {
        Gizmos.DrawRay(transform.position, transform.up * 0.25f);
    }

}
