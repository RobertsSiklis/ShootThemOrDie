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

    private Vector2 targetDirection;
    private Enemy myenemy;

    private void Start() {
        Enemy.OnEnemyDestroyed += EnemyHandler_OnEnemyDestroyed;
    }

    private void EnemyHandler_OnEnemyDestroyed(object sender, EventArgs e) {
        if (this != null) {
            Destroy(gameObject);
        }
        
    }

    private void Update() {
        Debug.Log(targetDirection + " " + gameObject.name);
        SetEnemyPosition();
        SetDirectionFromBulletPositionToEnemyPosition(transform);
        NormalizeTargetDirection();
        OnCollisionDestroy();
        
    }

    public void SetEnemy(Enemy enemy) {
        myenemy = enemy;
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

    private void OnCollisionDestroy() {
        if (DrawBulletCollisionRay()) {
            OnBulletCollision?.Invoke(this, new OnBulletCollisionEventArgs() {
                enemy = DrawBulletCollisionRay().gameObject
            });
            Destroy(gameObject);      
        }
    }

    private GameObject DrawBulletCollisionRay() {
        float rayDistance = 0.25f;
        RaycastHit2D ray = Physics2D.Raycast(transform.position, targetDirection, rayDistance);
        if (ray.collider != null) {
            return ray.collider.gameObject;
        }
        return null;
    }

    public Vector2 GetTargetDirection() {
        return targetDirection;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawRay(transform.position, transform.up * 0.25f);
    }

}
