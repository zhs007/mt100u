using System.Collections.Generic;
using System.Numerics;
using NReco.Csv;
using System.IO;
using UnityEngine;

namespace Battle
{
    public class UnitMgr
    {
        protected static Dictionary<int, UnitData> mapUnitData = new Dictionary<int, UnitData>();

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
            var inputLocation = Application.dataPath;
            inputLocation += (@"/Resources/CSV/" + docName);

            using (var streamRdr = new StreamReader(inputLocation))
            {
                var arrHeader = new List<string>();
                int row = 0;

                var csvReader = new CsvReader(streamRdr, ",");
                while (csvReader.Read())
                {
                    if (row == 0)
                    {
                        for (int i = 0; i < csvReader.FieldsCount; i++)
                        {
                            arrHeader.Add(csvReader[i]);
                        }
                    }
                    else
                    {
                        var ud = new UnitData();

                        for (int i = 0; i < csvReader.FieldsCount; i++)
                        {
                            if (arrHeader[i] == "typeid")
                            {
                                ud.typeid = int.Parse(csvReader[i]);
                            }
                            else if (arrHeader[i] == "hp")
                            {
                                ud.hp = int.Parse(csvReader[i]);
                            }
                            else if (arrHeader[i] == "dps")
                            {
                                ud.dps = int.Parse(csvReader[i]);
                            }
                            else if (arrHeader[i] == "speed")
                            {
                                ud.speed = int.Parse(csvReader[i]) / 100.0f;
                            }
                            else if (arrHeader[i] == "size")
                            {
                                ud.size = int.Parse(csvReader[i]) / 100.0f;
                            }
                            else if (arrHeader[i] == "thinkts")
                            {
                                ud.thinkts = int.Parse(csvReader[i]);
                            }
                        }

                        mapUnitData[ud.typeid] = ud;

                        // Debug.Log("load unitdata " + ud.typeid + " speed " + ud.speed);
                    }

                    row++;
                }
            }
        }
    };
}
