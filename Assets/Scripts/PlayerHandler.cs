using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerHandler : MonoBehaviour {

    static public event EventHandler<OnCollisionWithEnemyEventArgs> OnColissionWithEnemy;
    public class OnCollisionWithEnemyEventArgs : EventArgs {
        public Vector2 collidedEnemyPosition;
    }
    static public PlayerHandler Instance { get; private set; }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.LogError("More than one player!");
        }
    }

    private void Update() {
        DrawAutoAttackRange();
    }

    private void DrawAutoAttackRange() {
        float autoAttackRange = 5f;
        RaycastHit2D ray = Physics2D.CircleCast(transform.position, autoAttackRange, Vector2.zero);
        if (ray.collider != null) {
            Vector2 enemyPosition = ray.collider.transform.position;
            OnColissionWithEnemy?.Invoke(this, new OnCollisionWithEnemyEventArgs() {
                collidedEnemyPosition = enemyPosition
            });
        }
    }
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, 5f);
    }

}
