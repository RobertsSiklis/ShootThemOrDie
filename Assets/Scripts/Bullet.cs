using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 targetDirection;
    private float bulletSpeed = 30f;
    private void Start() {
        GameInput.OnShoot += GameInput_OnShoot;
        targetDirection = DirectionFromMousePositionToObjectPosition(transform);
        targetDirection = targetDirection.normalized;
    }

    private void GameInput_OnShoot(object sender, GameInput.OnShootEventArgs e) {
        targetDirection = e.ShotPosition;
    }

    private void Update() {
        transform.position += bulletSpeed * Time.deltaTime * (Vector3)targetDirection;
    }

    private Vector2 DirectionFromMousePositionToObjectPosition(Transform transform) {
        return targetDirection - (Vector2)transform.position;
    }

}
