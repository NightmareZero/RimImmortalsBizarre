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

	public class Gizmo_EnergyBar : Gizmo
	{
		public IEnergyHediff data;

		private float barLength = 122f; // 每个bar的长度
		private float gizmoLength = 180f; // 整个gizmo的长度

		public override bool Visible
		{
			get
			{
				// 如果没有数据, 或者数据不完整, 则不显示
				if (data == null) return false;
				if (data.bar1Num == null && data.bar2Num == null && data.icon == null) return false;

				// 如果设置了不显示, 则不显示
				return data.Visible;
			}
		}
		public override float GetWidth(float maxWidth)
		{
			return gizmoLength;
		}

		public Gizmo_EnergyBar(IEnergyHediff energy)
		{
			Order = -110f;
			this.data = energy;

			// 根据opt配置
			this.configOpt();
		}

		private void configOpt()
		{
			// 根据是否启用了能量条, 设置是否显示, 以及相关偏移量
			
			// 如果没有图标
			if (data.icon == null)
			{
				barLength = 176f;
			}

			// 如果只有图标
			if (data.bar1Num == null && data.bar2Num == null)
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
			rect3.yMin = bar1Fix;
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
			Rect rect5 = new Rect(rect2.x + 125, rect2.y + 8, 50, 60);
			GUI.DrawTexture(rect5, data.icon);
			Text.Anchor = TextAnchor.UpperLeft;
			return new GizmoResult(GizmoState.Clear);
		}
		private void DrawBar(Rect rect, BarNum n)
		{
			Text.Font = GameFont.Small;
			Rect rect1 = new Rect(rect.x, rect.y, 60f, 25f);
			Rect rect2 = new Rect(rect.x + 60f, rect.y, 100f, 25f);
			Rect rect3 = new Rect(rect.x, rect.y + 25f, 120f, 10f);
			Widgets.Label(rect1, n.label);
			Widgets.Label(rect2, n.num.ToString("F0") + " / " + n.max.ToString("F0"));
			Widgets.FillableBar(rect3, n.num / n.max, n.barTex, n.emptyBarTex, doBorder: false);
		}
		// private void Draw2Bar(Rect rect, float a, float b, string label)
		// {
		// 	Text.Font = GameFont.Small;
		// 	Rect rect1 = new Rect(rect.x, rect.y, 60f, 25f);
		// 	Rect rect2 = new Rect(rect.x + 60f, rect.y, 100f, 25f);
		// 	Rect rect3 = new Rect(rect.x, rect.y + 25f, 120f, 10f);
		// 	Widgets.Label(rect1, label);
		// 	float num = (a / b) * 100f;
		// 	if (num >= 100) num = 100;
		// 	Widgets.Label(rect2, (num).ToString("F0") + "%");
		// 	Widgets.FillableBar(rect3, a / b, SecondBarTex, EmptyBarTex, doBorder: false);
		// }
	}

}
