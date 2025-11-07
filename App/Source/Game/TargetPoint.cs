using SFML.Graphics;
using SFML.System;
using System;

namespace GameJam
{
    public class TargetPoint : StaticActor
    {
        public enum TargetType
        {
            Start,
            Intermediate,
            Finish
        }

        public TargetType Type;

        public TargetPoint()
        {
            Layer = ELayer.Items;
        }
        public override void Update(float dt)
        {
            if (MyGame.Get.CurrentState != MyGame.GameState.Game) return;
            base.Update(dt);
        }

        public void Init(TargetType type)
        {
            switch (type)
            {
                case TargetType.Start:
                    Sprite = new Sprite(new Texture(Assets.Open("Lightbound.Data.Textures.TargetPoint.Scroll.png")));
                    Center();

                    Position = new Vector2f(
                        1f + (GetLocalBounds().Width / 2f),
                        Engine.Get.random.NextFloat(
                            1f + (GetLocalBounds().Height / 2f),
                            Engine.Get.Window.Size.Y - 1f - (GetLocalBounds().Height / 2f)
                        )
                    );
                    break;
                case TargetType.Intermediate:

                    Sprite = new Sprite(new Texture(Assets.Open("Lightbound.Data.Textures.TargetPoint.MoneyBag.png")));
                    Center();

                    Position = new Vector2f(
                        Engine.Get.random.NextFloat(
                            GetLocalBounds().Width,
                            Engine.Get.Window.Size.X - GetLocalBounds().Width
                        ),
                        Engine.Get.random.NextFloat(
                            1f + (GetLocalBounds().Height / 2f),
                            Engine.Get.Window.Size.Y - 1f - (GetLocalBounds().Height / 2f)
                        )
                    );
                    break;
                case TargetType.Finish:
                    Sprite = new Sprite(new Texture(Assets.Open("Lightbound.Data.Textures.TargetPoint.Chest.png")));
                    Center();

                    Position = new Vector2f(
                        Engine.Get.Window.Size.X - 1f - (GetLocalBounds().Width / 2f),
                        Engine.Get.random.NextFloat(
                            1f + (GetLocalBounds().Height / 2f),
                            Engine.Get.Window.Size.Y - 1f - (GetLocalBounds().Height / 2f)
                        )
                    );

                    OnDestroy += OnFinishDestroy;

                    break;
            }
            Type = type;
        }

        public void OnFinishDestroy(Actor actor)
        {
            RemoveOnFinishDestroy();
            MyGame.Get.ChangeState(MyGame.GameState.Victory);
        }

        public void RemoveOnFinishDestroy()
        {
            OnDestroy -= OnFinishDestroy;
        }
    }
}
