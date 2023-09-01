using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 30f;
    private Vector2 targetDirection;
    private Bullet bullet;

    private void Awake() {
        bullet = GetComponent<Bullet>();
    }

    private void Update() {
            targetDirection = bullet.GetTargetDirection();
            RotateBulletToTheTargetDirection();
            MoveBullet();
    }

    private void RotateBulletToTheTargetDirection() {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, targetDirection);
    }

    private void MoveBullet() {
        transform.position += bulletSpeed * Time.deltaTime * (Vector3)targetDirection;
    }
}
