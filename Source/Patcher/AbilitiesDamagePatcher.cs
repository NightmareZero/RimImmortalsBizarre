using RimWorld;
using Verse;

namespace NzRimImmortalBizarre
{
    public interface DamagePatcher
    {
        int PatchType();
        void SetDamageMultiplier(float multiplier);
        void SetArmorPenetrationMultiplier(float penetration);
    }


    public static class Patcher
    {
        public const int AoJing = 1;
        public const int ZoWangDao = 2;

        public static void DoDamagePatch(CompAbilityEffect effect, DamagePatcher patcher)
        {
            int patchType = patcher.PatchType();
            switch (patchType)
            {
                case AoJing:
                    PatchAoJing(effect, patcher);
                    break;
                case ZoWangDao:
                    PatchZoWangDao(effect, patcher);
                    break;
                default:
                    // 处理未知的 patchType
                    break;
            }
        }

        private static void PatchAoJing(CompAbilityEffect effect, DamagePatcher patcher)
        {
            Pawn caster = effect?.parent?.pawn;
            if (caster == null)
            {
                Log.Error("NzRimImmortalBizarre.PatchAoJing: caster is null");
                return;
            }

            float multiplier = Utils.GetAscendHediff(caster)?.Severity ?? 0.0f;
            multiplier = multiplier * 2.5f + 1.0f;

            #if DEBUG
            Log.Message("NzRimImmortalBizarre.PatchAoJing: multiplier = " + multiplier);
            #endif

            patcher.SetDamageMultiplier(multiplier);
            patcher.SetArmorPenetrationMultiplier(multiplier);
        }

        private static void PatchZoWangDao(CompAbilityEffect effect, DamagePatcher patcher)
        {
            // 实现 PatchZoWangDao 的逻辑
        }
    }
}