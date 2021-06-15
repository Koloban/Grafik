using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;

namespace График
{
    public partial class Form1 : Form
    {
        Graphics graph;
        // Необходимо для работы с прорисовкой в ПикчерБокс
        Pen bluePen = new Pen(Color.Blue, 1);
        // Синий карандаш для прорисовки сетки
        Pen redPen = new Pen(Color.Red, 2);
        // Красный карандаш для прорисовки сетки
        int x = 150;
        int y = 150;
        // Т.к. ПикчерБокс у нас 300х300, Центр координат находится на 150х150
        CancellationTokenSource cts;
        // Понадобиться для отмены

        private void Form1_Load(object sender, EventArgs e)
        {
            graph = pictureBox1.CreateGraphics();
            // Определяем для граф поле прорисовки
            Pryam_grb.Visible = false;
            Krug_grb.Visible = false;
            Pryamoug_grb.Visible = false;
            Eleps_grb.Visible = false;
            // Делаем крупбоксы невидимыми до прорисовки сетки
        }

        public Form1()
        {
          InitializeComponent();
        }

        void setka()
        {
            graph.DrawLine(bluePen, x - 130, y, x + 150, y);
            graph.DrawLine(bluePen, x, y - 150, x, y + 150);
            // Делаем прямые осей координат от начала до конца с центром 150
            for (int i = 30; i < 280; i += 15)
            {
                graph.DrawLine(bluePen, i, y - 5, i, y + 5);
                graph.DrawLine(bluePen, x - 5, i, x + 5, i);
            }
            // Рисуем штришки с шагом в 15(15 у нас будет за 1 единицу на координатной сетке)
            graph.DrawLine(bluePen, x + 150, y, x + 140, y + 5);
            graph.DrawLine(bluePen, x + 150, y, x + 140, y - 5);
            graph.DrawLine(bluePen, x, y - 150, x - 5, y - 140);
            graph.DrawLine(bluePen, x, y - 150, x + 5, y - 140);
        }//Рисуем косые шришки(стрелочки на оси абсцисс и ординат)

        private void button1_Click(object sender, EventArgs e)
        {
            Pryam_grb.Visible = true;
            Krug_grb.Visible = true;
            Pryamoug_grb.Visible = true;
            Eleps_grb.Visible = true;
            Start_b.Visible = false;
            setka(); // При нажатии на кнопку "отрисовка сетки" прорисовывем сетку и делаем крупбоксы видимыми
        }

        private void Pryam_but_Click(object sender, EventArgs e)
        {
            int x1=0, x2=0, y1=0, y2=0;
            // Пишем что трайкетч нужен для отлавливания и корректно введенных данных в поля
            try
            {
                x1 = int.Parse(t_pryam_x1.Text); x2 = int.Parse(t_pryam_x2.Text);
                y1 = int.Parse(t_pryam_y1.Text) * -1; y2 = int.Parse(t_pryam_y2.Text) * -1;
                //Присваиваем точки начала и конца отрезка из полей в форме переводя их в int
                graph.DrawLine(redPen, x + (x1 * 15), y + (y1 * 15), x + (x2 * 15), y + (y2 * 15));
                //Красным карандашом рисуем отрезок. Поскольку шаг у нас определен как 15 умножаем введенные пользователем данные на 15 и считаем от начала координат которые мы задали глобальными переменными
            }// Алгоритм такой же при отрисовке остальных элементов, поэтому не будем повторяться
            catch
            {
                MessageBox.Show("Ошибка ввода");
                // Указываем пользователю, что введены некорректные данные 
            }
        }

        private void Okrug_but_Click(object sender, EventArgs e)
        //90% совпадает с отрисовкой отрезка
        {
            int x_m = 0, y_m = 0, rad=0;
            try
            {
                x_m = int.Parse(t_okrug_x.Text); y_m = int.Parse(t_okrug_y.Text)*-1;
                rad = int.Parse(t_okrug_rad.Text)*15; if (rad <= 0)  cts.Cancel();
                graph.DrawEllipse(redPen, x + (x_m * 15 - rad / 2), y + (y_m * 15 - rad / 2), rad, rad);
                //Чтобы получить окружность рисуем эллипс с одинаковой шириной и высотой определенный радиусом
            }
            catch
            {
                MessageBox.Show("Ошибка ввода");            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        //Тоже самое, что и до этого, просто рисуем прямоугольник
        {
            int x_m = 0, y_m = 0, shir = 0, vis = 0;
            try
            {
                x_m = int.Parse(t_pryam_x.Text); y_m = int.Parse(t_pryam_y.Text)*-1;
                shir = int.Parse(t_pryam_shir.Text) * 15; if (shir <= 0) cts.Cancel();
                vis = int.Parse(t_pryam_vis.Text) * 15; if (vis <= 0) cts.Cancel();
                graph.DrawRectangle(redPen, x + x_m * 15, y + y_m * 15, shir, vis);
            }
            catch
            {
                MessageBox.Show("Ошибка ввода");
            }      
        }

        private void Eleps_but_Click(object sender, EventArgs e)
        //Тоже самое, просто рисуем эллипс 
        {
            int x_m = 0, y_m = 0, shir = 0, vis = 0;
            try
            {
                x_m = int.Parse(t_eleps_x.Text); y_m = int.Parse(t_eleps_y.Text) * -1;
                shir = int.Parse(t_eleps_shir.Text) * 15; if (shir <= 0) cts.Cancel();
                vis = int.Parse(t_eleps_vis.Text) * 15; if (vis <= 0) cts.Cancel();
                graph.DrawEllipse(redPen, x + (x_m * 15 - shir / 2), y + (y_m * 15 - vis / 2), shir, vis);
            }
            catch
            {
                MessageBox.Show("Ошибка ввода");
            }
        }
    }
}