using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle;

public class MapEnemies : MonoBehaviour
{
    public GameObject[] prefabs;
    public BattleObj battle;

    public int minX, minY, maxX, maxY;

    public int nums;

    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("MapEnemies start...");
        // List<Vector2> lst = new List<Vector2>();

        for (int i = 0; i < nums; ++i)
        {
            int tx = Random.Range(minX, maxX);
            int ty = Random.Range(minY, maxY);

            while (true)
            {
                // if (lst.IndexOf(new Vector2(tx, ty)) < 0)
                // {
                //     break;
                // }
                if (battle.battle.IsValidPos(new Vector2(tx, ty), 1))
                {
                    break;
                }

                tx = Random.Range(minX, maxX);
                ty = Random.Range(minY, maxY);
            }

            int r = Random.Range(0, prefabs.Length);
            // lst.Add(new Vector2(tx, ty));

            GameObject gobj = Instantiate(prefabs[r],
            new Vector3(tx, ty, 0),
            Quaternion.identity);

            // Battle.MapObj obj = battle.battle.New(new Vector2(tx, ty), 1, true, null);
            Battle.Unit unit = battle.battle.NewUnit(20000, new Vector2(tx, ty), gobj, FactionType.Enemey);
            unit.AddAI(Battle.AIType.AI1);
            // unit.AddAI(new Battle.AI1(unit));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
