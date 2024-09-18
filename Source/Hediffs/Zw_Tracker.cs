using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;
using WhoXiuXian;

namespace NzRimImmortalBizarre
{

    [StaticConstructorOnStartup]
    public class Zw_Tracker : IExposable, IEnergyBar
    {

        private Pawn pawn;
        // 非罡
        private float feigang;
        // 非罡最大值
        private float fgMax = 100f;

        // 先天一气
        private float yiqi;
        // 先天一气最大值
        private float yqMax = 100f;

        public Zw_Tracker()
        {
            this.init();
        }

        public BarNum bar1Num => new BarNum("非罡", ref feigang, ref fgMax)
        {
            BarTex = SolidColorMaterials.NewSolidColorTexture(new Color(0.75f, 0.75f, 0.75f)),
        };

        // 暂时关闭第二栏
        public BarNum bar2Num => null;

        // 暂时关闭图标
        public Texture2D icon => null;

        public bool Visible => enableGizmo();

        public void ExposeData()
        {
            Scribe_Values.Look(ref feigang, "feigang"); // 非罡
            Scribe_Values.Look(ref yiqi, "yiqi");
        }

        public void ChangeFg(float value)
        {
            feigang += value;
            feigang = Mathf.Clamp(feigang, 0, fgMax);
        }

        private void init()
        {

        }

        public void injectHediff(Zw_Fg fgHediff) { 
            this.pawn = fgHediff.pawn;
        }

        private bool enableGizmo()
        {
            // TODO 添加灵芽检测, 有灵芽的时候才显示非罡条
            return feigang > 0;
        }

    }
}