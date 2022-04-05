﻿namespace RobotSvr
{
    public class IntroScene : Scene
    {
        public bool MBoOnClick = false;
        public long MDwStartTime = 0;

        public IntroScene() : base(SceneType.stIntro)
        {

        }

        public override void OpenScene()
        {
            MBoOnClick = false;
            MDwStartTime = MShare.GetTickCount() + 2 * 1000;
        }

        public override void CloseScene()
        {
        }

        public override void PlayScene()
        {
            if (MShare.GetTickCount() > MDwStartTime)
            {
                MBoOnClick = true;
                ClMain.DScreen.ChangeScene(SceneType.stLogin);
                if (!MShare.g_boDoFadeOut && !MShare.g_boDoFadeIn)
                {
                    MShare.g_boDoFadeIn = true;
                    MShare.g_nFadeIndex = 0;
                }
            }
        }
    }
}