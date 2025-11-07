using SFML.Graphics;
using SFML.System;
using System;

namespace GameJam
{
    public class Moth : StaticActor
    {
        public CircleShape CircleCollider;

        public float RadAngle;
        public float OrbitalSpeed;
        public float OrbitalDist;

        public const float TimeLoss = 0.5f;

        public int CurrentHits;
        public const int MaxHits = 3;
        public Moth()
        {
            Layer = ELayer.Enemies;
            Sprite = new Sprite(new Texture(Assets.Open("Lightbound.Data.Textures.Moth.Moth.png")));
            Center();

            CircleCollider = new CircleShape(30f);
            CircleCollider.Origin = new Vector2f(CircleCollider.GetLocalBounds().Width / 2f, CircleCollider.GetLocalBounds().Height / 2f);
            CircleCollider.FillColor = new Color(0, 255, 0, 100);

            RadAngle = 0f;
            Speed = 200f;
            OrbitalSpeed = 4f;
            OrbitalDist = 50f;

            CurrentHits = 0;
            OnDestroy += Explode;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            base.Draw(target, states);
            DebugUtil.DrawShape(CircleCollider);
        }

        public override void Update(float dt)
        {
            if (MyGame.Get.CurrentState != MyGame.GameState.Game) return;
            base.Update(dt);
            CircleCollider.Position = new Vector2f(Position.X, Position.Y);

            RadAngle += OrbitalSpeed * dt;
            Vector2f nearPosition = new Vector2f(
                MyGame.Get.Lightbeam.Position.X + (OrbitalDist * (float) Math.Cos(RadAngle)),
                MyGame.Get.Lightbeam.Position.Y + (OrbitalDist * (float) Math.Sin(RadAngle))
            );

            Forward = (nearPosition - Position).Normal();

            if ((MyGame.Get.Lightbeam.Position - Position).Size() <= MyGame.Get.Lightbeam.LightCircle.Radius)
            {
                MyGame.Get.Lightbeam.RemainingTime -= TimeLoss * dt;
            }
        }

        public void Hit() {
            CurrentHits++;
            if (CurrentHits >= MaxHits) {
                Destroy();
            }
        }

        private void Explode(Actor actor)
        {
            MyGame.Get.SoundManager.PlaySound("MothDeathVFX", 30.0f);
            Actor a = Engine.Get.Scene.Create<MothDeathEffect>();
            a.Position = new Vector2f(Position.X, Position.Y);
        }

        public void RemoveExplode()
        {
            OnDestroy -= Explode;
        }
    }
}
