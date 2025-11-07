using SFML.Audio;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameJam
{
    public class MyGame : Game
    {
        public enum GameState
        {
            None = 0,
            Intro = 1,
            Game = 2,
            Victory = 3,
            Defeat = 4
        }

        public GameState CurrentState = GameState.None;

        public SoundManager SoundManager { get; private set; }

        #region Intro Scene
        public IntroHUD IntroHUD { get; private set; }
        public Sound IntroAudio { get; private set; }
        #endregion

        #region Game Scene
        public Sound GameAudio { get; private set; }
        public GameBackground GameBackground { get; private set; }
        public InputHandler InputHandler { private set; get; }
        public Darkness Darkness { get; private set; }
        public Lightbeam Lightbeam { get; private set; }
        public List<PowerUp> PowerUps { get; private set; }
        public TargetPoint StartPoint { get; private set; }
        public Beacon StartingBeacon { get; private set; }
        public List<TargetPoint> IntermediatePoints { get; private set; }
        public TargetPoint FinishPoint { get; private set; }
        public Beacon FinishBeacon { get; private set; }
        public Character Character { get; private set; }
        public GameHUD GameHUD { private set; get; }
        #endregion

        #region Defeat Scene
        public Sound DefeatAudio { get; private set; }
        public DefeatHUD DefeatHUD { get; private set; }
        #endregion

        #region Victory Scene
        public Sound VictoryAudio { get; private set; }
        public VictoryHUD VictoryHUD { get; private set; }
        #endregion

        private static MyGame instance;

        public static MyGame Get
        {
            get
            {
                if (instance == null)
                {
                    instance = new MyGame();
                }

                return instance;
            }
        }
        private MyGame()
        {
        }
        public void Init()
        {
            SoundManager ??= new SoundManager();
            ChangeState(GameState.Intro);
        }

        private void CreateMothSpawner()
        {
            BoundaryActorSpawner<Moth> spawner;
            spawner = Engine.Get.Scene.Create<BoundaryActorSpawner<Moth>>();
            spawner.MinTime = 6.0f;
            spawner.MaxTime = 10.0f;
            spawner.Reset();
        }

        public void DeInit()
        {
        }
        public void Update(float dt)
        {
        }

        public void ChangeState(GameState newState) {
            if (newState != CurrentState)
            {
                switch (CurrentState)
                {
                    case GameState.None:
                        break;
                    case GameState.Intro:
                        SoundManager.RemoveSound(IntroAudio);
                        IntroAudio?.Dispose();
                        IntroAudio = null;

                        IntroHUD?.Destroy();
                        IntroHUD = null;

                        break;
                    case GameState.Game:
                        SoundManager.RemoveSound(GameAudio);
                        GameAudio?.Dispose();
                        GameAudio = null;

                        GameHUD?.Destroy();
                        GameHUD = null;

                        foreach (Moth m in Engine.Get.Scene.GetAll<Moth>()) m.RemoveExplode();
                        DestroyAll<BoundaryActorSpawner<Moth>>();
                        DestroyAll<Moth>();

                        Character.RemoveOnCharacterDestroy();
                        Character?.Destroy();
                        Character = null;

                        FinishBeacon?.Destroy();
                        FinishBeacon = null;
                        FinishPoint.RemoveOnFinishDestroy();
                        FinishPoint?.Destroy();
                        FinishPoint = null;
                        DestroyAll<TargetPoint>();
                        IntermediatePoints = null;
                        StartingBeacon?.Destroy();
                        StartingBeacon = null;
                        StartPoint?.Destroy();
                        StartPoint = null;

                        foreach (PowerUp p in Engine.Get.Scene.GetAll<PowerUp>()) p.RemoveOnPotionDestroy();
                        DestroyAll<PowerUp>();
                        PowerUps = null;

                        Lightbeam?.Destroy();
                        Lightbeam = null;
                        Darkness?.Destroy();
                        Darkness = null;
                        InputHandler?.Destroy();
                        InputHandler = null;
                        GameBackground?.Destroy();
                        GameBackground = null;

                        break;
                    case GameState.Defeat:
                        SoundManager.RemoveSound(DefeatAudio);
                        DefeatAudio?.Dispose();
                        DefeatAudio = null;

                        DefeatHUD?.Destroy();
                        DefeatHUD = null;

                        break;
                    case GameState.Victory:
                        SoundManager.RemoveSound(VictoryAudio);
                        VictoryAudio?.Dispose();
                        VictoryAudio = null;

                        VictoryHUD?.Destroy();
                        VictoryHUD = null;

                        break;
                }

                switch (newState)
                {
                    case GameState.None:
                        break;
                    case GameState.Intro:
                        IntroHUD ??= Engine.Get.Scene.Create<IntroHUD>();
                        IntroAudio = SoundManager.PlaySound("IntroAudio", 30.0f, true);
                        break;
                    case GameState.Game:
                        GameAudio = SoundManager.PlaySound("GameAudio", 30.0f, true);
                        GameBackground ??= Engine.Get.Scene.Create<GameBackground>();
                        InputHandler ??= Engine.Get.Scene.Create<InputHandler>();
                        Darkness ??= Engine.Get.Scene.Create<Darkness>();
                        Lightbeam ??= Engine.Get.Scene.Create<Lightbeam>();

                        PowerUps ??= new List<PowerUp>();
                        foreach (int i in Enumerable.Range(1, 5).ToArray())
                        {
                            PowerUps.Add(Engine.Get.Scene.Create<PowerUp>());
                        }

                        StartPoint ??= Engine.Get.Scene.Create<TargetPoint>();
                        StartPoint.Init(TargetPoint.TargetType.Start);

                        StartingBeacon ??= Engine.Get.Scene.Create<Beacon>();
                        StartingBeacon.Position = new Vector2f(StartPoint.Position.X, StartPoint.Position.Y);

                        IntermediatePoints ??= new List<TargetPoint>();
                        foreach (int i in Enumerable.Range(1, 11).ToArray())
                        {
                            TargetPoint intermediatePoint = Engine.Get.Scene.Create<TargetPoint>();
                            intermediatePoint.Init(TargetPoint.TargetType.Intermediate);
                            IntermediatePoints.Add(intermediatePoint);
                        }

                        FinishPoint ??= Engine.Get.Scene.Create<TargetPoint>();
                        FinishPoint.Init(TargetPoint.TargetType.Finish);

                        FinishBeacon ??= Engine.Get.Scene.Create<Beacon>();
                        FinishBeacon.Position = new Vector2f(FinishPoint.Position.X, FinishPoint.Position.Y);
                        FinishBeacon.LightCircle.Radius *= 0.25f;

                        Character ??= Engine.Get.Scene.Create<Character>();

                        CreateMothSpawner();

                        GameHUD = Engine.Get.Scene.Create<GameHUD>();
                        break;
                    case GameState.Defeat:
                        DefeatHUD ??= Engine.Get.Scene.Create<DefeatHUD>();
                        DefeatAudio = SoundManager.PlaySound("DefeatAudio", 30.0f);
                        break;
                    case GameState.Victory:
                        VictoryHUD ??= Engine.Get.Scene.Create<VictoryHUD>();
                        VictoryAudio = SoundManager.PlaySound("VictoryAudio", 30.0f);
                        break;
                }

                CurrentState = newState;
            }
        }

        private void DestroyAll<T>() where T : Actor
        {
            var actors = Engine.Get.Scene.GetAll<T>();
            actors.ForEach(x => x.Destroy());
        }
    }
}