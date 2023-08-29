using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerHandler : MonoBehaviour {

    static public event EventHandler<OnCollisionWithEnemyEventArgs> OnColissionWithEnemy;

    public class OnCollisionWithEnemyEventArgs : EventArgs {
        public Enemy
            enemy;
    }
    static public PlayerHandler Instance { get; private set; }

    [SerializeField] private float spawnTimer = 1f;
    private float timer;
    private RaycastHit2D rangeCollider;
    

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.LogError("More than one player!");
        }
    }

    private void Start() {
        timer = spawnTimer;
    }

    private void Update() {
        timer += Time.deltaTime;
        rangeCollider = DrawAutoAttackRange();
        if (timer >= spawnTimer) {
            timer = 0f;
            SetEnemy(CheckForEnemyInAttackRange());
        }  
    }

    private RaycastHit2D DrawAutoAttackRange() {
        float autoAttackRange = 5f;
        return Physics2D.CircleCast(transform.position, autoAttackRange, Vector2.zero);
    }

    private Enemy CheckForEnemyInAttackRange() {
        if (rangeCollider.collider != null) {
            return rangeCollider.collider.GetComponent<Enemy>();
        }
        return null;
    }

    private void SetEnemy(Enemy enemy) {
        if (enemy != null) {
            OnColissionWithEnemy?.Invoke(this, new OnCollisionWithEnemyEventArgs {
                enemy = enemy
            });
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, 5f);
    }

}
