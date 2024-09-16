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
    public interface IEnergyHediff
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
        bool Visible { get; }
    }

    public class BarNum
    {
        public string label;
        public float num;
        public float max = 100;

        // 条材质, 满
        public Texture2D barTex = SolidColorMaterials.NewSolidColorTexture(new Color(0.7f, 1.0f, 0.9f));

        // 条材质, 空
        public Texture2D emptyBarTex = SolidColorMaterials.NewSolidColorTexture(Color.clear);

        public void SetBarColor(Color color)
        {
            barTex = SolidColorMaterials.NewSolidColorTexture(color);
        }

    }




}