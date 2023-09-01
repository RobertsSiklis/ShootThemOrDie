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
        public GameObject bullet;
    }

    private Vector2 targetDirection;
    private Enemy enemy;
    private RaycastHit2D bulletRay;

    private void Update() {
        if (enemy != null) {
            Debug.Log(targetDirection);
            SetBulltetTargetFromEnemyPosition();
            SetDirectionFromBulletPositionToEnemyPosition(transform);
            NormalizeTargetDirection();
        }
        DrawBulletCollisionRay();
        OnBulletCollisionDestroy();
    }

    private void SetBulltetTargetFromEnemyPosition() {  
        targetDirection = enemy.GetEnemyPosition();
    }

    private void SetDirectionFromBulletPositionToEnemyPosition(Transform transform) {
         targetDirection -= (Vector2)transform.position;
    }

    private void NormalizeTargetDirection() {
        targetDirection = targetDirection.normalized;
    }

    private void OnBulletCollisionDestroy() {
        if (bulletRay.collider != null) {
            OnBulletCollision?.Invoke(this, new OnBulletCollisionEventArgs() {
                enemy = bulletRay.collider.gameObject,
                bullet = gameObject
            });  
        }
    }

    private GameObject DrawBulletCollisionRay() {
        float rayDistance = 0.25f;
        bulletRay = Physics2D.Raycast(transform.position, targetDirection, rayDistance);
        if (bulletRay.collider != null) {
            return bulletRay.collider.gameObject;
        }
        return null;
    }
    public void SetEnemy(Enemy enemy) {
        this.enemy = enemy;
    }

    public Vector2 GetTargetDirection() {
        return targetDirection;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawRay(transform.position, transform.up * 0.25f);
    }

}
