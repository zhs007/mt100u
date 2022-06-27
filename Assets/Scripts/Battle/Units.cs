using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using NReco.Csv;
using UnityEngine;

namespace Battle
{
    public class Units
    {
        protected Dictionary<int, Unit> mapUnits;

        public Units()
        {
            mapUnits = new Dictionary<int, Unit>();
        }

        public void AddUnit(Unit unit)
        {
            mapUnits[unit.EntityID] = unit;
        }
    }
}
