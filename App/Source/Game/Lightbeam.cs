using Candle;
using SFML.Graphics;
using SFML.System;
using System;

namespace GameJam
{
    public class Lightbeam : StaticActor
    {
        public RadialLight LightSource;
        public CircleShape LightCircle;

        public float RemainingTime;
        public float TimeIncrease;
        public const float TimeIncreaseRate = 2f;

        public const float RadiusRatio = 7.5f;
        public const float BorderRadius = 10f;
        public float MinRadius = 30f;

        public Lightbeam()
        {
            Position = new Vector2f(Engine.Get.Window.Size.X / 2, Engine.Get.Window.Size.Y / 2);

            LightSource = new RadialLight();
            LightCircle = new CircleShape();
            LightCircle.FillColor = new Color(255, 255, 255, 0);
            
            Speed = 175f;

            RemainingTime = 25f;
            TimeIncrease = 0f;
            LightCircle.Radius = (RemainingTime * RadiusRatio);
            
            LightSource.Range = LightCircle.Radius + BorderRadius;

            MyGame.Get.Darkness.Lights.Add(LightSource);
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            base.Draw(target, states);
            LightCircle.Draw(target, states);
        }

        public override void Update(float dt)
        {
            if (MyGame.Get.CurrentState != MyGame.GameState.Game) return;
            base.Update(dt);

            if (TimeIncrease > 0f)
            {
                TimeIncrease -= (dt * TimeIncreaseRate);
                RemainingTime += (dt * TimeIncreaseRate);
                LightCircle.Radius = Math.Max(MinRadius, RemainingTime * RadiusRatio);
            }
            else if (RemainingTime > 0f)
            {
                RemainingTime -= dt;
                LightCircle.Radius = Math.Max(MinRadius, RemainingTime * RadiusRatio);
                if (LightCircle.Radius >= 30f && MinRadius < 30f) MinRadius = 30f;
            }
            else
            {
                RemainingTime = 0f;
                MinRadius = 1f;
                LightCircle.Radius = 1f;
            }

            LightCircle.Origin = new Vector2f(LightCircle.GetLocalBounds().Width / 2f, LightCircle.GetLocalBounds().Height / 2f);
            LightSource.Range = LightCircle.Radius + BorderRadius;

            LightSource.Position = new Vector2f(Position.X, Position.Y);
            LightCircle.Position = new Vector2f(Position.X, Position.Y);
            Forward = (Engine.Get.MousePos - Position).Normal();

            if (Position.X < 1f) Position = new Vector2f(1f, Position.Y);
            if (Position.X > Engine.Get.Window.Size.X - 1f) Position = new Vector2f(Engine.Get.Window.Size.X - 1f, Position.Y);
            if (Position.Y < 1f) Position = new Vector2f(Position.X, 1f);
            if (Position.Y > Engine.Get.Window.Size.Y - 1f) Position = new Vector2f(Position.X, Engine.Get.Window.Size.Y - 1f);
        }
    }
}
