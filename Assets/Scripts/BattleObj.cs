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
        battle = new Battle.Battle(minX, minY, maxX, maxY, areaW, areaH);
    }

    // Start is called before the first frame update
    void Start()
    {
        // battle = new Battle.Battle();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {

    }
}
