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
    [SerializeField] private float bulletSpeed = 30f;
    private void Start() {
        PlayerHandler.OnColissionWithEnemy += PlayerHandler_OnColissionWithEnemy;
        EnemyHandler.OnEnemyDestroyed += EnemyHandler_OnEnemyDestroyed;
    }

    private void EnemyHandler_OnEnemyDestroyed(object sender, EventArgs e) {
        if (this != null) {
            Destroy(gameObject);
        }
        
    }

    private void Update() {
        SetDirectionFromMousePositionToObjectPosition(transform);
        NormalizeTargetDirection();
        RotateBulletToTheTargetDirection();
        OnCollisionDestroy();
        MoveBullet();
    }

    private void PlayerHandler_OnColissionWithEnemy(object sender, PlayerHandler.OnCollisionWithEnemyEventArgs e) {
        targetDirection = e.collidedEnemyPosition;
    }


    private void SetDirectionFromMousePositionToObjectPosition(Transform transform) {
         targetDirection -= (Vector2)transform.position;
    }

    private void NormalizeTargetDirection() {
        targetDirection = targetDirection.normalized;
    }

    private void RotateBulletToTheTargetDirection() {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, targetDirection);
    }

    private GameObject DrawBulletCollisionRay() {
        float rayDistance = 0.25f;
        RaycastHit2D ray = Physics2D.Raycast(transform.position, targetDirection, rayDistance);
        if (ray.collider != null) {
            return ray.collider.gameObject;
        }
        return null;
    }

    private void OnCollisionDestroy() {
        if (DrawBulletCollisionRay()) {
            OnBulletCollision?.Invoke(this, new OnBulletCollisionEventArgs() {
                enemy = DrawBulletCollisionRay().gameObject
            });
            Destroy(gameObject);      
        }
    }

    private void MoveBullet() {
        transform.position += bulletSpeed * Time.deltaTime * (Vector3)targetDirection;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawRay(transform.position, transform.up * 0.25f);
    }

}
