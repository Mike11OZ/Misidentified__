using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Misidentified
{
    public class FirstPersonCamera
    {
        public Vector3 Position;
        public float Yaw;
        public float Pitch;

        public float MouseSensitivity = 0.0025f;

        public Matrix View
        {
            get
            {
                // Build a rotation quaternion using yaw/pitch
                var rotation = Quaternion.CreateFromYawPitchRoll(Yaw, Pitch, 0);

                // Forward vector from rotation
                Vector3 forward = Vector3.Transform(Vector3.Forward, rotation);
                Vector3 up = Vector3.Transform(Vector3.Up, rotation);

                // Camera looks from Position to Position + forward
                return Matrix.CreateLookAt(Position, Position + forward, up);
            }
        }

        public Matrix Projection { get; private set; }

        public FirstPersonCamera(GraphicsDevice graphicsDevice)
        {
            Projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4,
                graphicsDevice.Viewport.AspectRatio,
                0.1f,
                1000f
            );
        }

        public void Update()
        {
            Vector2 mouseDelta = Input.MouseDelta();

            Yaw -= mouseDelta.X * MouseSensitivity;
            Pitch -= mouseDelta.Y * MouseSensitivity;

            // Clamp vertical look
            Pitch = MathHelper.Clamp(Pitch, -MathHelper.PiOver2 + 0.01f, MathHelper.PiOver2 - 0.01f);
        }

        public Vector3 Forward
        {
            get
            {
                return Vector3.Normalize(new Vector3(
                    (float)Math.Sin(Yaw),   // X axis
                    0,                       // Keep movement flat on Y
                    (float)Math.Cos(Yaw)    // Z axis
                ));
            }
        }

        public Vector3 Right =>
            Vector3.Normalize(new Vector3(
                (float)Math.Cos(Yaw),
                0,
                -(float)Math.Sin(Yaw)
            ));
    }
}
