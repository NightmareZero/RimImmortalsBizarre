using System.Linq;
using RimWorld;
using UnityEngine;
using Verse;

namespace NzRimImmortalBizarre
{

    public class Verb_CastPsycast : Verb_CastAbility
    {
        private const float StatLabelOffsetY = 1f;


        public override void OrderForceTarget(LocalTargetInfo target)
        {
            if (IsApplicableTo(target))
            {
                base.OrderForceTarget(target);
            }
        }

        public override bool ValidateTarget(LocalTargetInfo target, bool showMessages = true)
        {
            if (!base.ValidateTarget(target, showMessages))
            {
                return false;
            }

            bool num = ability.EffectComps.All((CompAbilityEffect e) => e.Props.canTargetBosses);
            Pawn pawn = target.Pawn;
            if (!num && pawn != null && pawn.kindDef.isBoss)
            {
                Messages.Message("CommandPsycastInsanityImmune".Translate(), caster, MessageTypeDefOf.RejectInput, historical: false);
                return false;
            }


            return true;
        }

        public override void OnGUI(LocalTargetInfo target)
        {
            bool flag = ability.EffectComps.Any((CompAbilityEffect e) => e.Props.psychic);
            bool flag2 = ability.EffectComps.All((CompAbilityEffect e) => e.Props.canTargetBosses);
            Texture2D texture2D = UIIcon;


            if (target.IsValid && CanHitTarget(target) && IsApplicableTo(target))
            {
                foreach (LocalTargetInfo affectedTarget in ability.GetAffectedTargets(target))
                {
                    if (!flag2 && affectedTarget.Pawn.kindDef.isBoss)
                    {
                        DrawIneffectiveWarning(affectedTarget, "IneffectivePsychicImmune".Translate());
                    }
                    else if (flag)
                    {
                        if (true)
                        {
                            DrawSensitivityStat(affectedTarget);
                        }
                        // else
                        // {
                        //     DrawIneffectiveWarning(affectedTarget);
                        // }
                    }
                }

                if (ability.EffectComps.Any((CompAbilityEffect e) => !e.Valid(target)))
                {
                    texture2D = TexCommand.CannotShoot;
                }
            }
            else
            {
                texture2D = TexCommand.CannotShoot;
            }

            GenUI.DrawMouseAttachment(texture2D);


            DrawAttachmentExtraLabel(target);
        }

        private void DrawIneffectiveWarning(LocalTargetInfo target, string text = null)
        {
            DrawIneffectiveWarningStatic(target, text);
        }

        public static void DrawIneffectiveWarningStatic(LocalTargetInfo target, string text = null)
        {
            if (target.Pawn != null)
            {
                Vector3 drawPos = target.Pawn.DrawPos;
                drawPos.z += 1f;
                if (text == null)
                {
                    text = "Ineffective".Translate();
                }

                GenMapUI.DrawText(new Vector2(drawPos.x, drawPos.z), text, Color.red);
            }
        }

        private void DrawSensitivityStat(LocalTargetInfo target)
        {
            if (target.Pawn != null && !target.Pawn.IsHiddenFromPlayer())
            {
                Pawn pawn = target.Pawn;
                float statValue = pawn.GetStatValue(StatDefOf.PsychicSensitivity);
                Vector3 drawPos = pawn.DrawPos;
                drawPos.z += 1f;
                GenMapUI.DrawText(new Vector2(drawPos.x, drawPos.z), (string)(StatDefOf.PsychicSensitivity.LabelCap + ": ") + statValue, (statValue > float.Epsilon) ? Color.white : Color.red);
            }
        }
    }
}