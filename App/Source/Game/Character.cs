using SFML.Graphics;
using SFML.System;
using System;
using System.Linq;

namespace GameJam
{
    public class Character : StaticActor
    {
        public TargetPoint CurrentTarget;

        public const float AcquisitionRange = 5f;

        public int CurrentHealth;
        public float HealthLossTimer;
        public const float HealthLossRate = 1f;
        public Character()
        {
            Layer = ELayer.Character;
            Sprite = new Sprite(new Texture(Assets.Open("Lightbound.Data.Textures.Player.Character.png")));
            Center();

            Speed = 100f;
            Forward = new Vector2f(0f, 0f).Normal();

            DetermineNextTarget();
            Position = new Vector2f(MyGame.Get.StartPoint.Position.X, MyGame.Get.StartPoint.Position.Y);

            CurrentHealth = 15;
            HealthLossTimer = HealthLossRate;

            OnDestroy += OnCharacterDestroy;
        }

        public override void Update(float dt)
        {
            if (MyGame.Get.CurrentState != MyGame.GameState.Game) return;
            base.Update(dt);

            Forward = (CurrentTarget.Position - Position).Normal();

            if ((CurrentTarget.Position - Position).Size() <= AcquisitionRange)
            {
                MyGame.Get.IntermediatePoints.Remove(CurrentTarget);
                CurrentTarget.Destroy();
                DetermineNextTarget();
            }

            if (
                MyGame.Get.Lightbeam is not null
                && (MyGame.Get.Lightbeam.Position - Position).Size() > MyGame.Get.Lightbeam.LightCircle.Radius
                && MyGame.Get.StartingBeacon is not null
                && (MyGame.Get.StartingBeacon.Position - Position).Size() > MyGame.Get.StartingBeacon.LightCircle.Radius
                && MyGame.Get.FinishBeacon is not null
                && (MyGame.Get.FinishBeacon.Position - Position).Size() > MyGame.Get.FinishBeacon.LightCircle.Radius
            )
            {
                HealthLossTimer -= dt;
                if (HealthLossTimer <= 0) {
                    HealthLossTimer = HealthLossRate;
                    CurrentHealth -= 1;
                    if (CurrentHealth <= 0) {
                        Destroy();
                    }
                }
            }
        }

        private void OnCharacterDestroy(Actor actor)
        {
            RemoveOnCharacterDestroy();
            MyGame.Get.ChangeState(MyGame.GameState.Defeat);
        }

        public void RemoveOnCharacterDestroy()
        {
            OnDestroy -= OnCharacterDestroy;
        }

        public void DetermineNextTarget() {
            if (MyGame.Get.IntermediatePoints is not null && MyGame.Get.IntermediatePoints.Count > 0)
            {
                CurrentTarget = MyGame.Get.IntermediatePoints
                    .OrderBy(ip => (int) (ip.Position - Position).Size())
                    .ToList()
                    .First();
            }
            else
            {
                CurrentTarget = MyGame.Get.FinishPoint;
            }
        }
    }
}
