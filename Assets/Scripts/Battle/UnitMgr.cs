using System.Collections.Generic;
using System.IO;
using System.Numerics;
using NReco.Csv;
using UnityEngine;

namespace Battle
{
    public class UnitMgr
    {
        protected static Dictionary<int, UnitData>
            mapUnitData = new Dictionary<int, UnitData>();

        public static UnitData GetUnitData(int typeid)
        {
            if (mapUnitData.ContainsKey(typeid))
            {
                return mapUnitData[typeid];
            }

            return null;
        }

        public static void Init()
        {
            var docName = @"/chara.csv";

            Utils
                .LoadCSV<UnitData>(docName,
                (CsvReader reader, List<string> header) =>
                {
                    var ud = new UnitData();

                    for (int i = 0; i < reader.FieldsCount; i++)
                    {
                        if (header[i] == "typeid")
                        {
                            ud.typeid = int.Parse(reader[i]);
                        }
                        else if (header[i] == "hp")
                        {
                            ud.hp = int.Parse(reader[i]);
                        }
                        else if (header[i] == "dps")
                        {
                            ud.dps = int.Parse(reader[i]);
                        }
                        else if (header[i] == "speed")
                        {
                            ud.speed = int.Parse(reader[i]) / 100.0f;
                        }
                        else if (header[i] == "size")
                        {
                            ud.size = int.Parse(reader[i]) / 100.0f;
                        }
                        else if (header[i] == "thinkts")
                        {
                            ud.thinkts = int.Parse(reader[i]);
                        }
                        else if (header[i] == "visualrange")
                        {
                            ud.visualRange = int.Parse(reader[i]) / 100.0f;
                        }
                    }

                    mapUnitData[ud.typeid] = ud;

                    return ud;
                });
        }
    }
}
