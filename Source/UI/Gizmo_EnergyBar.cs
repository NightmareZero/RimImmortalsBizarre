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
		public Pawn pawn;
		public IEnergyHediff eh;
		public EnergyBarOpt guiOpt;

		private Texture2D EmptyBarTex = SolidColorMaterials.NewSolidColorTexture(Color.clear);
		private Texture2D FirstBarTex = SolidColorMaterials.NewSolidColorTexture(new Color(0.7f, 1.0f, 0.9f));
		private Texture2D SecondBarTex = SolidColorMaterials.NewSolidColorTexture(new Color(0.3f, 0.4f, 0.4f));

		private static readonly Texture2D High = ContentFinder<Texture2D>.Get("UI/Icons/High");
		private static readonly Texture2D Middle = ContentFinder<Texture2D>.Get("UI/Icons/Middle");
		private static readonly Texture2D Lower = ContentFinder<Texture2D>.Get("UI/Icons/Lower");
		public override bool Visible
		{
			get
			{
				if (pawn == null || eh == null)
					return false;
				return true;
			}
		}
		public override float GetWidth(float maxWidth)
		{
			return 180f;
		}

		public Gizmo_EnergyBar(Pawn pawn, IEnergyHediff energy)
		{
			Order = -110f;
			this.pawn = pawn;
			this.eh = energy;
			this.guiOpt = eh.opt;

			// 根据opt配置
			this.configOpt();
		}

		private void configOpt()
		{
			this.FirstBarTex = SolidColorMaterials.NewSolidColorTexture(guiOpt.FirstColor);
			this.SecondBarTex = SolidColorMaterials.NewSolidColorTexture(guiOpt.SecondColor);
		}

		public override GizmoResult GizmoOnGUI(Vector2 topLeft, float maxWidth, GizmoRenderParms parms)
		{
			Rect rect = new Rect(topLeft.x, topLeft.y, GetWidth(maxWidth), 75f);
			Rect rect2 = rect.ContractedBy(2f);
			Widgets.DrawWindowBackground(rect);

			// 画第一栏
			Rect rect3 = rect2;
			rect3.width = 122f;
			rect3.height = rect.height / 2f;
			DrawBar(rect3, this.eh.bar1Num);

			// 画第二栏
			Rect rect4 = rect2;
			rect4.width = 122f;
			rect4.yMin = rect2.y + rect2.height / 2f;
			DrawBar(rect4, this.eh.bar2Num);

			// 画图标
			Rect rect5 = new Rect(rect2.x + 125, rect2.y + 8, 50, 60);
			GUI.DrawTexture(rect5, eh.icon);
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
			Widgets.Label(rect2, n.current.ToString("F0") + " / " + n.max.ToString("F0"));
			Widgets.FillableBar(rect3, n.current / n.max, FirstBarTex, EmptyBarTex, doBorder: false);
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
