using Candle;
using SFML.Graphics;
using SFML.System;

namespace GameJam
{
    public class Beacon : StaticActor
    {
        public RadialLight LightSource;
        public CircleShape LightCircle;

        public const float BorderRadius = 10f;

        public Beacon()
        {
            LightSource = new RadialLight();
            LightCircle = new CircleShape(200f);
            LightSource.Range = LightCircle.Radius + BorderRadius;

            MyGame.Get.Darkness.Lights.Add(LightSource);
        }

        public override void Update(float dt)
        {
            LightSource.Range = LightCircle.Radius + BorderRadius;
            LightCircle.Origin = new Vector2f(LightCircle.GetLocalBounds().Width / 2f, LightCircle.GetLocalBounds().Height / 2f);
            LightSource.Position = new Vector2f(Position.X, Position.Y);
            LightCircle.Position = new Vector2f(Position.X, Position.Y);
        }
    }
}
