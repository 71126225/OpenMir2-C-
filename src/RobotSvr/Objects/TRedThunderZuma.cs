﻿using SystemModule;

namespace RobotSvr
{
    public class TRedThunderZuma : TGasKuDeGi
    {
        public bool boCasted = false;

        public TRedThunderZuma(RobotClient robotClient) : base(robotClient)
        {
            boCasted = false;
        }

        public override void CalcActorFrame()
        {
            TMonsterAction pm;
            this.m_nCurrentFrame = -1;
            this.m_nBodyOffset = Actor.GetOffset(this.m_wAppearance);
            pm = Actor.GetRaceByPM(this.m_btRace, this.m_wAppearance);
            if (pm == null)
            {
                return;
            }
            switch (this.m_nCurrentAction)
            {
                case Grobal2.SM_TURN:
                    if ((this.m_nState & Grobal2.STATE_STONE_MODE) != 0)
                    {
                        this.m_nStartFrame = pm.ActDeath.start + this.m_btDir * (pm.ActDeath.frame + pm.ActDeath.skip);
                        this.m_nEndFrame = this.m_nStartFrame;
                        this.m_dwFrameTime = pm.ActDeath.ftime;
                        this.m_dwStartTime = MShare.GetTickCount();
                        this.m_nDefFrameCount = pm.ActDeath.frame;
                    }
                    else
                    {
                        base.CalcActorFrame();
                    }
                    break;
                case Grobal2.SM_LIGHTING:
                    this.m_nStartFrame = pm.ActCritical.start + this.m_btDir * (pm.ActCritical.frame + pm.ActCritical.skip);
                    this.m_nEndFrame = this.m_nStartFrame + pm.ActCritical.frame - 1;
                    this.m_dwFrameTime = pm.ActCritical.ftime;
                    this.m_dwStartTime = MShare.GetTickCount();
                    this.m_dwWarModeTime = MShare.GetTickCount();
                    this.firedir = this.m_btDir;
                    this.Shift(this.m_btDir, 0, 0, 1);
                    this.m_boUseEffect = true;
                    this.m_nEffectStart = 0;
                    this.m_nEffectFrame = 0;
                    this.m_nEffectEnd = 6;
                    this.m_dwEffectStartTime = MShare.GetTickCount();
                    this.m_dwEffectFrameTime = 150;
                    this.m_nCurEffFrame = 0;
                    boCasted = true;
                    break;
                case Grobal2.SM_DIGUP:
                    this.m_nStartFrame = pm.ActDeath.start + this.m_btDir * (pm.ActDeath.frame + pm.ActDeath.skip);
                    this.m_nEndFrame = this.m_nStartFrame + pm.ActDeath.frame - 1;
                    this.m_dwFrameTime = pm.ActDeath.ftime;
                    this.m_dwStartTime = MShare.GetTickCount();
                    this.m_dwWarModeTime = MShare.GetTickCount();
                    this.Shift(this.m_btDir, 0, 0, 1);
                    break;
                default:
                    base.CalcActorFrame();
                    break;
            }
        }

        public override void Run()
        {
            bool bofly;
            if ((this.m_nCurrentFrame - this.m_nStartFrame) == 2)
            {
                if (this.m_nCurrentAction == Grobal2.SM_LIGHTING)
                {
                    if (boCasted == true)
                    {
                        boCasted = false;
                        ClMain.g_PlayScene.NewMagic(this, 80, 80, this.m_nCurrX, this.m_nCurrY, this.m_nTargetX, this.m_nTargetY, this.m_nTargetRecog, magiceff.TMagicType.mtRedThunder, false, 30, ref bofly);
                    }
                }
            }
            base.Run();
        }

        public override int GetDefaultFrame(bool wmode)
        {
            int result;
            TMonsterAction pm;
            pm = Actor.GetRaceByPM(this.m_btRace, this.m_wAppearance);
            if ((this.m_nState & Grobal2.STATE_STONE_MODE) != 0)
            {
                result = pm.ActDeath.start + this.m_btDir * (pm.ActDeath.frame + pm.ActDeath.skip);
            }
            else
            {
                result = base.GetDefaultFrame(wmode);
            }
            return result;
        }
    }
}