using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridinstatiator : MonoBehaviour
{
    public GameObject tilePrefab;
    public int rows = 10;
    public int cols = 10;
    public float tileSize = 0.3f;  // Size of the tile in meters (30cm)

    // Define tiles as a list of GameObjects
    public List<scr> tiles = new List<scr>(); 



    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        int id = 1;
        for (int x = 0; x < cols; x++)
        {
            for (int z = 0; z < rows; z++)
            {
                GameObject tile = Instantiate(tilePrefab, new Vector3(x * tileSize, -0.49f, z * tileSize), Quaternion.identity);
                tile.transform.parent = this.transform;

                // Set i and j indices on tile
                scr tileScript = tile.GetComponent<scr>();
                if (tileScript != null)
                {
                    tileScript.SetIndices(x, z, id);
                    id++;
                }

                tiles.Add(tileScript);
            }
            Debug.Log(tiles);
            Debug.Log(tiles.Count);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
