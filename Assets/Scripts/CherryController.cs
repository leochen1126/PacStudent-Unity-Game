using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    public GameObject cherryPrefab;
    public float spawnInterval = 10f;
    public float cherrySpeed = 2f;
    private Vector2 centerPoint;
    private float spawnTimer;

    void Start()
    {
        centerPoint = new Vector2(0, 0); // Center of the level
        spawnTimer = spawnInterval;
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            SpawnCherry();
            spawnTimer = spawnInterval;
        }
    }

    private void SpawnCherry()
    {
        Vector2 spawnPosition = GetRandomSpawnPosition();
        GameObject cherry = Instantiate(cherryPrefab, spawnPosition, Quaternion.identity);
        CherryMovement movement = cherry.AddComponent<CherryMovement>();
        movement.SetTarget(centerPoint, cherrySpeed);
    }

    private Vector2 GetRandomSpawnPosition()
    {
        Vector2 spawnPosition = Vector2.zero;
        float cameraHeight = Camera.main.orthographicSize * 2;
        float cameraWidth = cameraHeight * Camera.main.aspect;

        int side = Random.Range(0, 4);  // Choose a random side: 0 = left, 1 = right, 2 = top, 3 = bottom

        switch (side)
        {
            case 0:  // Left
                spawnPosition = new Vector2(-cameraWidth / 2 - 1, Random.Range(-cameraHeight / 2, cameraHeight / 2));
                break;
            case 1:  // Right
                spawnPosition = new Vector2(cameraWidth / 2 + 1, Random.Range(-cameraHeight / 2, cameraHeight / 2));
                break;
            case 2:  // Top
                spawnPosition = new Vector2(Random.Range(-cameraWidth / 2, cameraWidth / 2), cameraHeight / 2 + 1);
                break;
            case 3:  // Bottom
                spawnPosition = new Vector2(Random.Range(-cameraWidth / 2, cameraWidth / 2), -cameraHeight / 2 - 1);
                break;
        }

        return spawnPosition;
    }
}
