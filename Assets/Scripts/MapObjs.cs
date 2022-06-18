using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObjs : MonoBehaviour
{
    public GameObject[] prefabs;
    public BattleObj battle;

    public int minX, minY, maxX, maxY;

    public int nums;

    // Start is called before the first frame update
    void Start()
    {
        List<Vector2> lst = new List<Vector2>();

        for (int i = 0; i < nums; ++i)
        {
            int tx = Random.Range(minX, maxX);
            int ty = Random.Range(minY, maxY);

            while (true)
            {
                if (lst.IndexOf(new Vector2(tx, ty)) < 0)
                {
                    break;
                }

                tx = Random.Range(minX, maxX);
                ty = Random.Range(minY, maxY);
            }

            int r = Random.Range(0, prefabs.Length);
            lst.Add(new Vector2(tx, ty));

            Instantiate(prefabs[r],
            new Vector3(tx, ty, 0),
            Quaternion.identity);

            battle.battle.NewMapObj(new Vector2(tx, ty), 1, true);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
