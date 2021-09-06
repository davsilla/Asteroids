﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Asteroids
{
    class Ship : Primitive2D
    {
        private const float MAX_SPEED = 0.3f;
        private const float ACCELERATION = 0.1f;
        private const float TURN_SPEED = 2.5f;

        public float Speed { get; set; } = 0f;
        public Vector3 Direction { get; set; }

        public Ship(float x, float y)  : base(x, y)
        {
            CreateShip();
            Scale = 0.5f;
        }

        private void CreateShip()
        {
            vertexCount = 3;

            VertexPositionColor[] vertices = new VertexPositionColor[3];
            vertices[0] = new VertexPositionColor(new Vector3(0, 1f, 0), Globals.SPACE_WHITE);
            vertices[1] = new VertexPositionColor(new Vector3(0.5f, -0.5f, 0), Globals.SPACE_WHITE);
            vertices[2] = new VertexPositionColor(new Vector3(-0.5f, -0.5f, 0), Globals.SPACE_WHITE);

            vertexBuffer = new VertexBuffer(Globals.graphicsDevice.GraphicsDevice, typeof(VertexPositionColor), vertices.Length, BufferUsage.WriteOnly);
            
            short[] indices = new short[3];
            indices[0] = 0; indices[1] = 1; indices[2] = 2;

            vertexBuffer.SetData(vertices);
            indexBuffer = new IndexBuffer(Globals.graphicsDevice.GraphicsDevice, typeof(short), indices.Length, BufferUsage.WriteOnly);
            indexBuffer.SetData(indices);

            Initialized = true;
        }

        public override void Update()
        {
            base.Update();

            float fElapsedTime = (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;

            if (Globals.keyboard.IsKeyHeld(Keys.W))
            {
                if (Speed < MAX_SPEED) Speed += ACCELERATION * fElapsedTime;
                Direction = new Vector3(-MathF.Sin(Angle), MathF.Cos(Angle), 0);
                Position += Direction * Speed;
            }
            else if (Speed > 0)
            {
                Speed -= 0.5f * ACCELERATION * fElapsedTime;
                Direction = new Vector3(-MathF.Sin(Angle), MathF.Cos(Angle), 0);
                Position += Direction * Speed;
            }

            if (Globals.keyboard.IsKeyHeld(Keys.A))
            {
                Angle += TURN_SPEED * fElapsedTime;
            }

            if (Globals.keyboard.IsKeyHeld(Keys.D))
            {
                Angle -= TURN_SPEED * fElapsedTime;
            }

            if (Angle > 2f * MathF.PI) Angle -= 2f * MathF.PI;
            else if (Angle < -2f * MathF.PI) Angle += 2f * MathF.PI;
        }
    }
}
