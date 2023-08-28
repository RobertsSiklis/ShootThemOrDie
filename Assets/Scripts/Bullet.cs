using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public static event EventHandler<OnBulletCollisionEventArgs> OnBulletCollision;

    public class OnBulletCollisionEventArgs : EventArgs {
        public GameObject enemy;
    }

    [SerializeField] private float bulletSpeed = 30f;
    private Vector2 targetDirection;
    private Enemy myenemy;
    private RaycastHit2D bulletCollider;

    private void Start() {
        Enemy.OnEnemyDestroyed += EnemyHandler_OnEnemyDestroyed;
    }

    private void EnemyHandler_OnEnemyDestroyed(object sender, EventArgs e) {
        if (this != null) {
            Destroy(gameObject);
        }
        
    }

    private void Update() {
        bulletCollider = DrawBulletCollisionRay();
        OnCollisionDestroy();
        SetEnemyPosition();
        SetDirectionFromBulletPositionToEnemyPosition(transform);
        NormalizeTargetDirection();
        RotateBulletToTheTargetDirection();
        
        MoveBullet();
    }

    private RaycastHit2D DrawBulletCollisionRay() {
        float rayDistance = 0.25f;
        return Physics2D.Raycast(transform.position, targetDirection, rayDistance);
    }

    private void OnCollisionDestroy() {
            CheckForBulletCollision();
            OnBulletCollision?.Invoke(this, new OnBulletCollisionEventArgs() {
                enemy = bulletCollider.collider.gameObject
            });
            Destroy(gameObject);
    }

    private Enemy CheckForBulletCollision() {
        if (bulletCollider.collider != null) {
             return bulletCollider.collider.GetComponent<Enemy>();
        }
        return null;
    }



    private void SetEnemyPosition() {
        targetDirection = myenemy.GetEnemyPosition();
    }

    private void SetDirectionFromBulletPositionToEnemyPosition(Transform transform) {
         targetDirection -= (Vector2)transform.position;
    }

    private void NormalizeTargetDirection() {
        targetDirection = targetDirection.normalized;
    }

    private void RotateBulletToTheTargetDirection() {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, targetDirection);
    }

    private void MoveBullet() {
        transform.position += bulletSpeed * Time.deltaTime * (Vector3)targetDirection;
    }

    public void SetEnemy(Enemy enemy) {
        myenemy = enemy;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawRay(transform.position, transform.up * 0.25f);
    }

}
