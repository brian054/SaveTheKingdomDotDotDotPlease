using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using static SaveTheKingdomDotDotDotPlease.Enums;

namespace SaveTheKingdomDotDotDotPlease
{
    internal class Overworld
    {
        int tileSize = Globals.WorldTileSize;
        int scaledTileSize = Globals.ScaledWorldTileSize;

        private Texture2D mapTexture;
        //private Texture2D SpriteSheet;

        Color[] mapData;

        // the entire map
        Tile[,] tileMap;

        // a List of solidTiles
        HashSet<TileType> solidTiles;

        //Vector2 playerPos;
        public Overworld(Texture2D mapTexture)
        {
            this.mapTexture = mapTexture;
            mapData = new Color[mapTexture.Width * mapTexture.Height];
            mapTexture.GetData(mapData);

            tileMap = new Tile[mapTexture.Width, mapTexture.Height];

            // only 2 solid tiles for now
            solidTiles = new();
            solidTiles.Add(TileType.Water);
            solidTiles.Add(TileType.Stone);

            // use tileMap and build your map.
            for (int x = 0; x < mapTexture.Width; x++)
            {
                for (int y = 0; y < mapTexture.Height; y++)
                {
                    Color pixelColor = mapData[y * 40 + x];

                    TileType tileType = GetTileTypeFromColor(pixelColor);

                    Rectangle sourceRectangle = GetSourceRectangleForTile(tileType);

                    Tile tile = new(tileType, solidTiles.Contains(tileType), sourceRectangle);
                    tileMap[x,y] = tile;
                }
            }

        }

        public void Update(GameTime gameTime) {
            //var keyState = Keyboard.GetState();
            //if (keyState.IsKeyDown(Keys.D))
            //{

            //} 
        }

        public void Draw(SpriteBatch sb) // might pass player in?
        {
            for (int x = 0; x < tileMap.GetLength(0); x++) 
            {
                for (int y = 0; y < tileMap.GetLength(1); y++)
                {
                    Rectangle destinationRectangle = new Rectangle(x * scaledTileSize, y * scaledTileSize, scaledTileSize, scaledTileSize);
                    sb.Draw(Globals.SpriteSheet, destinationRectangle, tileMap[x,y].sourceRect, Color.White);
                }
            }
        }

        public Boolean IsSolid(int tileX, int tileY)
        {
            if (tileMap[tileX, tileY].solid) { return true; } return false;
        }

        // this might go in tile.cs, not sure, maybe not
        private Rectangle GetSourceRectangleForTile(TileType tileType)
        {
            switch (tileType)
            {
                case TileType.Grass:
                    return new Rectangle(0, 0, tileSize, tileSize);
                case TileType.Stone:
                    return new Rectangle(16, 0, tileSize, tileSize);
                case TileType.Water:
                    return new Rectangle(16 * 2, 0, tileSize, tileSize);
                default:
                    return new Rectangle(0, 8, tileSize, tileSize);
            }
        }

        // this would probably end up getting it's own file.
        public TileType GetTileTypeFromColor(Color color)
        {
            // Define RGB values for specific tile color
            if (color.R == 22 && color.G == 217 && color.B == 33)
                return TileType.Grass;
            else if (color.R == 5 && color.G == 107 && color.B == 194)
                return TileType.Water;
            else if (color.R == 77 && color.G == 91 && color.B == 103)
                return TileType.Stone;
            else
                return TileType.Unknown;  // For any other colors
        }

        
    }
}
