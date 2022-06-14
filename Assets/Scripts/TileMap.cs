using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMap : MonoBehaviour
{
    public Tilemap m_tilemap;
    public Tile m_tile001;
    public Tile m_tile002;
    // Start is called before the first frame update
    void Start()
    {
        m_tilemap = GameObject.Find("Tilemap").GetComponent<Tilemap>();

        m_tile001 = Resources.Load<Tile>("Map/Tile/LevelScene_16x_1");
        m_tile002 = Resources.Load<Tile>("Map/Tile/LevelScene_16x_2");

        for (int x = 0; x < 20; x++)
        {
            for (int y = 0; y < 20; y++)
            {
                Vector3Int p = new Vector3Int(x, y, 0);
                m_tilemap.SetTile(p, m_tile001);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
