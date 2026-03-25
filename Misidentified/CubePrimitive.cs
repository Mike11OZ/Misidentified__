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
            // +Z face
            new(new Vector3(-s,-s, s), new Vector2(0,1)),
            new(new Vector3(-s, s, s), new Vector2(0,0)),
            new(new Vector3( s, s, s), new Vector2(1,0)),
            new(new Vector3( s,-s, s), new Vector2(1,1)),

            // -Z face
            new(new Vector3( s,-s,-s), new Vector2(0,1)),
            new(new Vector3( s, s,-s), new Vector2(0,0)),
            new(new Vector3(-s, s,-s), new Vector2(1,0)),
            new(new Vector3(-s,-s,-s), new Vector2(1,1)),

            // +X face
            new(new Vector3( s,-s, s), new Vector2(0,1)),
            new(new Vector3( s, s, s), new Vector2(0,0)),
            new(new Vector3( s, s,-s), new Vector2(1,0)),
            new(new Vector3( s,-s,-s), new Vector2(1,1)),

            // -X face
            new(new Vector3(-s,-s,-s), new Vector2(0,1)),
            new(new Vector3(-s, s,-s), new Vector2(0,0)),
            new(new Vector3(-s, s, s), new Vector2(1,0)),
            new(new Vector3(-s,-s, s), new Vector2(1,1)),

            // +Y face
            new(new Vector3(-s, s, s), new Vector2(0,1)),
            new(new Vector3(-s, s,-s), new Vector2(0,0)),
            new(new Vector3( s, s,-s), new Vector2(1,0)),
            new(new Vector3( s, s, s), new Vector2(1,1)),

            // -Y face
            new(new Vector3(-s,-s,-s), new Vector2(0,1)),
            new(new Vector3(-s,-s, s), new Vector2(0,0)),
            new(new Vector3( s,-s, s), new Vector2(1,0)),
            new(new Vector3( s,-s,-s), new Vector2(1,1)),
            };

            indices = new short[]
            {
            0,1,2,  0,2,3,    // +Z
            4,5,6,  4,6,7,    // -Z
            8,9,10, 8,10,11,  // +X
            12,13,14,12,14,15,// -X
            16,17,18,16,18,19,// +Y
            20,21,22,20,22,23 // -Y
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
