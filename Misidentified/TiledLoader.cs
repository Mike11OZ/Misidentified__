using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Misidentified
{
    public class TiledLoader
    {
        public int Width;
        public int Height;
        public int TileWidth;
        public int TileHeight;

        public Dictionary<string, int[]> Layers = new();

        public void Load(string filePath)
        {
            var doc = XDocument.Load(filePath);

            var map = doc.Element("map");

            Width = int.Parse(map.Attribute("width").Value);
            Height = int.Parse(map.Attribute("height").Value);
            TileWidth = int.Parse(map.Attribute("tilewidth").Value);
            TileHeight = int.Parse(map.Attribute("tileheight").Value);

            foreach (var layer in map.Elements("layer"))
            {
                string name = layer.Attribute("name").Value;
                var data = layer.Element("data");

                List<int> tiles = new();

                if (data.Attribute("encoding")?.Value == "csv")                 // CSV parsing method
                {
                    string raw = data.Value;

                    var lines = raw.Split('\n');

                    foreach (var line in lines)
                    {
                        var values = line.Split(',');

                        foreach (var val in values)
                        {
                            if (!string.IsNullOrWhiteSpace(val))
                                tiles.Add(int.Parse(val.Trim()));
                        }
                    }
                }
                else
                {
                    foreach (var tile in data.Elements("tile"))                     // Fallback to XML format if not CSV
                    {
                        tiles.Add(int.Parse(tile.Attribute("gid").Value));
                    }
                }

                Layers[name] = tiles.ToArray();
            }
        }

        public void DrawLayer(string layerName, Texture2D tileset, SpriteBatch spriteBatch)
        {
            if (!Layers.ContainsKey(layerName))
                return;

            int[] tiles = Layers[layerName];

            int tilesPerRow = tileset.Width / TileWidth;

            for (int i = 0; i < tiles.Length; i++)
            {
                int gid = tiles[i];
                if (gid == 0) continue;

                int x = (i % Width) * TileWidth;
                int y = (i / Width) * TileHeight;

                int firstGid = 1;
                gid -= firstGid;

                Rectangle source = new Rectangle(0, 0, TileWidth, TileHeight);
                spriteBatch.Draw(tileset, new Vector2(x, y), source, Color.White);
            }
        }
    }
}
