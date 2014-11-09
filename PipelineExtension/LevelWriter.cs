using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

using TWrite = Resources.LevelLibrary;

namespace PipelineExtension
{
    [ContentTypeWriter]
    public class LevelWriter : ContentTypeWriter<TWrite>
    {
        protected override void Write(ContentWriter output, TWrite value)
        {
            output.Write(Convert.ToInt32(value.type));

            for (int row = 0; row < value.rows; row++)
            {
                for (int column = 0; column < value.columns; column++)
                {
                    output.Write(value.tileMap[row, column]);
                }
            }
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return "Resources.LevelReader, Resources";
        }
    }
}
