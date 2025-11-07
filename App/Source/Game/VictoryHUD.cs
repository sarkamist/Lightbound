using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace GameJam
{
    public class VictoryHUD : StaticActor
    {
        private readonly string TitleStr = "YOU WIN";
        private readonly string PlayAgainStr = "R - PLAY AGAIN";
        private readonly string QuitStr = "Q - EXIT";

        private Texture BackgroundTexture;
        private Sprite BackgroundSprite;

        private Font FontOsifont, FontScratchyLemon;
        private Text TXT_Title, TXT_PlayAgain, TXT_Quit;

        private Sprite IMG_CharacterImage;

        private float InputDelay;

        public VictoryHUD()
        {
            RenderWindow window = Engine.Get.Window;
            Vector2u size = window.Size;

            Layer = ELayer.HUD;
            FontScratchyLemon = new Font(Assets.Open("Lightbound.Data.Fonts.ScratchyLemon.ttf"));
            FontOsifont = new Font(Assets.Open("Lightbound.Data.Fonts.osifont-lgpl3fe.ttf"));

            BackgroundTexture = new Texture(Assets.Open("Lightbound.Data.Textures.Backgrounds.MenuBackground.jpg"));
            BackgroundSprite = new Sprite(BackgroundTexture);
            BackgroundSprite.Scale = new Vector2f(
                (float) size.X / BackgroundTexture.Size.X,
                (float) size.Y / BackgroundTexture.Size.Y
            );
            BackgroundSprite.Position = new Vector2f(0, 0);

            TXT_Title = new Text(TitleStr, FontScratchyLemon, 250);
            TXT_Title.Position = new Vector2f(
                (size.X - TXT_Title.GetLocalBounds().Width) / 2f,
                130
            );
            TXT_Title.FillColor = Color.Black;

            TXT_PlayAgain = new Text(PlayAgainStr, FontOsifont, 40);
            TXT_PlayAgain.Position = new Vector2f(
                (size.X - TXT_PlayAgain.GetLocalBounds().Width) / 2f,
                (size.Y - TXT_PlayAgain.GetLocalBounds().Height) / 2f + 10
            );
            TXT_PlayAgain.FillColor = Color.Blue;

            TXT_Quit = new Text(QuitStr, FontOsifont, 38);
            TXT_Quit.Position = new Vector2f(
                (size.X - TXT_Quit.GetLocalBounds().Width) / 2f,
                (size.Y - TXT_Quit.GetLocalBounds().Height) / 2f + TXT_PlayAgain.GetLocalBounds().Height + 30);
            TXT_Quit.FillColor = Color.White;

            IMG_CharacterImage = new Sprite(new Texture(Assets.Open("Lightbound.Data.Textures.Player.Character.png")));
            IMG_CharacterImage.Position = new Vector2f(
                (size.X - IMG_CharacterImage.GetLocalBounds().Width) / 2f,
                (size.Y - IMG_CharacterImage.GetLocalBounds().Height) / 2f + IMG_CharacterImage.GetLocalBounds().Height + 80
            );

            InputDelay = 1f;

            Engine.Get.Window.KeyPressed += OnKeyPressed;
            OnDestroy += OnVictoryHUDDestroy;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(BackgroundSprite, states);
            target.Draw(TXT_Title, states);
            target.Draw(TXT_PlayAgain, states);
            target.Draw(TXT_Quit, states);
            target.Draw(IMG_CharacterImage, states);
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            if (InputDelay > 0f) InputDelay -= dt;
            else InputDelay = 0f;
        }

        public void OnKeyPressed(object sender, KeyEventArgs args)
        {
            if (InputDelay > 0f) return;

            if (args.Code == Keyboard.Key.R)
            {
                MyGame.Get.ChangeState(MyGame.GameState.Game);
            }
            else if (args.Code == Keyboard.Key.Q)
            {
                Environment.Exit(0);
            }
        }

        public void OnVictoryHUDDestroy(Actor actor)
        {
            OnDestroy -= OnVictoryHUDDestroy;
            Engine.Get.Window.KeyPressed -= OnKeyPressed;
        }
    }
}
