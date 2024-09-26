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

    public int[,] levelNewMap =
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

    public float tileSize = 1f; 

    void Start()
    {
        DeleteExistingLevel();// Disable current level
        // Generate the level based on the levelNewMap array
        GenerateLevel();
    }

    void DeleteExistingLevel()
    {
        GameObject[] existingLevelPieces = GameObject.FindGameObjectsWithTag("Level");
        foreach (GameObject piece in existingLevelPieces)
        {
            Destroy(piece);
        }
    }

    void GenerateLevel()
    {
        for (int y = 0; y < levelNewMap.GetLength(0); y++)
        {
            for (int x = 0; x < levelNewMap.GetLength(1); x++)
            {
                Vector3 position = new Vector3(x * tileSize, -y * tileSize, 0); 
                PlaceTile(levelNewMap[y, x], position);
            }
        }
        MirrorLevel();
    }

    void PlaceTile(int tileID, Vector3 position)
    {
        GameObject tilePrefab = null;
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
            GameObject tile = Instantiate(tilePrefab, position, Quaternion.identity);
            tile.tag = "Level";
        }
    }

    void MirrorLevel()
    {
        int oldWidth = levelNewMap.GetLength(1);
        int oldHeight = levelNewMap.GetLength(0);

        for (int y = 0; y < oldHeight; y++)
        {
            for (int x = 0; x < oldWidth; x++)
            {
                Vector3 position = new Vector3((oldWidth + x) * tileSize, -y * tileSize, 0); 
                PlaceTile(levelNewMap[y, x], position);
            }
        }
    }
    void AdjustCamera()
    {
        Camera.main.orthographicSize = levelNewMap.GetLength(0) * tileSize / 2;
        Camera.main.transform.position = new Vector3(levelNewMap.GetLength(1) * tileSize / 2, -levelNewMap.GetLength(0) * tileSize / 2, -10);
    }

}

