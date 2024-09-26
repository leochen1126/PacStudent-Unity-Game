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
        DeleteExistLevel();

        // Generate the level based on the levelMap array
        GenerateLevel();
    }

    void DeleteExistLevel()
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
        for (int y = 0; y < levelMap.GetLength(0); y++)  // Loop through rows
        {
            for (int x = 0; x < levelMap.GetLength(1); x++)  // Loop through columns
            {
                Vector3 position = new Vector3(x * tileSize, -y * tileSize, 0); // Calculate the position for each tile

                // Pass the tileID, position, and the current x, y coordinates to PlaceTile
                PlaceTile(levelMap[y, x], position, x, y);
            }
        }

        // After generating the top-left quadrant, mirror it horizontally and vertically
        MirrorLevel();
    }

    void PlaceTile(int tileID, Vector3 position, int x, int y)
    {
        GameObject tilePrefab = null;
        Quaternion rotation = Quaternion.identity;  // Default rotation

        // Choose the appropriate prefab based on tileID
        switch (tileID)
        {
            case 1:  // Outside corner
                tilePrefab = outsideCornerPrefab;
                rotation = DetermineCornerRotation(x, y);  // Adjust rotation for corners
                break;
            case 2:  // Outside wall
                tilePrefab = outsideWallPrefab;
                rotation = DetermineWallRotation(x, y);  // Adjust rotation for walls
                break;
            case 3:  // Inside corner
                tilePrefab = insideCornerPrefab;
                rotation = DetermineInsideCornerRotation(x, y);  // Adjust rotation for inside corners
                break;
            case 4:  // Inside wall
                tilePrefab = insideWallPrefab;
                rotation = DetermineWallRotation(x, y);  // Adjust rotation for inside walls
                break;
            case 5:
                tilePrefab = pelletPrefab;
                break;
            case 6:
                tilePrefab = powerPelletPrefab;
                break;
            case 7:
                tilePrefab = tJunctionPrefab;
                rotation = DetermineTJunctionRotation(x, y);  // Adjust rotation for T-junctions
                break;
        }

        if (tilePrefab != null)
        {
            // Instantiate the tile with the determined rotation
            GameObject tile = Instantiate(tilePrefab, position, rotation);
            tile.tag = "Level";
        }
    }
    Quaternion DetermineCornerRotation(int x, int y)
    {
        // Assume default rotation
        Quaternion rotation = Quaternion.identity;

        // Check the surroundings
        bool hasWallAbove = (y > 0 && (levelMap[y - 1, x] == 2 || levelMap[y - 1, x] == 4));
        bool hasWallLeft = (x > 0 && (levelMap[y, x - 1] == 2 || levelMap[y, x - 1] == 4));
        bool hasWallBelow = (y < levelMap.GetLength(0) - 1 && (levelMap[y + 1, x] == 2 || levelMap[y + 1, x] == 4));
        bool hasWallRight = (x < levelMap.GetLength(1) - 1 && (levelMap[y, x + 1] == 2 || levelMap[y, x + 1] == 4));

        // Rotate the corner based on surrounding walls
        if (hasWallAbove && hasWallRight)      // Top-left corner (no rotation)
            rotation = Quaternion.Euler(0, 0, 0);
        else if (hasWallAbove && hasWallLeft)  // Top-right corner (90 degrees)
            rotation = Quaternion.Euler(0, 0, 90);
        else if (hasWallBelow && hasWallLeft)  // Bottom-right corner (180 degrees)
            rotation = Quaternion.Euler(0, 0, 180);
        else if (hasWallBelow && hasWallRight) // Bottom-left corner (270 degrees)
            rotation = Quaternion.Euler(0, 0, 270);

        return rotation;
    }
    Quaternion DetermineWallRotation(int x, int y)
    {
        // Assume default rotation (horizontal)
        Quaternion rotation = Quaternion.Euler(0, 0, 0);

        // Check if the wall should be vertical (based on neighboring walls)
        bool hasWallAbove = (y > 0 && (levelMap[y - 1, x] == 2 || levelMap[y - 1, x] == 4));
        bool hasWallBelow = (y < levelMap.GetLength(0) - 1 && (levelMap[y + 1, x] == 2 || levelMap[y + 1, x] == 4));

        if (hasWallAbove || hasWallBelow)
        {
            // If there's a wall above or below, rotate it to vertical
            rotation = Quaternion.Euler(0, 0, 90);
        }

        return rotation;
    }
    Quaternion DetermineInsideCornerRotation(int x, int y)
    {
        // Assume default rotation
        Quaternion rotation = Quaternion.identity;

        bool hasWallAbove = (y > 0 && (levelMap[y - 1, x] == 3 || levelMap[y - 1, x] == 4));
        bool hasWallLeft = (x > 0 && (levelMap[y, x - 1] == 3 || levelMap[y, x - 1] == 4));
        bool hasWallBelow = (y < levelMap.GetLength(0) - 1 && (levelMap[y + 1, x] == 3 || levelMap[y + 1, x] == 4));
        bool hasWallRight = (x < levelMap.GetLength(1) - 1 && (levelMap[y, x + 1] == 3 || levelMap[y, x + 1] == 4));

        // Rotate the inside corner based on surrounding walls
        if (hasWallAbove && hasWallRight)      // Top-left corner (no rotation)
            rotation = Quaternion.Euler(0, 0, 0);
        else if (hasWallAbove && hasWallLeft)  // Top-right corner (90 degrees)
            rotation = Quaternion.Euler(0, 0, 90);
        else if (hasWallBelow && hasWallLeft)  // Bottom-right corner (180 degrees)
            rotation = Quaternion.Euler(0, 0, 180);
        else if (hasWallBelow && hasWallRight) // Bottom-left corner (270 degrees)
            rotation = Quaternion.Euler(0, 0, 270);

        return rotation;
    }
    Quaternion DetermineTJunctionRotation(int x, int y)
    {
        // Assume default rotation (no rotation)
        Quaternion rotation = Quaternion.identity;

        bool hasWallAbove = (y > 0 && (levelMap[y - 1, x] == 2 || levelMap[y - 1, x] == 4));
        bool hasWallLeft = (x > 0 && (levelMap[y, x - 1] == 2 || levelMap[y, x - 1] == 4));
        bool hasWallBelow = (y < levelMap.GetLength(0) - 1 && (levelMap[y + 1, x] == 2 || levelMap[y + 1, x] == 4));
        bool hasWallRight = (x < levelMap.GetLength(1) - 1 && (levelMap[y, x + 1] == 2 || levelMap[y, x + 1] == 4));

        // Rotate the T-junction based on surrounding walls
        if (hasWallAbove && hasWallRight && hasWallLeft)  // T facing down
            rotation = Quaternion.Euler(0, 0, 180);
        else if (hasWallBelow && hasWallRight && hasWallLeft)  // T facing up
            rotation = Quaternion.Euler(0, 0, 0);
        else if (hasWallLeft && hasWallAbove && hasWallBelow)  // T facing right
            rotation = Quaternion.Euler(0, 0, 90);
        else if (hasWallRight && hasWallAbove && hasWallBelow)  // T facing left
            rotation = Quaternion.Euler(0, 0, 270);

        return rotation;
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
                PlaceTile(levelMap[y, x], position, x, y);

            }
        }
    }
    void AdjustCamera()
    {
        Camera.main.orthographicSize = levelMap.GetLength(0) * tileSize / 2;
        Camera.main.transform.position = new Vector3(levelMap.GetLength(1) * tileSize / 2, -levelMap.GetLength(0) * tileSize / 2, -10);
    }

}

