using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static SaveTheKingdomDotDotDotPlease.Globals;
using static SaveTheKingdomDotDotDotPlease.Enums;

namespace SaveTheKingdomDotDotDotPlease
{
    internal class Tile
    {
        private TileType type; 
        public Boolean solid;
        public Rectangle sourceRect; 
        public Tile(TileType type, Boolean solid, Rectangle sourceRect)
        {
            this.type = type;
            this.solid = solid;
            this.sourceRect = sourceRect;
        }
        //private Rectangle GetSourceRectangleForTile(TileType tileType)
        //{
        //    switch (tileType)
        //    {
        //        case TileType.Grass:
        //            return new Rectangle(0, 0, WorldTileSize, WorldTileSize);
        //        case TileType.Stone:
        //            return new Rectangle(16, 0, WorldTileSize, WorldTileSize);
        //        case TileType.Water:
        //            return new Rectangle(16 * 2, 0, WorldTileSize, WorldTileSize);
        //        default:
        //            return new Rectangle(0, 8, WorldTileSize, WorldTileSize);
        //    }
        //}
    }
}
