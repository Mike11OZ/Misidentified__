using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Misidentified
{
    public class CubePrimitive
    {
        VertexPositionTexture[] vertices;
        short[] indices;

        public CubePrimitive(float size = 1f)
        {
            float s = size / 2f;

            vertices = new VertexPositionTexture[]
            {
            // Front
            new(new Vector3(-s, -s, s), new Vector2(0,1)),
            new(new Vector3(-s, s, s), new Vector2(0,0)),
            new(new Vector3(s, s, s), new Vector2(1,0)),
            new(new Vector3(s, -s, s), new Vector2(1,1)),

            // Back
            new(new Vector3(s, -s, -s), new Vector2(0,1)),
            new(new Vector3(s, s, -s), new Vector2(0,0)),
            new(new Vector3(-s, s, -s), new Vector2(1,0)),
            new(new Vector3(-s, -s, -s), new Vector2(1,1)),
            };

            indices = new short[]
            {
            0,1,2, 0,2,3,
            4,5,6, 4,6,7
            };
        }

        public void Draw(GraphicsDevice device, BasicEffect effect, Matrix world)
        {
            effect.World = world;

            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                device.DrawUserIndexedPrimitives(
                    PrimitiveType.TriangleList,
                    vertices,
                    0,
                    vertices.Length,
                    indices,
                    0,
                    indices.Length / 3
                );
            }
        }
    }
}
