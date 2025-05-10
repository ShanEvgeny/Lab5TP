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
        public GreenCircle(float x, float y, float angle) : base(x, y, angle) { }
        public override void Render(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.LightGreen), -15, -15, 30, 30);
        }
        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-15, -15, 30, 30);
            return path;
        }
        
    }
}
