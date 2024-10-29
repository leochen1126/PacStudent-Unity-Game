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
        float x = Random.Range(-10f, 10f);  // Adjust based on camera size
        float y = Random.Range(-10f, 10f);
        if (Random.value > 0.5f) x = x > 0 ? 10f : -10f;
        else y = y > 0 ? 10f : -10f;

        return new Vector2(x, y);
    }
}
