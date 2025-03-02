using Verse;
using UnityEngine;

namespace NzRimImmortalBizarre
{
    public class Mote_SpinningLine : Mote
    {
        public Pawn caster;
        public float range;
        private float angle = 0;
        public float speed = 1f; // 旋转速度

        public float lineSize = 1f; // 线的粗细

        public override void Tick()
        {
            base.Tick();
            angle += 360f * Time.deltaTime * speed; // 根据speed调整旋转速度
            if (angle >= 360f)
            {
                angle -= 360f;
            }
        }

        protected override void DrawAt(Vector3 drawLoc, bool flip = false)
        {
            Mesh mesh = MeshMakerPlanes.NewPlaneMesh(lineSize);

            var start = caster.DrawPos;
            var end = start + new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)) * range;
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
                    Graphics.DrawMesh(mesh, matrix, material, 0);
                }
            }
        }
    }
}