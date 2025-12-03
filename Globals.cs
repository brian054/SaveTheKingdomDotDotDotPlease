using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveTheKingdomDotDotDotPlease
{
    internal class Globals
    {
        public static int WindowWidth { get; set; } = 1280;
        public static int WindowHeight { get; set; } = 720;
        public static int SCALE { get; set; } = 4; // def = 5, no maybe 4
        public static int WorldTileSize { get; set; } = 8; 
        public static int ScaledWorldTileSize = SCALE * WorldTileSize;

        public static int EntityTileSize { get; set; } = 16;
        public static int ScaledEntityTileSize = SCALE * EntityTileSize;

        public static Texture2D SpriteSheet;
    }
}
