using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    static public event EventHandler OnEnemyDestroyed;
    private void Start() {
        Bullet.OnBulletCollision += Bullet_OnBulletCollision;
    }

    private void Bullet_OnBulletCollision(object sender, Bullet.OnBulletCollisionEventArgs e) {
        OnEnemyDestroyed?.Invoke(this, EventArgs.Empty);
        Destroy(e.enemy);
    }
}
