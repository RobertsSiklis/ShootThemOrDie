using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    static public event EventHandler OnEnemyDestroyed;
    private void Start() {
        Bullet.OnBulletCollision += Bullet_OnBulletCollision;
    }

    private void Bullet_OnBulletCollision(object sender, Bullet.OnBulletCollisionEventArgs e) {
        OnEnemyDestroyed?.Invoke(this, EventArgs.Empty);
        Destroy(e.enemy);
    }

    public Vector2 GetEnemyPosition() {
        return transform.position;
    }
}
