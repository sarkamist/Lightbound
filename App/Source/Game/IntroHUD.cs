using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace GameJam
{
    public class IntroHUD : StaticActor
    {
        private readonly string TitleStr = "LIGHTBOUND";
        private readonly string MissionStr = "Help the little demon make it to the final treasure alive.";
        private readonly string BulletPoint1Str = "You must shine your lightbeam on it so that it will not be harmed by the darkness.";
        private readonly string BulletPoint2Str = "You must be careful as your lightbeam will fade over time.";
        private readonly string BulletPoint3Str = "Scout the map for potions that will allow you to replenish your lightbeam.";
        private readonly string BulletPoint4Str = "Moths will be attracted to your lightbeam, kill them to prevent them from absorbing your light.";
        private readonly string HowToPlayStr = "- The lightbeam will follow your cursor through the map.\n- To absorb a potion, hold your left mouse button over it.\n- Moths can be killed by clicking on them thrice.";
        private readonly string SubtitleStr = "BEGIN";

        private Texture BackgroundTexture;
        private Sprite BackgroundSprite;

        private RectangleShape RCT_Overlay;

        private Font FontOsifont, FontScratchyLemon;
        private Text TXT_Title, TXT_Mission, TXT_BulletPoint1, TXT_BulletPoint2, TXT_BulletPoint3, TXT_BulletPoint4, TXT_HowToPlay, TXT_Subtitle;

        private Sprite IMG_CharacterPortrait;

        public IntroHUD()
        {
            RenderWindow window = Engine.Get.Window;
            Vector2u size = window.Size;

            Layer = ELayer.HUD;
            FontScratchyLemon = new Font(Assets.Open("Lightbound.Data.Fonts.ScratchyLemon.ttf"));
            FontOsifont = new Font(Assets.Open("Lightbound.Data.Fonts.osifont-lgpl3fe.ttf"));

            BackgroundTexture = new Texture(Assets.Open("Lightbound.Data.Textures.Backgrounds.MenuBackground.jpg"));
            BackgroundSprite = new Sprite(BackgroundTexture);
            BackgroundSprite.Scale = new Vector2f(
                (float)size.X / BackgroundTexture.Size.X,
                (float)size.Y / BackgroundTexture.Size.Y
            );
            BackgroundSprite.Position = new Vector2f(0, 0);

            TXT_Title = new Text(TitleStr, FontScratchyLemon, 160);
            TXT_Title.Position = new Vector2f(
                (size.X - TXT_Title.GetLocalBounds().Width) / 2f,
                35
            );
            TXT_Title.FillColor = Color.Black;

            TXT_Mission = new Text(MissionStr, FontOsifont, 40);
            TXT_Mission.Position = new Vector2f(
                (size.X  - TXT_Mission.GetLocalBounds().Width) / 2f,
                250
            );
            TXT_Mission.FillColor = Color.Blue;

            TXT_BulletPoint1 = new Text(BulletPoint1Str, FontOsifont, 33);
            TXT_BulletPoint1.Position = new Vector2f(
                (size.X - TXT_BulletPoint1.GetLocalBounds().Width) / 2f,
                315
            );
            TXT_BulletPoint1.FillColor = Color.Black;

            TXT_BulletPoint2 = new Text(BulletPoint2Str, FontOsifont, 33);
            TXT_BulletPoint2.Position = new Vector2f(
                (size.X - TXT_BulletPoint2.GetLocalBounds().Width) / 2f,
                365
            );
            TXT_BulletPoint2.FillColor = Color.Black;

            TXT_BulletPoint3 = new Text(BulletPoint3Str, FontOsifont, 33);
            TXT_BulletPoint3.Position = new Vector2f(
               (size.X - TXT_BulletPoint3.GetLocalBounds().Width) / 2f,
                415
            );
            TXT_BulletPoint3.FillColor = Color.Black;

            TXT_BulletPoint4 = new Text(BulletPoint4Str, FontOsifont, 33);
            TXT_BulletPoint4.Position = new Vector2f(
                (size.X - TXT_BulletPoint4.GetLocalBounds().Width) / 2f,
                465
            );
            TXT_BulletPoint4.FillColor = Color.Black;

            TXT_Subtitle = new Text(SubtitleStr, FontScratchyLemon, 70);
            TXT_Subtitle.Position = new Vector2f(
                (size.X - TXT_Subtitle.GetLocalBounds().Width) / 2f,
                ((size.Y - TXT_Subtitle.GetLocalBounds().Height) / 2f) + 115
            );
            TXT_Subtitle.FillColor = Color.Blue;

            TXT_HowToPlay = new Text(HowToPlayStr, FontOsifont, 28);
            TXT_HowToPlay.Position = new Vector2f(
                (size.X - TXT_HowToPlay.GetLocalBounds().Width) / 2f,
                (size.Y - TXT_HowToPlay.GetLocalBounds().Height) - 50
            );
            TXT_HowToPlay.FillColor = Color.White;

            RCT_Overlay = new RectangleShape(
                new Vector2f(
                    (size.X / 2f) + 95,
                    (size.Y / 7f) + 35
                )
            );
            RCT_Overlay.Position = new Vector2f(
                ((size.X - TXT_HowToPlay.GetLocalBounds().Width) / 2f) - 50,
                size.Y - TXT_HowToPlay.GetLocalBounds().Height - 65
            );
            RCT_Overlay.FillColor = new Color(0, 0, 0, 180);

            IMG_CharacterPortrait = new Sprite(new Texture(Assets.Open("Lightbound.Data.Textures.Player.Character.png")));
            IMG_CharacterPortrait.Position = new Vector2f(
                (size.X - IMG_CharacterPortrait.GetLocalBounds().Width) / 2f,
                ((size.Y - IMG_CharacterPortrait.GetLocalBounds().Height) / 2f) + 205
            );

            Engine.Get.Window.KeyPressed += OnKeyPressed;
            Engine.Get.Window.MouseButtonPressed += OnMouseButtonPressed;
            OnDestroy += OnIntroHUDDestroy;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(BackgroundSprite, states);
            target.Draw(RCT_Overlay, states);
            target.Draw(TXT_Title, states);
            target.Draw(TXT_Mission, states);
            target.Draw(TXT_BulletPoint1, states);
            target.Draw(TXT_BulletPoint2, states);
            target.Draw(TXT_BulletPoint3, states);
            target.Draw(TXT_BulletPoint4, states);
            target.Draw(TXT_Subtitle, states);
            target.Draw(TXT_HowToPlay, states);
            target.Draw(IMG_CharacterPortrait, states);
        }

        public void OnKeyPressed(object sender, KeyEventArgs args)
        {
            if (args.Code == Keyboard.Key.Space)
            {
                MyGame.Get.ChangeState(MyGame.GameState.Game);
            }
        }

        public void OnMouseButtonPressed(object sender, MouseButtonEventArgs args)
        {
            if (args.Button == Mouse.Button.Left)
            {
                MyGame.Get.ChangeState(MyGame.GameState.Game);
            }
        }

        public void OnIntroHUDDestroy(Actor actor)
        {
            OnDestroy -= OnIntroHUDDestroy;
            Engine.Get.Window.KeyPressed -= OnKeyPressed;
            Engine.Get.Window.MouseButtonPressed -= OnMouseButtonPressed;
        }
    }
}
