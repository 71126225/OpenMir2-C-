﻿using SystemModule;

namespace RobotSvr
{
    public class TGhostShipMonster : TActor
    {
        public bool FFireBall = false;
        public bool FLighting = false;
        protected int ax = 0;
        protected int ax2 = 0;
        protected int ay = 0;
        protected int ay2 = 0;
        protected byte firedir = 0;

        public TGhostShipMonster() : base()
        {
            this.m_boUseEffect = false;
            FFireBall = false;
            FLighting = false;
        }

        public override void CalcActorFrame()
        {
            TMonsterAction pm;
            this.m_boUseMagic = false;
            this.m_boUseEffect = false;
            this.m_boHitEffect = false;
            this.m_boReverseFrame = false;
            this.m_nCurrentFrame = -1;
            this.m_nHitEffectNumber = 0;
            this.m_nBodyOffset = Actor.GetOffset(this.m_wAppearance);
            pm = Actor.GetRaceByPM(this.m_btRace, this.m_wAppearance);
            if (pm == null)
            {
                return;
            }
            switch (this.m_nCurrentAction)
            {
                case Grobal2.SM_HIT:
                    this.m_nStartFrame = this.m_Action.ActAttack.start + this.m_btDir * (this.m_Action.ActAttack.frame + this.m_Action.ActAttack.skip);
                    this.m_nEndFrame = this.m_nStartFrame + this.m_Action.ActAttack.frame - 1;
                    this.m_dwFrameTime = this.m_Action.ActAttack.ftime;
                    this.m_dwStartTime = MShare.GetTickCount();
                    this.m_boWarMode = true;
                    this.m_dwWarModeTime = MShare.GetTickCount();
                    if (this.m_wAppearance == 354)
                    {
                        this.m_boHitEffect = true;
                        this.m_nMagLight = 2;
                        this.m_nHitEffectNumber = 3;
                        this.m_nHitEffectNumber += 101;
                    }
                    if (this.m_wAppearance == 815)
                    {
                        this.m_boHitEffect = true;
                        this.m_nMagLight = 2;
                        this.m_nHitEffectNumber = 3;
                        this.m_nHitEffectNumber += 301;
                    }
                    this.Shift(this.m_btDir, 0, 0, 1);
                    break;
                case Grobal2.SM_LIGHTING:
                    switch (this.m_wAppearance)
                    {
                        case 354:
                        case 356:
                        case 359:
                        case 813:
                        case 815:
                            this.m_nStartFrame = this.m_Action.ActCritical.start + this.m_btDir * (this.m_Action.ActCritical.frame + this.m_Action.ActCritical.skip);
                            this.m_nEndFrame = this.m_nStartFrame + this.m_Action.ActCritical.frame - 1;
                            this.m_dwFrameTime = this.m_Action.ActCritical.ftime;
                            this.m_dwStartTime = MShare.GetTickCount();
                            this.m_dwWarModeTime = MShare.GetTickCount();
                            if ((this.m_wAppearance == 354) || (this.m_wAppearance == 815))
                            {
                                this.m_boSmiteWideHit2 = true;
                            }
                            break;
                        default:
                            base.CalcActorFrame();
                            return;
                            break;
                    }
                    break;
                case Grobal2.SM_LIGHTING_1:
                    switch (this.m_wAppearance)
                    {
                        case 356:
                        case 813:
                            this.m_nStartFrame = this.m_Action.ActCritical.start + this.m_btDir * (this.m_Action.ActCritical.frame + this.m_Action.ActCritical.skip);
                            this.m_nEndFrame = this.m_nStartFrame + this.m_Action.ActCritical.frame - 1;
                            this.m_dwFrameTime = this.m_Action.ActCritical.ftime;
                            this.m_dwStartTime = MShare.GetTickCount();
                            this.m_dwWarModeTime = MShare.GetTickCount();
                            FLighting = true;
                            break;
                    }
                    break;
                case Grobal2.SM_DIGUP:
                    switch (this.m_wAppearance)
                    {
                        case 351:
                        case 827:
                            this.m_nStartFrame = pm.ActDeath.start + this.m_btDir * (pm.ActDeath.frame + pm.ActDeath.skip);
                            break;
                        default:
                            base.CalcActorFrame();
                            return;
                            break;
                    }
                    this.m_nEndFrame = this.m_nStartFrame + pm.ActDeath.frame - 1;
                    this.m_dwFrameTime = pm.ActDeath.ftime;
                    this.m_dwStartTime = MShare.GetTickCount();
                    this.Shift(this.m_btDir, 0, 0, 1);
                    break;
                case Grobal2.SM_SPELL:
                    if (this.m_CurMagic.MagicSerial == 23)
                    {
                        this.m_nStartFrame = this.m_Action.ActCritical.start + this.m_btDir * (this.m_Action.ActCritical.frame + this.m_Action.ActCritical.skip);
                        this.m_nEndFrame = this.m_nStartFrame + this.m_Action.ActCritical.frame - 1;
                        this.m_dwFrameTime = this.m_Action.ActCritical.ftime;
                        this.m_dwStartTime = MShare.GetTickCount();
                        this.m_dwWarModeTime = MShare.GetTickCount();
                    }
                    else
                    {
                        this.m_nStartFrame = pm.ActAttack.start + this.m_btDir * (pm.ActAttack.frame + pm.ActAttack.skip);
                        this.m_nEndFrame = this.m_nStartFrame + pm.ActAttack.frame - 1;
                        this.m_dwFrameTime = pm.ActAttack.ftime;
                        this.m_dwStartTime = MShare.GetTickCount();
                    }
                    this.m_nCurEffFrame = 0;
                    this.m_boUseMagic = true;
                    this.m_nMagLight = 2;
                    this.m_nSpellFrame = pm.ActAttack.frame;
                    this.m_dwWaitMagicRequest = MShare.GetTickCount();
                    this.m_boWarMode = true;
                    this.m_dwWarModeTime = MShare.GetTickCount();
                    this.Shift(this.m_btDir, 0, 0, 1);
                    break;
                case Grobal2.SM_FLYAXE:
                    this.m_nStartFrame = pm.ActAttack.start + this.m_btDir * (pm.ActAttack.frame + pm.ActAttack.skip);
                    this.m_nEndFrame = this.m_nStartFrame + pm.ActAttack.frame - 1;
                    this.m_dwFrameTime = pm.ActAttack.ftime;
                    this.m_dwStartTime = MShare.GetTickCount();
                    this.m_dwWarModeTime = MShare.GetTickCount();
                    this.Shift(this.m_btDir, 0, 0, 1);
                    break;
                default:
                    base.CalcActorFrame();
                    break;
            }
        }

        public override void RunFrameAction(int frame)
        {
            //TNormalDrawEffect neff;
            //if (this.m_nCurrentAction == Grobal2.SM_LIGHTING)
            //{
            //    if ((frame == 5) && this.m_boSmiteWideHit2)
            //    {
            //        this.m_boSmiteWideHit2 = false;
            //        neff = new TNormalDrawEffect(this.m_nCurrX, this.m_nCurrY, WMFile.Units.WMFile.g_WMagic2Images, 1391, 14, 75, true);
            //        if (neff != null)
            //        {
            //            ClMain.g_PlayScene.m_EffectList.Add(neff);
            //        }
            //        if (this.m_wAppearance == 354)
            //        {
            //            ClMain.g_ShakeScreen.SetScrShake_X(4);
            //            ClMain.g_ShakeScreen.SetScrShake_Y(3);
            //        }
            //        if (this.m_wAppearance == 815)
            //        {
            //            ClMain.g_ShakeScreen.SetScrShake_X(7);
            //            ClMain.g_ShakeScreen.SetScrShake_Y(5);
            //        }
            //    }
            //}
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
                else if (this.m_nCurrentDefFrame >= this.m_nDefFrameCount)
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
            int prv;
            long dwFrameTimetime;
            bool bofly;
            TFlyingAxe meff;
            TFlyingArrow meff2;
            if ((this.m_nCurrentAction == Grobal2.SM_WALK) || (this.m_nCurrentAction == Grobal2.SM_BACKSTEP) || (this.m_nCurrentAction == Grobal2.SM_RUN) || (this.m_nCurrentAction == Grobal2.SM_HORSERUN) || (this.m_nCurrentAction == Grobal2.SM_RUSH) || (this.m_nCurrentAction == Grobal2.SM_RUSHEX) || (this.m_nCurrentAction == Grobal2.SM_RUSHKUNG))
            {
                return;
            }
            this.m_boMsgMuch = false;
            if (this.m_MsgList.Count >= 2)
            {
                this.m_boMsgMuch = true;
            }
            RunFrameAction(this.m_nCurrentFrame - this.m_nStartFrame);
            prv = this.m_nCurrentFrame;
            if (this.m_nCurrentAction != 0)
            {
                if ((this.m_nCurrentFrame < this.m_nStartFrame) || (this.m_nCurrentFrame > this.m_nEndFrame))
                {
                    this.m_nCurrentFrame = this.m_nStartFrame;
                }
                if (this.m_boMsgMuch)
                {
                    // Round(m_dwFrameTime / 1.6)
                    dwFrameTimetime = HUtil32.Round(this.m_dwFrameTime * 2 / 3);
                }
                else
                {
                    dwFrameTimetime = this.m_dwFrameTime;
                }
                if (MShare.GetTickCount() - this.m_dwStartTime > dwFrameTimetime)
                {
                    if (this.m_nCurrentFrame < this.m_nEndFrame)
                    {
                        if (this.m_boUseMagic)
                        {
                            if (this.m_nCurEffFrame == this.m_nSpellFrame - 2)
                            {
                                if (this.m_CurMagic.ServerMagicCode >= 0)
                                {
                                    this.m_nCurrentFrame++;
                                    this.m_nCurEffFrame++;
                                    this.m_dwStartTime = MShare.GetTickCount();
                                }
                            }
                            else
                            {
                                if (this.m_nCurrentFrame < this.m_nEndFrame - 1)
                                {
                                    this.m_nCurrentFrame++;
                                }
                                this.m_nCurEffFrame++;
                                this.m_dwStartTime = MShare.GetTickCount();
                            }
                        }
                        else
                        {
                            this.m_nCurrentFrame++;
                            this.m_dwStartTime = MShare.GetTickCount();
                        }
                    }
                    else
                    {
                        if (this.m_boDelActionAfterFinished)
                        {
                            this.m_boDelActor = true;
                        }
                        this.ActionEnded();
                        this.m_nCurrentAction = 0;
                        this.m_boUseMagic = false;
                        this.m_boUseEffect = false;
                        this.m_boHitEffect = false;
                    }
                    if (this.m_boUseMagic)
                    {
                        if (this.m_nCurEffFrame == this.m_nSpellFrame - 1)
                        {
                            if (this.m_CurMagic.ServerMagicCode > 0)
                            {
                                TUseMagicInfo _wvar1 = this.m_CurMagic;
                                ClMain.g_PlayScene.NewMagic(this, _wvar1.ServerMagicCode, _wvar1.EffectNumber, this.m_nCurrX, this.m_nCurrY, _wvar1.targx, _wvar1.targy, _wvar1.target, _wvar1.EffectType, _wvar1.Recusion, _wvar1.anitime, ref bofly, _wvar1.magfirelv);
                                if (bofly)
                                {
                                }
                                else
                                {
                                }
                            }
                            this.m_CurMagic.ServerMagicCode = 0;
                        }
                    }
                }
                this.m_nCurrentDefFrame = 0;
                this.m_dwDefFrameTime = MShare.GetTickCount();
            }
            else if ((MShare.GetTickCount() - this.m_dwSmoothMoveTime) > 200)
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
            if (prv != this.m_nCurrentFrame)
            {
                this.m_dwLoadSurfaceTime = MShare.GetTickCount();
            }
        }
    }
}

