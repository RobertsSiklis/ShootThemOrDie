using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float spawnTime = 0f;
    private float spawnTimer = 0f;

    void Start()
    {
        PlayerHandler.OnColissionWithEnemy += PlayerHandler_OnColissionWithEnemy;
    }

    private void Update() {
        spawnTimer += Time.deltaTime;
    }

    private void PlayerHandler_OnColissionWithEnemy(object sender, PlayerHandler.OnCollisionWithEnemyEventArgs e) {
        if (spawnTimer >= spawnTime) {
            spawnTime = 1f;
            spawnTimer = 0f;
            Instantiate(bullet, PlayerHandler.Instance.transform.position, Quaternion.identity);
        }
    }

}
