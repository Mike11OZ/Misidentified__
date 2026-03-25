using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Misidentified
{
    public class Player
    {
        public Vector3 Position;
        public float VelocityY;
        public bool IsGrounded;

        public float Speed = 5f;
        public float JumpStrength = 6f;
        public float Gravity = 15f;

        public void Update(GameTime gameTime, FirstPersonCamera camera)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector3 move = Vector3.Zero;

            if (Input.IsKeyDown(Keys.W)) move += camera.Forward;
            if (Input.IsKeyDown(Keys.S)) move -= camera.Forward;
            if (Input.IsKeyDown(Keys.A)) move -= Vector3.Cross(camera.Forward, Vector3.Up);
            if (Input.IsKeyDown(Keys.D)) move += Vector3.Cross(camera.Forward, Vector3.Up);

            move.Normalize();
            Position += move * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Gravity
            if (!IsGrounded)
                VelocityY -= Gravity * dt;

            // Jump
            if (IsGrounded && Input.WasKeyPressed(Keys.Space))
            {
                VelocityY = JumpStrength;
                IsGrounded = false;
            }

            Position.Y += VelocityY * dt;

            // Simple ground collision
            if (Position.Y <= 1.8f)
            {
                Position.Y = 1.8f;
                VelocityY = 0;
                IsGrounded = true;
            }

            camera.Position = Position;
        }
    }
}
