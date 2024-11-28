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
				if (data.bar1Num == null && data.bar2Num == null && data.icon == null) return false;

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

			// 重设界面条
			this.ResetDrawCfg();
		}

		public void ResetDrawCfg()
		{
			// 根据是否启用了能量条, 设置是否显示, 以及相关偏移量
#if DEBUG
			Log.Message("Gizmo_EnergyBar:" + " bar1: open=" + (data.bar1Num != null) + " bar2: open=" + (data.bar2Num != null) + " icon: open=" + (data.icon != null));
#endif
			// 如果没有图标
			if (data.icon == null)
			{
				barLength = 174f;
			}

			// 如果只有图标
			if ((data.bar1Num == null || !data.bar1Num.Enabled()) &&
			(data.bar2Num == null || !data.bar2Num.Enabled()))
			{
				barLength = 60f;
				barLineHeight = 20f;
			}
		}

		public override GizmoResult GizmoOnGUI(Vector2 topLeft, float maxWidth, GizmoRenderParms parms)
		{
			Rect rect = new Rect(topLeft.x, topLeft.y, GetWidth(maxWidth), 75f);

			try
			{

				Rect rect2 = rect.ContractedBy(2f);
				Widgets.DrawWindowBackground(rect);

				// 偏移量
				float bar1Fix = doubleBar() ? 0 : 15f;

				// 画第一栏
				Rect rect3 = rect2;
				rect3.width = barLength;
				rect3.height = rect.height / 2f;
				rect3.yMin = rect2.y + bar1Fix;
				DrawBar(rect3, data.bar1Num);

				// 画第二栏
				if (doubleBar())
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
			catch (Exception e)
			{
				e.PrintExceptionWithStackTrace();
				this.data.Visible = false;
				return new GizmoResult(GizmoState.Clear);
			}
		}
		private void DrawBar(Rect rect, BarNum n)
		{
			// 计算大小
			var fontLength = 60f;
			var titleHeight = doubleBar() ? 20f : 25f;
			var barHeight = doubleBar() ? 10f : 16f;
			Text.Font = GameFont.Small;

			// 绘制区域
			Rect rect1 = new Rect(rect.x, rect.y, fontLength, titleHeight); // 标题
			Widgets.Label(rect1, n.Label);
			Rect rect2 = new Rect(rect.x + fontLength, rect.y, barLength - 10f, titleHeight); // 间隔
			Widgets.Label(rect2, n.Num.ToString("F0") + " / " + n.Max.ToString("F0"));
			Rect rect3 = new Rect(rect.x, rect.y + titleHeight, barLength, barHeight); // 进度条
			Widgets.FillableBar(rect3, n.Num / n.Max, n.BarTex, n.EmptyBarTex, doBorder: false);
		}

		private bool doubleBar()
		{
			return data.bar2Num != null && data.bar2Num.Enabled();
		}

	}

}
