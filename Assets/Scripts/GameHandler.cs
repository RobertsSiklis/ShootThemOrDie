using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private GameObject bullet;

    void Start()
    {
        GameInput.OnShoot += GameInput_OnShoot;
    }

    private void GameInput_OnShoot(object sender, System.EventArgs e) {
        Instantiate(bullet, PlayerHandler.Instance.transform.position, Quaternion.identity);
    }


}
