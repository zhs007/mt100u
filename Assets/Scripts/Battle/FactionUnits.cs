using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using NReco.Csv;
using UnityEngine;

namespace Battle
{
    public class FactionUnits
    {
        protected Dictionary<int, Units> mapFactionUnits;

        public FactionUnits()
        {
            mapFactionUnits = new Dictionary<int, Units>();
            mapFactionUnits[FactionType.Player] = new Units();
            mapFactionUnits[FactionType.Enemey] = new Units();
        }

        public void AddUnit(Unit unit)
        {
            mapFactionUnits[unit.Faction].AddUnit(unit);
        }
    }
}
