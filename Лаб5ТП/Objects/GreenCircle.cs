using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаб5ТП.Objects
{
    internal class GreenCircle : BaseObject
    {
        public float diametr;
        public Action<GreenCircle> DecreaseToZero;
        public GreenCircle(float x, float y, float angle) : base(x, y, angle) {
            diametr = 50;
        }
        public override void Render(Graphics g)
        {
            if (diametr == 0)
            {
                DecreaseToZero(this);
            }
            g.FillEllipse(new SolidBrush(Color.LightGreen), -diametr/2, -diametr / 2, diametr, diametr);
        }
        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-diametr / 2, -diametr / 2, diametr, diametr);
            return path;
        }
    }
}
