using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveTheKingdomDotDotDotPlease
{
    internal class Globals
    {
        // TODO: pick a standard lol...
        public static int WindowWidth { get; set; } = 1280;
        public static int WindowHeight { get; set; } = 720;
        public static int SCALE { get; set; } = 4; // def = 5
        public static int TileSize { get; set; } = 8;
        public static int ScaledTileSize = SCALE * TileSize;
    }
}
