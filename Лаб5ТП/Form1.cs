using Лаб5ТП.Objects;

namespace Лаб5ТП
{
    public partial class Form1 : Form
    {
        MyRectangle myRect;
        List<BaseObject> objects = new();
        Player player;
        Marker marker;
        GreenCircle greenCircle1;
        GreenCircle greenCircle2;
        public Form1()
        {
            InitializeComponent();
            int countPoints = 0;
            player = new Player(pbMain.Width / 2, pbMain.Height / 2, 0);
            player.OnOverlap += (p, obj) =>
            {
                txtLog.Text = $"[{DateTime.Now:HH:mm:ss:ff}] Игрок пересекся с {obj}\n" + txtLog.Text;
            };
            player.OnMarkerOverlap += (m) =>
            {
                objects.Remove(m);
                marker = null;
            };
            marker = new Marker(pbMain.Width / 2 + 50, pbMain.Height / 2 + 50, 0);
            greenCircle1 = new GreenCircle(new Random().Next(20, pbMain.Width - 20), new Random().Next(20, pbMain.Height - 20), 0);
            greenCircle2 = new GreenCircle(new Random().Next(20, pbMain.Width - 20), new Random().Next(20, pbMain.Height - 20), 0);
            player.OnGreenCircleOverlap += (gr) =>
            {
                changeGrToDefault(gr);
                countPoints++;
                txtPoints.Text = $"Очки: {countPoints}";
            };
            greenCircle1.DecreaseToZero += (gr) =>
            {
                changeGrToDefault(gr);
            };
            greenCircle2.DecreaseToZero += (gr) =>
            {
                changeGrToDefault(gr);
            };
            objects.Add(marker);
            objects.Add(greenCircle1);
            objects.Add(greenCircle2);
            objects.Add(player);
        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Color.White);
            updatePlayer();
            updateGreenCircle(greenCircle1);
            updateGreenCircle(greenCircle2);
            foreach (var obj in objects.ToList())
            {
                if (obj != player && player.Overlaps(obj, g))
                {
                    player.Overlap(obj);
                    obj.Overlap(player);
                }
            }
            foreach (var obj in objects)
            {
                g.Transform = obj.GetTransform();
                obj.Render(g);
            }
        }
        private void updatePlayer()
        {
            if (marker != null)
            {
                float dx = marker.X - player.X;
                float dy = marker.Y - player.Y;
                float length = MathF.Sqrt(dx * dx + dy * dy);
                dx /= length;
                dy /= length;
                player.vX += dx * 0.5f;
                player.vY += dy * 0.5f;
                player.Angle = 90 - MathF.Atan2(player.vX, player.vY) * 180 / MathF.PI;
            }
            player.vX += -player.vX * 0.1f;
            player.vY += -player.vY * 0.1f;
            player.X += player.vX;
            player.Y += player.vY;
        }
        private void updateGreenCircle(GreenCircle greenCircle)
        {
            if (greenCircle.diametr != 0)
            {
                greenCircle.diametr -= (float) 0.25;
            }
        }
        private void changeGrToDefault(GreenCircle greenCircle)
        {
            greenCircle.X = new Random().Next(30, pbMain.Width - 30);
            greenCircle.Y = new Random().Next(30, pbMain.Height - 30);
            greenCircle.diametr = 50;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            pbMain.Invalidate();
        }

        private void pbMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (marker == null)
            {
                marker = new Marker(0, 0, 0);
                objects.Add(marker);
            }
            marker.X = e.X;
            marker.Y = e.Y;
        }
    }
}