using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;

namespace Resources
{
    public class DataLibrary
    {
        [XmlArrayItem(ElementName = "Snake", Type = typeof(SnakeProps))]
        [XmlArray(ElementName = "Snakes")]
        public SnakeProps[] snakeProps;
    }

    public class SnakeProps
    {
        public Color headColor { get; set; }
        public Color bodyColor { get; set; }
        public Color appleColor { get; set; }

        public int headSize { get; set; }
        public int bodySize { get; set; }
        public int appleSize { get; set; }

        public int startingLength { get; set; }
        public int increaseLength { get; set; }

        public int speed { get; set; }

        public bool borders { get; set; }
        public bool collideOthers { get; set; }

        public string controlMethod { get; set; }
    }
}
