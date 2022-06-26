using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle;

public class BattleObj : MonoBehaviour
{
    public int minX, minY, maxX, maxY;
    public int areaW, areaH;
    public Battle.Battle battle;

    public BattleObj()
    {
        Battle.AIMgr.Init();

        battle = new Battle.Battle(minX, minY, maxX, maxY, areaW, areaH);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("BattleObj start...");

        Battle.UnitMgr.Init();

        // battle = new Battle.Battle(minX, minY, maxX, maxY, areaW, areaH);
        // battle = new Battle.Battle();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        battle.onIdle((int)(Time.deltaTime * 1000));
    }
}
