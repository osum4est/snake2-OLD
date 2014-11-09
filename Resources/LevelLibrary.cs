using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Resources
{
    public class LevelLibrary
    {
        // Maybe don't hard code these???
        public static int _rows = 25;
        public static int _columns = 40;


        public char[,] tileMap { get; set; }
        public int rows { get { return tileMap.GetLength(0); } }
        public int columns { get { return tileMap.GetLength(1); } }
        public int type;

        public LevelLibrary (char[,] tileMap, int type)
        {
            this.tileMap = tileMap;
            this.type = type;
        }

        public List<Vector2> FindCharLocations(char c)
        {
            List<Vector2> locations = new List<Vector2>();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (tileMap[i, j] == c)
                    {
                        locations.Add(new Vector2(j, i));
                    }
                }
            }

            return locations;
        }

        public char GetChar (int row, int column)
        {
            return tileMap[row, column];
        }
    }
}
