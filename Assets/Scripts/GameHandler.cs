using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float spawnTime = 0f;
    private float spawnTimer = 0f;

    private void Start() {
        PlayerHandler.OnColissionWithEnemy += PlayerHandler_OnColissionWithEnemy;
    }

    private void PlayerHandler_OnColissionWithEnemy(object sender, PlayerHandler.OnCollisionWithEnemyEventArgs e) {
        if (spawnTimer >= spawnTime) {
            spawnTime = 1f;
            spawnTimer = 0f;
            GameObject newBullet = Instantiate(bulletPrefab, PlayerHandler.Instance.transform.position, Quaternion.identity);
            Bullet actualBullet = newBullet.GetComponent<Bullet>();
            if (actualBullet != null) {
                actualBullet.SetEnemy(e.enemy);
            }
        }
    }

    private void Update() {
        spawnTimer += Time.deltaTime;
    }



}
