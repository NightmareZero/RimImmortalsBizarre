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
        // 选项
        // 不可修改
        EnergyBarOpt opt { get; }

        // 第一栏
        // 必须为ref
        BarNum bar1Num { get; }

        // 第二栏
        // 必须为ref
        BarNum bar2Num { get; }

        // 右侧的图标
        // 必须为ref, 可空
        Texture2D icon { get; }
    }

    public class BarNum
    {
        public string label;
        public float current;
        public float max;
    }

    public class EnergyBarOpt
    {
        // 第一列颜色
        public Color FirstColor = new Color(0.7f, 1.0f, 0.9f);

        // 第二列颜色
        public Color SecondColor = new Color(0.3f, 0.4f, 0.4f);

    }


}