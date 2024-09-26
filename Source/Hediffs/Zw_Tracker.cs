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
        private float fgMax = 1000f;

        // 先天一气
        private float yiqi;
        // 先天一气最大值
        private float yqMax = 1000f;

        public Zw_Tracker()
        {
            this.init();
        }

        public BarNum bar1Num => new BarNum("非罡", ref feigang, ref fgMax)
        {
            BarTex = SolidColorMaterials.NewSolidColorTexture(new Color(0.75f, 0.75f, 0.75f)),
        };

        // 暂时关闭第二栏
        public BarNum bar2Num => new BarNum("先天一气", ref yiqi, ref yqMax)
        {
            BarTex = SolidColorMaterials.NewSolidColorTexture(new Color(0.75f, 0.75f, 0.75f)),
            enable = enableSecondBar
        };

        // 暂时关闭图标
        public Texture2D icon => null;

        public bool Visible => enableGizmo();

        public void ExposeData()
        {
            Scribe_Values.Look(ref feigang, "feigang"); // 非罡
            Scribe_Values.Look(ref yiqi, "yiqi");
            Scribe_Values.Look(ref fgMax, "fgMax");
            Scribe_Values.Look(ref yqMax, "yqMax");
        }

        /// <summary>
        /// 增加或者减少非罡值,
        /// 增加不会溢出
        /// </summary>
        /// <param name="value"></param>
        /// <returns>是否有足够的非罡(供消耗)</returns>
        public bool ChangeFg(float value)
        {
            if (value < 0 && feigang < -value)
            {
                return false;
            }
            feigang += value;
            feigang = Mathf.Clamp(feigang, 0, fgMax);
            return true;
        }

        public bool ChangeYq(float value)
        {
            if (value < 0 && yiqi < -value)
            {
                return false;
            }
            yiqi += value;
            yiqi = Mathf.Clamp(yiqi, 0, yqMax);
            return true;
        }

        public void mustChangeFg(float value)
        {
            feigang += value;
            feigang = Mathf.Clamp(feigang, 0, fgMax);
        }

        /// <summary>
        /// 是否有足够的非罡, 会自动取绝对值, 所以传入负数也可以
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool EnoughFg(float value)
        {
            value = Mathf.Abs(value);
            return feigang >= value;
        }

        public bool EnoughYq(float value)
        {
            value = Mathf.Abs(value);
            return yiqi >= value;
        }

        private void init()
        {

        }

        public void Tick()
        {
            // 根据境界修改非罡上限
            var level = pawn.GetRiEnergyRootLevel();
            this.fgMax = level * 100;
            // 根据身体部件修改先天一气上限
            var xs = pawn.GetXinSuPartCount();
            this.yqMax = xs * 100;
        }

        public void injectHediff(Zw_Fg fgHediff)
        {
            this.pawn = fgHediff.pawn;
        }

        private bool enableGizmo()
        {
            // TODO 添加灵芽检测, 有灵芽的时候才显示非罡条
            return fgMax > 0;
        }

        private bool enableSecondBar()
        {
            return yqMax > 0;
        }

    }
}