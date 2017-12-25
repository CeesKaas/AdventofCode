using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace day14.implementation
{
    public class DiskGrid
    {
        public bool[][] Data { get; } = new bool[128][];
        public Guid[][] Regions { get; }

        public IEnumerable<string> StringData
        {
            get
            {
                foreach (var line in Data)
                {
                    yield return string.Join("", line.Select(_ => _ ? "#" : "."));
                }
            }
        }

        const string AvailableChars = "0123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz";

        public IEnumerable<string> StringRegions
        {
            get
            {
                var counter = 1;
                var toInt = Regions.SelectMany(_ => _).Distinct().Where(_ => _ != Guid.Empty).ToDictionary(_ => _, _ =>
                {
                    var label = $"{AvailableChars[counter / AvailableChars.Length]}{AvailableChars[counter % AvailableChars.Length]}";
                    counter++;
                    return label;
                });
                toInt[Guid.Empty] = "00";
                foreach (var line in Regions)
                {
                    yield return string.Join("", line.Select(_ => $"{toInt[_]}"));
                }
            }
        }


        public int ActiveBlocks
        {
            get
            {
                var count = 0;

                foreach (var line in Data)
                {
                    foreach (var block in line)
                    {
                        if (block)
                        {
                            count++;
                        }
                    }
                }

                return count;
            }
        }

        public int ActiveRegions
        {
            get
            {
                return Regions.SelectMany(_ => _).Distinct().Where(_ => _ != Guid.Empty).Count();
            }
        }

        private Guid[][] BuildRegions()
        {
            var regions = new Guid[128][];
            for (int i = 0; i < 128; i++)
            {
                regions[i] = new Guid[128];
            }
            for (int i = 0; i < 128; i++)
            {
                for (int j = 0; j < 128; j++)
                {
                    if (!Data[i][j])
                    {
                        regions[i][j] = Guid.Empty;
                        continue;
                    }

                    var regionLeft = Guid.Empty;
                    if (j > 0)
                    {
                        regionLeft = regions[i][j - 1];
                    }
                    var regionUp = Guid.Empty;
                    if (i > 0)
                    {
                        regionUp = regions[i - 1][j];
                    }

                    if (regionLeft != Guid.Empty && regionUp != Guid.Empty)
                    {
                        regions[i][j] = regionUp;
                        if (regionLeft != regionUp)
                        {
                            regions = regions.Select(line => line.Select(item => item == regionLeft ? regionUp : item).ToArray()).ToArray();
                        }
                    }
                    else if (regionLeft != Guid.Empty)
                    {
                        regions[i][j] = regionLeft;
                    }
                    else if (regionUp != Guid.Empty)
                    {
                        regions[i][j] = regionUp;
                    }
                    else
                    {
                        regions[i][j] = Guid.NewGuid();
                    }
                }
            }

            return regions;
        }

        public DiskGrid(string inputString)
        {
            var lines = Enumerable.Range(0, 128).Select(_ => Hasher.KnotHash($"{inputString}-{_}")).ToArray();

            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                Data[i] = new bool[128];
                for (int j = 0; j < line.Length; j++)
                {
                    var b = line[j];
                    Data[i][(j * 8) + 0] = ((b >> 7) & 1) == 1;
                    Data[i][(j * 8) + 1] = ((b >> 6) & 1) == 1;
                    Data[i][(j * 8) + 2] = ((b >> 5) & 1) == 1;
                    Data[i][(j * 8) + 3] = ((b >> 4) & 1) == 1;
                    Data[i][(j * 8) + 4] = ((b >> 3) & 1) == 1;
                    Data[i][(j * 8) + 5] = ((b >> 2) & 1) == 1;
                    Data[i][(j * 8) + 6] = ((b >> 1) & 1) == 1;
                    Data[i][(j * 8) + 7] = ((b >> 0) & 1) == 1;
                }
            }
            Regions = BuildRegions();
        }
    }
}
