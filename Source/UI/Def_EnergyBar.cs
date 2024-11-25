using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using UnityEngine;
using Verse;
namespace NzRimImmortalBizarre
{
    // 可以被获取能量的Hediff
    public interface IEnergyBar
    {
        // 第一栏
        // 必须为ref
        BarNum bar1Num { get; }

        // 第二栏
        // 必须为ref
        BarNum bar2Num { get; }

        // 右侧的图标
        // 必须为ref, 可空
        Texture2D icon { get; }

        // 是否显示
        bool Visible { get; set; }
    }



    public class BarNum
    {
        // ====== 设置部分 ======
        public delegate bool Enable();
        public Enable enable = () => true;
        public string Label;
        // 条材质, 满
        public Texture2D BarTex = SolidColorMaterials.NewSolidColorTexture(new Color(0.7f, 1.0f, 0.9f));
        // 条材质, 空
        public Texture2D EmptyBarTex = SolidColorMaterials.NewSolidColorTexture(Color.clear);

        // ====== 引用部分 ======
        public ref float Num => ref _num;
        private float _num;
        public float Max => _max;
        private float _max = 100;

        // ====== 构造部分 ======
        public BarNum(string label, ref float num, ref float max)
        {
            _num = num;
            _max = max;
            Label = label;
        }

        public override string ToString()
        {
            return $"{Label}: {Num}/{Max}";
        }

    }

    public static class GizmoEnergyBarUtil
    {

        public static bool Enabled(this BarNum barNum)
        {
            return barNum != null && barNum.enable();
        }
    }



}