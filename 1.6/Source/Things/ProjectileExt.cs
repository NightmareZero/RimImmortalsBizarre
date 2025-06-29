
using Verse;
using RimWorld;
using UnityEngine;
using Verse.Sound;

namespace NzRimImmortalBizarre
{
    public class ProjectileExt : Projectile
    {
        public float DamageAmountMultiplier = 1f;

        public override int DamageAmount => (int)(base.DamageAmount * DamageAmountMultiplier);

        public override float ArmorPenetration => base.ArmorPenetration * DamageAmountMultiplier;

    }
}