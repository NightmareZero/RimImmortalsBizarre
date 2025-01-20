using Verse;
using UnityEngine;

namespace NzRimImmortalBizarre
{
    public class Mote_Rope : MoteThrown
    {
        public Vector3 start;
        public Vector3 end;

        protected override void DrawAt(Vector3 drawLoc, bool flip = false)
        {
            base.DrawAt(drawLoc, flip);
            if (start != Vector3.zero && end != Vector3.zero)
            {
                // 绘制连接效果
                Vector3 direction = end - start;
                float distance = direction.magnitude;
                direction.Normalize();
                for (float i = 0; i < distance; i += 0.1f)
                {
                    Vector3 position = start + direction * i;
                    Graphics.DrawMesh(MeshPool.plane10, position, Quaternion.identity, this.Graphic.MatSingle, 0);
                }
            }
        }
    }
}