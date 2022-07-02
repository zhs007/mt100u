using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using NReco.Csv;
using UnityEngine;

namespace Battle
{
    public class Utils
    {
        public static void LoadCSV<T>(
            string fn,
            Func<CsvReader, List<string>, T> funcParseObj
        )
        {
            var inputLocation =
                Application.dataPath + (@"/Resources/CSV/" + fn);

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
                        funcParseObj(csvReader, arrHeader);
                    }

                    row++;
                }
            }
        }
    }
}
