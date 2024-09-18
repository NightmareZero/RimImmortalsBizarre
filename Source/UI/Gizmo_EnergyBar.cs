﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using UnityEngine;
using Verse;
namespace NzRimImmortalBizarre
{

	public class Gizmo_EnergyBar : Gizmo
	{
		public IEnergyBar data;

		private float barLength = 122f; // 每个bar的长度

		private float barLineHeight = 10f; // 每个bar的高度
		private float gizmoLength = 180f; // 整个gizmo的长度

		public override bool Visible
		{
			get
			{
				// 如果没有数据, 或者数据不完整, 则不显示
				if (data == null) return false;
				if (!data.bar1Num.Enabled() && !data.bar2Num.Enabled() && data.icon == null) return false;

				// 如果设置了不显示, 则不显示
				return data.Visible;
			}
		}
		public override float GetWidth(float maxWidth)
		{
			return gizmoLength;
		}

		public Gizmo_EnergyBar(IEnergyBar hediff)
		{
			Order = -110f;
			this.data = hediff;

			// 根据opt配置
			this.configOpt();
		}

		private void configOpt()
		{
			// 根据是否启用了能量条, 设置是否显示, 以及相关偏移量
#if DEBUG
			Log.Message("Gizmo_EnergyBar:" + " bar1: open=" + (data.bar1Num != null) + " bar2: open=" + (data.bar2Num != null) + " icon: open=" + (data.icon != null));
#endif
			// 如果没有图标
			if (data.icon == null)
			{
				barLength = 174f;
				barLineHeight = 20f;
			}

			// 如果只有图标
			if (!data.bar1Num.Enabled() && !data.bar2Num.Enabled())
			{
				barLength = 60f;
			}

		}

		public override GizmoResult GizmoOnGUI(Vector2 topLeft, float maxWidth, GizmoRenderParms parms)
		{
			Rect rect = new Rect(topLeft.x, topLeft.y, GetWidth(maxWidth), 75f);
			Rect rect2 = rect.ContractedBy(2f);
			Widgets.DrawWindowBackground(rect);

			// 偏移量
			float bar1Fix = data.bar2Num == null ? 0 : 25f;

			// 画第一栏
			Rect rect3 = rect2;
			rect3.width = barLength;
			rect3.height = rect.height / 2f;
			rect3.yMin = rect2.y + bar1Fix;
			DrawBar(rect3, data.bar1Num);

			// 画第二栏
			if (data.bar2Num != null)
			{
				Rect rect4 = rect2;
				rect4.width = barLength;
				rect4.yMin = rect2.y + rect2.height / 2f;
				DrawBar(rect4, data.bar2Num);
			}

			// 画图标
			if (data.icon != null)
			{
				Rect rect5 = new Rect(rect2.x + 125, rect2.y + 8, 50, 60);
				GUI.DrawTexture(rect5, data.icon);
				Text.Anchor = TextAnchor.UpperLeft;

			}
			return new GizmoResult(GizmoState.Clear);
		}
		private void DrawBar(Rect rect, BarNum n)
		{
			Text.Font = GameFont.Small;
			Rect rect1 = new Rect(rect.x, rect.y, 60f, 25f);
			Rect rect2 = new Rect(rect.x + 60f, rect.y, barLength - 10f, 25f);
			Rect rect3 = new Rect(rect.x, rect.y + 25f, barLength, barLineHeight);
			Widgets.Label(rect1, n.Label);
			Widgets.Label(rect2, n.Num.ToString("F0") + " / " + n.Max.ToString("F0"));
			Widgets.FillableBar(rect3, n.Num / n.Max, n.BarTex, n.EmptyBarTex, doBorder: false);
		}

	}

}
