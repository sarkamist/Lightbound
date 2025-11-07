using SFML.System;
using System;

namespace GameJam
{
    public class BoundaryActorSpawner<T> : Actor where T : Actor, new()
    {
        public float MinTime = 2.0f;
        public float MaxTime = 3.0f;
        private float coolDown;
        public void Reset()
        {
            Random r = new Random();
            float d = (float) r.NextDouble();
            coolDown = MaxTime * d + (1.0f - d) * MinTime;
        }
        public override void Update(float dt)
        {
            if (MyGame.Get.CurrentState != MyGame.GameState.Game) return;
            base.Update(dt);
            coolDown -= dt;
            if (coolDown < 0.0f)
            {
                SpawnActor();
                Reset();
            }
        }
        public void SpawnActor()
        {
            Random r = new Random();
            float d1 = (float) r.NextDouble();
            float d2 = (float) r.NextDouble();

            Actor a = Engine.Get.Scene.Create<T>();

            float X = r.Next(0, 2) == 0 ? -a.GetLocalBounds().Width : Engine.Get.Window.Size.X + a.GetLocalBounds().Width;
            float Y = r.Next(0, 2) == 0 ? -a.GetLocalBounds().Height : Engine.Get.Window.Size.Y + a.GetLocalBounds().Height;

            a.Position = new Vector2f(X, Y);
        }
    }
}

