﻿using SystemModule;

namespace RobotSvr
{
    public class TSkeletonKingMon : TGasKuDeGi
    {
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
                case Grobal2.SM_BACKSTEP:
                case Grobal2.SM_WALK:
                    this.m_nStartFrame = pm.ActWalk.start + this.m_btDir * (pm.ActWalk.frame + pm.ActWalk.skip);
                    this.m_nEndFrame = this.m_nStartFrame + pm.ActWalk.frame - 1;
                    this.m_dwFrameTime = pm.ActWalk.ftime;
                    this.m_dwStartTime = MShare.GetTickCount();
                    this.m_nEffectFrame = pm.ActWalk.start;
                    this.m_nEffectStart = pm.ActWalk.start;
                    this.m_nEffectEnd = pm.ActWalk.start + pm.ActWalk.frame - 1;
                    this.m_dwEffectStartTime = MShare.GetTickCount();
                    this.m_dwEffectFrameTime = this.m_dwFrameTime;
                    this.m_boUseEffect = true;
                    this.m_nMaxTick = pm.ActWalk.usetick;
                    this.m_nCurTick = 0;
                    this.m_nMoveStep = 1;
                    if (this.m_nCurrentAction == Grobal2.SM_WALK)
                    {
                        this.Shift(this.m_btDir, this.m_nMoveStep, 0, this.m_nEndFrame - this.m_nStartFrame + 1);
                    }
                    else
                    {
                        this.Shift(ClFunc.GetBack(this.m_btDir), this.m_nMoveStep, 0, this.m_nEndFrame - this.m_nStartFrame + 1);
                    }
                    break;
                case Grobal2.SM_HIT:
                    this.m_nStartFrame = pm.ActAttack.start + this.m_btDir * (pm.ActAttack.frame + pm.ActAttack.skip);
                    this.m_nEndFrame = this.m_nStartFrame + pm.ActAttack.frame - 1;
                    this.m_dwFrameTime = pm.ActAttack.ftime;
                    this.m_dwStartTime = MShare.GetTickCount();
                    this.m_dwWarModeTime = MShare.GetTickCount();
                    this.Shift(this.m_btDir, 0, 0, 1);
                    this.m_boUseEffect = true;
                    this.firedir = this.m_btDir;
                    this.m_nEffectFrame = this.m_nStartFrame;
                    this.m_nEffectStart = this.m_nStartFrame;
                    this.m_nEffectEnd = this.m_nEndFrame;
                    this.m_dwEffectStartTime = MShare.GetTickCount();
                    this.m_dwEffectFrameTime = this.m_dwFrameTime;
                    break;
                case Grobal2.SM_FLYAXE:
                    this.m_nStartFrame = pm.ActCritical.start + this.m_btDir * (pm.ActCritical.frame + pm.ActCritical.skip);
                    this.m_nEndFrame = this.m_nStartFrame + pm.ActCritical.frame - 1;
                    this.m_dwFrameTime = pm.ActCritical.ftime;
                    this.m_dwStartTime = MShare.GetTickCount();
                    this.m_dwWarModeTime = MShare.GetTickCount();
                    this.Shift(this.m_btDir, 0, 0, 1);
                    this.m_boUseEffect = true;
                    this.firedir = this.m_btDir;
                    this.m_nEffectFrame = this.m_nStartFrame;
                    this.m_nEffectStart = this.m_nStartFrame;
                    this.m_nEffectEnd = this.m_nEndFrame;
                    this.m_dwEffectStartTime = MShare.GetTickCount();
                    this.m_dwEffectFrameTime = this.m_dwFrameTime;
                    break;
                case Grobal2.SM_LIGHTING:
                    this.m_nStartFrame = 80 + pm.ActAttack.start + this.m_btDir * (pm.ActAttack.frame + pm.ActAttack.skip);
                    this.m_nEndFrame = this.m_nStartFrame + pm.ActAttack.frame - 1;
                    this.m_dwFrameTime = pm.ActAttack.ftime;
                    this.m_dwStartTime = MShare.GetTickCount();
                    this.m_dwWarModeTime = MShare.GetTickCount();
                    this.Shift(this.m_btDir, 0, 0, 1);
                    this.m_boUseEffect = true;
                    this.firedir = this.m_btDir;
                    this.m_nEffectFrame = this.m_nStartFrame;
                    this.m_nEffectStart = this.m_nStartFrame;
                    this.m_nEffectEnd = this.m_nEndFrame;
                    this.m_dwEffectStartTime = MShare.GetTickCount();
                    this.m_dwEffectFrameTime = this.m_dwFrameTime;
                    break;
                case Grobal2.SM_STRUCK:
                    this.m_nStartFrame = pm.ActStruck.start + this.m_btDir * (pm.ActStruck.frame + pm.ActStruck.skip);
                    this.m_nEndFrame = this.m_nStartFrame + pm.ActStruck.frame - 1;
                    this.m_dwFrameTime = pm.ActStruck.ftime;
                    this.m_dwStartTime = MShare.GetTickCount();
                    this.m_nEffectFrame = pm.ActStruck.start;
                    this.m_nEffectStart = pm.ActStruck.start;
                    this.m_nEffectEnd = pm.ActStruck.start + pm.ActStruck.frame - 1;
                    this.m_dwEffectStartTime = MShare.GetTickCount();
                    this.m_dwEffectFrameTime = this.m_dwFrameTime;
                    this.m_boUseEffect = true;
                    break;
                case Grobal2.SM_NOWDEATH:
                    this.m_nStartFrame = pm.ActDie.start + this.m_btDir * (pm.ActDie.frame + pm.ActDie.skip);
                    this.m_nEndFrame = this.m_nStartFrame + pm.ActDie.frame - 1;
                    this.m_dwFrameTime = pm.ActDie.ftime;
                    this.m_dwStartTime = MShare.GetTickCount();
                    this.m_nEffectFrame = pm.ActDie.start;
                    this.m_nEffectStart = pm.ActDie.start;
                    this.m_nEffectEnd = pm.ActDie.start + pm.ActDie.frame - 1;
                    this.m_dwEffectStartTime = MShare.GetTickCount();
                    this.m_dwEffectFrameTime = this.m_dwFrameTime;
                    this.m_boUseEffect = true;
                    break;
                default:
                    base.CalcActorFrame();
                    break;
            }
        }

        public override void Run()
        {
            int prv;
            long m_dwEffectFrameTimetime;
            long m_dwFrameTimetime;
            if ((this.m_nCurrentAction == Grobal2.SM_WALK) || (this.m_nCurrentAction == Grobal2.SM_BACKSTEP) || (this.m_nCurrentAction == Grobal2.SM_RUN) || (this.m_nCurrentAction == Grobal2.SM_HORSERUN))
            {
                return;
            }
            this.m_boMsgMuch = false;
            if (this.m_MsgList.Count >= MShare.MSGMUCH)
            {
                this.m_boMsgMuch = true;
            }
            this.RunFrameAction(this.m_nCurrentFrame - this.m_nStartFrame);
            if (this.m_boUseEffect)
            {
                if (this.m_boMsgMuch)
                {
                    m_dwEffectFrameTimetime = HUtil32.Round(this.m_dwEffectFrameTime * 2 / 3);
                }
                else
                {
                    m_dwEffectFrameTimetime = this.m_dwEffectFrameTime;
                }
                if (MShare.GetTickCount() - this.m_dwEffectStartTime > m_dwEffectFrameTimetime)
                {
                    this.m_dwEffectStartTime = MShare.GetTickCount();
                    if (this.m_nEffectFrame < this.m_nEffectEnd)
                    {
                        this.m_nEffectFrame++;
                    }
                    else
                    {
                        this.m_boUseEffect = false;
                    }
                }
            }
            prv = this.m_nCurrentFrame;
            if (this.m_nCurrentAction != 0)
            {
                if ((this.m_nCurrentFrame < this.m_nStartFrame) || (this.m_nCurrentFrame > this.m_nEndFrame))
                {
                    this.m_nCurrentFrame = this.m_nStartFrame;
                }
                if (this.m_boMsgMuch)
                {
                    m_dwFrameTimetime = HUtil32.Round(this.m_dwFrameTime * 2 / 3);
                }
                else
                {
                    m_dwFrameTimetime = this.m_dwFrameTime;
                }
                if (MShare.GetTickCount() - this.m_dwStartTime > m_dwFrameTimetime)
                {
                    if (this.m_nCurrentFrame < this.m_nEndFrame)
                    {
                        this.m_nCurrentFrame++;
                        this.m_dwStartTime = MShare.GetTickCount();
                    }
                    else
                    {
                        this.m_nCurrentAction = 0;
                        this.m_boUseEffect = false;
                        this.BoUseDieEffect = false;
                    }
                    this.m_nCurrentDefFrame = 0;
                    this.m_dwDefFrameTime = MShare.GetTickCount();
                }
            }
            else
            {
                if ((MShare.GetTickCount() - this.m_dwSmoothMoveTime) > 200)
                {
                    if (MShare.GetTickCount() - this.m_dwDefFrameTime > 500)
                    {
                        this.m_dwDefFrameTime = MShare.GetTickCount();
                        this.m_nCurrentDefFrame++;
                        if (this.m_nCurrentDefFrame >= this.m_nDefFrameCount)
                        {
                            this.m_nCurrentDefFrame = 0;
                        }
                    }
                    this.DefaultMotion();
                }
            }
            if (prv != this.m_nCurrentFrame)
            {
                this.m_dwLoadSurfaceTime = MShare.GetTickCount();
            }
        }
    }
}

