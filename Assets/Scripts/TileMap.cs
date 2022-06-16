using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMap : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile tile001;
    public Tile tile002;
    // Start is called before the first frame update
    void Start()
    {
        tilemap = GameObject.Find("Tilemap").GetComponent<Tilemap>();

        tile001 = Resources.Load<Tile>("Map/Tile/LevelScene_16x_1");
        tile002 = Resources.Load<Tile>("Map/Tile/LevelScene_16x_2");

        for (int x = -500; x < 500; x++)
        {
            for (int y = -500; y < 500; y++)
            {
                Vector3Int p = new Vector3Int(x, y, 0);
                int t = Random.Range(0, 2);
                if (t == 0)
                {
                    tilemap.SetTile(p, tile001);
                }
                else
                {
                    tilemap.SetTile(p, tile002);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
