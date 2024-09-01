using RimWorld;
using UnityEngine;
using Verse;

namespace NzRimImmortalBizarre
{
    public class ProjectileAj : Projectile
    {

        public void SetDamageMultiplier(float multiplier)
        {
           weaponDamageMultiplier = multiplier;
        }
        
    }
}