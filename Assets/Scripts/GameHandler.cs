using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;

    private void Start() {
        PlayerHandler.OnColissionWithEnemy += PlayerHandler_OnColissionWithEnemy;
        Bullet.OnBulletCollision += Bullet_OnBulletCollision;
    }

    private void PlayerHandler_OnColissionWithEnemy(object sender, PlayerHandler.OnCollisionWithEnemyEventArgs e) {
            GameObject newBullet = Instantiate(bulletPrefab, PlayerHandler.Instance.transform.position, Quaternion.identity);
            Bullet actualBullet = newBullet.GetComponent<Bullet>();
            actualBullet.SetEnemy(e.enemy);
    }

    private void Bullet_OnBulletCollision(object sender, Bullet.OnBulletCollisionEventArgs e) {
        Destroy(e.enemy);
        Destroy(e.bullet);
    }

}
