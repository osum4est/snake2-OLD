using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;

using TInput = System.String;
using TOutput = Resources.LevelLibrary;

namespace PipelineExtension
{
    [ContentProcessor(DisplayName = "Level Processor")]
    public class LevelProcessor : ContentProcessor<TInput, TOutput>
    {
        const int offset = 1;

        public override TOutput Process(TInput input, ContentProcessorContext context)
        {
            
            string[] lines = input.Split('\n');

            int type = Convert.ToInt32(lines[0]);

            int rows = Resources.LevelLibrary._rows;
            int columns = Resources.LevelLibrary._columns;

            char[,] tileMap = new char[rows, columns];
            for (int row = 0; row < rows; row++)
            {
                char[] values = lines[row + offset].ToCharArray();
                for(int column = 0; column < columns; column++)
                {
                    tileMap[row, column] = values[column];
                }
            }

            return new Resources.LevelLibrary(tileMap, type);
        }
    }
}