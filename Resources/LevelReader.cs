using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using TRead = Resources.LevelLibrary;

namespace Resources
{
    public class LevelReader : ContentTypeReader<TRead>
    {
        protected override TRead Read(ContentReader input, TRead existingInstance)
        {
            // TODO: Do not hard code these somehow!!
            int rows = LevelLibrary._rows;
            int columns = LevelLibrary._columns;

            int type = (int)input.ReadInt32();

            char[,] tileMap = new char[rows, columns];

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    tileMap[row, column] = input.ReadChar();
                }
            }

            return new LevelLibrary(tileMap, type);
        }
    }
}
