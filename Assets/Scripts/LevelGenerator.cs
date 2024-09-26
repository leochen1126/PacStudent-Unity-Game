using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject outsideCornerPrefab;
    public GameObject outsideWallPrefab;
    public GameObject insideCornerPrefab;
    public GameObject insideWallPrefab;
    public GameObject pelletPrefab;
    public GameObject powerPelletPrefab;
    public GameObject tJunctionPrefab;

    public int[,] levelMap =
    {
        {1,2,2,2,2,2,2,2,2,2,2,2,2,7},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,4},
        {2,6,4,0,0,4,5,4,0,0,0,4,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,3},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,5},
        {2,5,3,4,4,3,5,3,3,5,3,4,4,4},
        {2,5,3,4,4,3,5,4,4,5,3,4,4,3},
        {2,5,5,5,5,5,5,4,4,5,5,5,5,4},
        {1,2,2,2,2,1,5,4,3,4,4,3,0,4},
        {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
        {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
        {0,0,0,0,0,2,5,4,4,0,3,4,4,0},
        {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
        {0,0,0,0,0,0,5,0,0,0,4,0,0,0}
    };

    public float tileSize = 1f;  // Size of each tile in Unity units

    void Start()
    {
        // Delete existing level layout (if any)
        DeleteExistingLevel();

        // Generate the level based on the levelMap array
        GenerateLevel();
    }

    void DeleteExistingLevel()
    {
        // Remove all objects tagged as "Level" in the scene
        GameObject[] existingLevelPieces = GameObject.FindGameObjectsWithTag("Level");
        foreach (GameObject piece in existingLevelPieces)
        {
            Destroy(piece);
        }
    }

    void GenerateLevel()
    {
        for (int y = 0; y < levelMap.GetLength(0); y++)
        {
            for (int x = 0; x < levelMap.GetLength(1); x++)
            {
                Vector3 position = new Vector3(x * tileSize, -y * tileSize, 0); // Calculate the position for each tile
                PlaceTile(levelMap[y, x], position);
            }
        }

        // After generating the top-left quadrant, mirror it horizontally and vertically
        MirrorLevel();
    }

    void PlaceTile(int tileID, Vector3 position)
    {
        GameObject tilePrefab = null;

        // Choose the appropriate prefab based on tileID
        switch (tileID)
        {
            case 1: tilePrefab = outsideCornerPrefab; break;
            case 2: tilePrefab = outsideWallPrefab; break;
            case 3: tilePrefab = insideCornerPrefab; break;
            case 4: tilePrefab = insideWallPrefab; break;
            case 5: tilePrefab = pelletPrefab; break;
            case 6: tilePrefab = powerPelletPrefab; break;
            case 7: tilePrefab = tJunctionPrefab; break;
        }

        if (tilePrefab != null)
        {
            // Instantiate the tile and assign the "Level" tag so it can be easily deleted
            GameObject tile = Instantiate(tilePrefab, position, Quaternion.identity);
            tile.tag = "Level";
        }
    }

    void MirrorLevel()
    {
        int originalWidth = levelMap.GetLength(1);
        int originalHeight = levelMap.GetLength(0);

        for (int y = 0; y < originalHeight; y++)
        {
            for (int x = 0; x < originalWidth; x++)
            {
                Vector3 position = new Vector3((originalWidth + x) * tileSize, -y * tileSize, 0); // Shift right
                PlaceTile(levelMap[y, x], position);
            }
        }
    }
    void AdjustCamera()
    {
        Camera.main.orthographicSize = levelMap.GetLength(0) * tileSize / 2;
        Camera.main.transform.position = new Vector3(levelMap.GetLength(1) * tileSize / 2, -levelMap.GetLength(0) * tileSize / 2, -10);
    }

}

