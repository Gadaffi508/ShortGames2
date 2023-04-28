using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public EnemyProducer enemyProducer;
    public GameObject playerPrefab;

    void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        player.onPlayerDeath += onPlayerDeath;
    }

    void onPlayerDeath(PlayerMovement player)
    {
        enemyProducer.SpawnEnemies(false);
        Destroy(player.gameObject);

        Invoke("restartGame", 3);
    }
    void restartGame()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            Destroy(enemy);
        }

        var playerObject = Instantiate(playerPrefab, new Vector3(0, 0.5f, 0), Quaternion.identity) as GameObject;
        var cameraRig = Camera.main.GetComponent<CameraRig>();
        cameraRig.target = playerObject;
        enemyProducer.SpawnEnemies(true);
        playerObject.GetComponent<PlayerMovement>().onPlayerDeath += onPlayerDeath;
    }

}
