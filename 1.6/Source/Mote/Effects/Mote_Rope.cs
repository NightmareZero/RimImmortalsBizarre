using Verse;
using UnityEngine;

namespace NzRimImmortalBizarre
{
    public class Mote_Rope : MoteThrown
    {
        public Pawn caster;
        public Pawn target;

        protected override void DrawAt(Vector3 drawLoc, bool flip = false)
        {
            var start = caster.DrawPos;
            var end = target.DrawPos;
            base.DrawAt(drawLoc, flip);

            Material material = new Material(this.Graphic.MatSingle);
            material.color = new Color(0.65f, 0.16f, 0.16f); // 设置红棕色

            if (start != Vector3.zero && end != Vector3.zero)
            {
                // 绘制连接效果
                Vector3 direction = end - start;
                float distance = direction.magnitude;
                direction.Normalize();

                for (float i = 0; i < distance; i += 0.1f)
                {
                    Vector3 position = start + direction * i;
                    Matrix4x4 matrix = Matrix4x4.TRS(position, Quaternion.identity, new Vector3(0.1f, 1f, 0.1f)); // 调整缩放比例
                    Graphics.DrawMesh(MeshPool.plane10, matrix, material, 0);
                }
            }
        }
    }
}