﻿using System;
using System.Collections;
using SystemModule;

namespace RobotSvr
{
    public class TSnowMon : TActor
    {
        protected TDirectDrawSurface AttackEffectSurface = null;
        protected TDirectDrawSurface AttackEffectSurface2 = null;
        protected TDirectDrawSurface DieEffectSurface = null;
        protected TDirectDrawSurface ChrEffect = null;
        protected bool BoUseDieEffect = false;
        // 0x258
        protected int firedir = 0;
        // 0x25C
        protected int fire16dir = 0;
        // 0c260
        protected int ax = 0;
        // 0x264
        protected int ay = 0;
        // 0x268
        protected int bx = 0;
        protected int by = 0;
        protected bool m_bowChrEffect = false;
        //Constructor  Create()
        public TSnowMon() : base()
        {
            AttackEffectSurface = null;
            AttackEffectSurface2 = null;
            DieEffectSurface = null;
            ChrEffect = null;
            this.m_boUseEffect = false;
            BoUseDieEffect = false;
            m_bowChrEffect = false;
        }
        public override void SetSound()
        {
            base.SetSound();
            if (this.m_boUseMagic && (this.m_CurMagic.MagicSerial > 0))
            {
                this.m_nMagicStartSound = 10000 + this.m_CurMagic.MagicSerial * 10;
                this.m_nMagicFireSound = this.m_nMagicStartSound + 1;
                this.m_nMagicExplosionSound = this.m_nMagicStartSound + 2;
            }
        }

        public override void RunSound()
        {
            this.m_boRunSound = true;
            SetSound();
            switch (this.m_nCurrentAction)
            {
                case Grobal2.SM_STRUCK:
                    if ((this.m_nStruckWeaponSound >= 0))
                    {
                    }
                    if ((this.m_nStruckSound >= 0))
                    {
                    }
                    if ((this.m_nScreamSound >= 0))
                    {
                    }
                    break;
                case Grobal2.SM_NOWDEATH:
                    if ((this.m_nDieSound >= 0))
                    {
                    }
                    break;
                case Grobal2.SM_THROW:
                case Grobal2.SM_HIT:
                case Grobal2.SM_FLYAXE:
                case Grobal2.SM_LIGHTING:
                case Grobal2.SM_DIGDOWN:
                    switch (this.m_wAppearance)
                    {
                        case 250:
                        case 251:
                        case 255:
                        case 256:
                            if (this.m_nCurrentAction == Grobal2.SM_LIGHTING)
                            {
                                if (this.m_nDie2Sound >= 0)
                                {
                                }
                            }
                            else if (this.m_nAttackSound >= 0)
                            {
                            }
                            break;
                        default:
                            if (this.m_nAttackSound >= 0)
                            {
                            }
                            break;
                    }
                    break;
                case Grobal2.SM_ALIVE:
                case Grobal2.SM_DIGUP:
                    break;
                case Grobal2.SM_SPELL:
                    break;
            }
        }

        public override void CalcActorFrame()
        {
            this.m_nCurrentFrame = -1;
            this.m_nBodyOffset = Actor.GetOffset(this.m_wAppearance);
            TMonsterAction pm = Actor.GetRaceByPM(this.m_btRace, this.m_wAppearance);
            if (pm == null)
            {
                return;
            }
            switch (this.m_nCurrentAction)
            {
                case Grobal2.SM_TURN:
                    this.m_nStartFrame = pm.ActStand.start + this.m_btDir * (pm.ActStand.frame + pm.ActStand.skip);
                    this.m_nEndFrame = this.m_nStartFrame + pm.ActStand.frame - 1;
                    this.m_dwFrameTime = pm.ActStand.ftime;
                    this.m_dwStartTime = MShare.GetTickCount();
                    this.m_nDefFrameCount = pm.ActStand.frame;
                    this.Shift(this.m_btDir, 0, 0, 1);
                    break;
                case Grobal2.SM_WALK:
                    this.m_nStartFrame = pm.ActWalk.start + this.m_btDir * (pm.ActWalk.frame + pm.ActWalk.skip);
                    this.m_nEndFrame = this.m_nStartFrame + pm.ActWalk.frame - 1;
                    this.m_dwFrameTime = pm.ActWalk.ftime;
                    this.m_dwStartTime = MShare.GetTickCount();
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
                    firedir = this.m_btDir;
                    this.m_nEffectFrame = this.m_nStartFrame;
                    this.m_nEffectStart = this.m_nStartFrame;
                    if (this.m_btRace == 20)
                    {
                        this.m_nEffectEnd = this.m_nEndFrame + 1;
                    }
                    else
                    {
                        this.m_nEffectEnd = this.m_nEndFrame;
                    }
                    this.m_dwEffectStartTime = MShare.GetTickCount();
                    this.m_dwEffectFrameTime = this.m_dwFrameTime;
                    this.m_boUseEffect = false;
                    if (this.m_btRace == 51)
                    {
                        this.m_boUseEffect = true;
                    }
                    break;
                case Grobal2.SM_LIGHTING:
                    this.m_nStartFrame = pm.ActCritical.start + this.m_btDir * (pm.ActCritical.frame + pm.ActCritical.skip);
                    this.m_nEndFrame = this.m_nStartFrame + pm.ActCritical.frame - 1;
                    this.m_dwFrameTime = pm.ActCritical.ftime;
                    if ((this.m_nMagicNum == 2) && (new ArrayList(new int[] { 38, 39, 46 }).Contains(this.m_btRace)))
                    {
                        this.m_nStartFrame = pm.ActDeath.start + this.m_btDir * (pm.ActDeath.frame + pm.ActDeath.skip);
                        this.m_nEndFrame = this.m_nStartFrame + pm.ActDeath.frame - 1;
                        this.m_dwFrameTime = pm.ActDeath.ftime;
                    }
                    this.m_dwStartTime = MShare.GetTickCount();
                    this.m_dwWarModeTime = MShare.GetTickCount();
                    this.Shift(this.m_btDir, 0, 0, 1);
                    this.m_boUseEffect = true;
                    this.m_nEffectFrame = this.m_nStartFrame;
                    this.m_nEffectStart = this.m_nStartFrame;
                    this.m_nEffectEnd = this.m_nEndFrame;
                    this.m_dwEffectStartTime = MShare.GetTickCount();
                    this.m_dwEffectFrameTime = this.m_dwFrameTime;
                    break;
                case Grobal2.SM_SPELL:
                    this.m_nStartFrame = pm.ActAttack.start + this.m_btDir * (pm.ActAttack.frame + pm.ActAttack.skip);
                    this.m_nEndFrame = this.m_nStartFrame + pm.ActAttack.frame - 1;
                    this.m_dwFrameTime = pm.ActAttack.ftime;
                    this.m_dwStartTime = MShare.GetTickCount();
                    this.m_nCurEffFrame = 0;
                    this.m_boUseMagic = true;
                    this.m_nMagLight = 2;
                    this.m_nSpellFrame = pm.ActCritical.frame;
                    this.m_dwWaitMagicRequest = MShare.GetTickCount();
                    this.m_boWarMode = true;
                    this.m_dwWarModeTime = MShare.GetTickCount();
                    this.Shift(this.m_btDir, 0, 0, 1);
                    break;
                case Grobal2.SM_STRUCK:
                    this.m_nStartFrame = pm.ActStruck.start + this.m_btDir * (pm.ActStruck.frame + pm.ActStruck.skip);
                    this.m_nEndFrame = this.m_nStartFrame + pm.ActStruck.frame - 1;
                    this.m_dwFrameTime = this.m_dwStruckFrameTime;
                    // pm.ActStruck.ftime;
                    this.m_dwStartTime = MShare.GetTickCount();
                    break;
                case Grobal2.SM_DEATH:
                    this.m_nStartFrame = pm.ActDie.start + this.m_btDir * (pm.ActDie.frame + pm.ActDie.skip);
                    this.m_nEndFrame = this.m_nStartFrame + pm.ActDie.frame - 1;
                    this.m_nStartFrame = this.m_nEndFrame;
                    this.m_dwFrameTime = pm.ActDie.ftime;
                    this.m_dwStartTime = MShare.GetTickCount();
                    break;
                case Grobal2.SM_NOWDEATH:
                    this.m_nStartFrame = pm.ActDie.start + this.m_btDir * (pm.ActDie.frame + pm.ActDie.skip);
                    this.m_nEndFrame = this.m_nStartFrame + pm.ActDie.frame - 1;
                    this.m_dwFrameTime = pm.ActDie.ftime;
                    this.m_dwStartTime = MShare.GetTickCount();
                    if ((new ArrayList(new int[] { 40, 65 }).Contains(this.m_btRace)))
                    {
                        BoUseDieEffect = true;
                    }
                    // 38, 39,
                    if (new ArrayList(new int[] { 51 }).Contains(this.m_btRace))
                    {
                        BoUseDieEffect = true;
                    }
                    break;
                case Grobal2.SM_SKELETON:
                    this.m_nStartFrame = pm.ActDeath.start;
                    this.m_nEndFrame = this.m_nStartFrame + pm.ActDeath.frame - 1;
                    this.m_dwFrameTime = pm.ActDeath.ftime;
                    this.m_dwStartTime = MShare.GetTickCount();
                    if (this.m_btRace == 39)
                    {
                        this.m_nStartFrame = pm.ActDeath.start + this.m_btDir * (pm.ActDeath.frame + pm.ActDeath.skip);
                        this.m_nEndFrame = this.m_nStartFrame + pm.ActDeath.frame - 1;
                        this.m_dwFrameTime = pm.ActDeath.ftime;
                        this.m_dwStartTime = MShare.GetTickCount();
                        this.m_dwWarModeTime = MShare.GetTickCount();
                        this.Shift(this.m_btDir, 0, 0, 1);
                        this.m_boUseEffect = true;
                        this.m_nEffectFrame = this.m_nStartFrame;
                        this.m_nEffectStart = this.m_nStartFrame;
                        this.m_nEffectEnd = this.m_nEndFrame;
                        this.m_dwEffectStartTime = MShare.GetTickCount();
                        this.m_dwEffectFrameTime = this.m_dwFrameTime;
                    }
                    break;
            }
        }

        public override int GetDefaultFrame(bool wmode)
        {
            int result;
            int cf;
            TMonsterAction pm;
            result = 0;
            pm = Actor.GetRaceByPM(this.m_btRace, this.m_wAppearance);
            if (pm == null)
            {
                return result;
            }
            if (this.m_boDeath)
            {
                if (this.m_boSkeleton)
                {
                    result = pm.ActDeath.start;
                }
                else
                {
                    result = pm.ActDie.start + this.m_btDir * (pm.ActDie.frame + pm.ActDie.skip) + (pm.ActDie.frame - 1);
                }
            }
            else
            {
                this.m_nDefFrameCount = pm.ActStand.frame;
                if (this.m_nCurrentDefFrame < 0)
                {
                    cf = 0;
                }
                else if (this.m_nCurrentDefFrame >= pm.ActStand.frame)
                {
                    cf = 0;
                }
                else
                {
                    cf = this.m_nCurrentDefFrame;
                }
                result = pm.ActStand.start + this.m_btDir * (pm.ActStand.frame + pm.ActStand.skip) + cf;
            }
            return result;
        }

        public override void Run()
        {
            long dwEffectFrameTimetime;
            base.Run();
            if (this.m_boUseEffect)
            {
                if (this.m_boMsgMuch)
                {
                    dwEffectFrameTimetime = Math.Round(this.m_dwEffectFrameTime * 2 / 3);
                }
                else
                {
                    dwEffectFrameTimetime = this.m_dwEffectFrameTime;
                }
                if (MShare.GetTickCount() - this.m_dwEffectStartTime > dwEffectFrameTimetime)
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
        }
    }
}
