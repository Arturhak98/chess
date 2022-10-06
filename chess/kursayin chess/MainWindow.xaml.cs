using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace kursayin_chess
{
    public partial class MainWindow : Window
    {
        public int[,] cheespoint = { {-50,-33,-30,-90,-1000,-30,-33,-50 },
                          { -10,-10,-10,-10,-10,-10,-10,-10},
                          { 0,0,0,0,0,0,0,0 },
                          { 0,0,0,0,0,0,0,0 },
                          { 0,0,0,0,0,0,0,0 },
                          { 0,0,0,0,0,0,0,0 },
                          { 10,10,10,10,10,10,10,10},
                          { 50,33,30,90,1000,30,33,50 }
            };
        bool FigureCliced;
        Logic Logic;
        Image[] images=new Image[32];
        Image image1;
        double deltax, deltay;
        int startx, starty;
        Init init = new Init();
        public MainWindow()
        {
            InitializeComponent();
            Figure();
            Logic = new Logic(init,cheespoint);

           //  logic = new Logic(init);
            /*for (int i = 0; i < 8; i++)
            {
                tb.Text += "\r\n";
                for (int j = 0; j < 8; j++)
                {
                    tb.Text += Convert.ToString(init.cheespoint[i, j]);
                    tb.Text += " ";
                }
            }*/

        }

      
        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (FigureCliced)
                image1.Margin = new Thickness(e.GetPosition(this).X - deltax, e.GetPosition(this).Y - deltay, 0, 0);
        }

   
        private void Figure()
        {
            for (int i = 0; i < 8; i++)
            {
                Image image = new Image();
                image.Height = 50;
                image.Width = 50;
                string sorce = "b" + Convert.ToString(i + 1) + ".gif";
                image.Source = new BitmapImage(new Uri(sorce, UriKind.Relative));
                image.VerticalAlignment = VerticalAlignment.Top;
                image.HorizontalAlignment = HorizontalAlignment.Left;
                image.Margin = new Thickness(i * 50, 0, 0, 0);
                images[i] = image;
                gr.Children.Add(image);

            }
            for (int i = 0; i < 8; i++)
            {
                Image image;
                image = new Image();
                image.Height = 50;
                image.Width = 50;
                image.Source = new BitmapImage(new Uri("bpeshka.gif", UriKind.Relative));
                image.VerticalAlignment = VerticalAlignment.Top;
                image.HorizontalAlignment = HorizontalAlignment.Left;
                image.Margin = new Thickness(i * 50, 50, 0, 0);
                images[i + 8] = image;
                gr.Children.Add(image);

            }
            for (int i = 0; i < 8; i++)
            {
                Image image;
                image = new Image();
                image.Height = 50;
                image.Width = 50;
                image.Source = new BitmapImage(new Uri("wpeshka.gif", UriKind.Relative));
                image.VerticalAlignment = VerticalAlignment.Top;
                image.HorizontalAlignment = HorizontalAlignment.Left;
                image.Margin = new Thickness(i*50, 300, 0, 0);
                image.MouseDown += Image_MouseDown;
                image.MouseUp += Image_MouseUp;
                images[i + 16] = image;
                gr.Children.Add(image);

            }
            for (int i = 0; i < 8; i++)
            {
                Image image;
                image = new Image();
                image.Height = 50;
                image.Width = 50;
                string sorce = "w" + Convert.ToString( i+1) + ".gif";
                image.Source = new BitmapImage(new Uri(sorce, UriKind.Relative));
                image.VerticalAlignment = VerticalAlignment.Top;
                image.HorizontalAlignment = HorizontalAlignment.Left;
                image.Margin = new Thickness(i*50, 350, 0, 0);
                image.MouseDown += Image_MouseDown;
                image.MouseUp += Image_MouseUp;
                images[i + 24] = image;
                gr.Children.Add(image);

            }
        }

        //public static void IsQueen(int x, int y)
        //{
        //    this.gr.Children.Remove(images[init.ObjectNum[x, y]]);
        //    images[init.ObjectNum[x, y]].Source = new BitmapImage(new Uri("b4.gif", UriKind.Relative));
        //    gr.Children.
        // }



        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            bool b = false, w = false;
            int Number;
            FigureCliced = false;
            if (init.IsFree(((int)image1.Margin.Top + 25) / 50, (int)(image1.Margin.Left + 25) / 50, cheespoint) &&
                init.IsRight(startx, starty, ((int)image1.Margin.Left + 25) / 50, (int)(image1.Margin.Top + 25) / 50, cheespoint))
            {
                image1.Margin = new Thickness((int)(image1.Margin.Left + 25) / 50 * 50, (int)(image1.Margin.Top + 25) / 50 * 50, 0, 0);
                w=init.PointChange(startx, starty, (int)image1.Margin.Left / 50, (int)image1.Margin.Top / 50, cheespoint);
                Number = Logic.BlackFigures(cheespoint);
                if (!init.IsOpponent(Logic.moves1[Number, 1], Logic.moves1[Number, 0], Logic.moves1[Number, 3], Logic.moves1[Number, 2], cheespoint))
                {
                    images[init.ObjectNum[Logic.moves1[Number, 0], Logic.moves1[Number, 1]] - 1].Margin = new Thickness(Logic.moves1[Number, 3] * 50, Logic.moves1[Number, 2] * 50, 0, 0);
                    b = init.PointChange(Logic.moves1[Number, 1], Logic.moves1[Number, 0], Logic.moves1[Number, 3], Logic.moves1[Number, 2], cheespoint);
                }
                else
                {

                    gr.Children.Remove(images[init.ObjectNum[Logic.moves1[Number, 2], Logic.moves1[Number, 3]] - 1]);
                    images[init.ObjectNum[Logic.moves1[Number, 0], Logic.moves1[Number, 1]] - 1].Margin = new Thickness(Logic.moves1[Number, 3] * 50, Logic.moves1[Number, 2] * 50, 0, 0);
                    b = init.PointChange(Logic.moves1[Number, 1], Logic.moves1[Number, 0], Logic.moves1[Number, 3], Logic.moves1[Number, 2], cheespoint);

                }
            }
            else
            if (init.IsRight(startx, starty, ((int)image1.Margin.Left + 25) / 50, (int)(image1.Margin.Top + 25) / 50, cheespoint)
                && init.IsOpponent(startx, starty, (int)(image1.Margin.Left + 25) / 50, (int)(image1.Margin.Top + 25) / 50, cheespoint))
            {

                w = init.PointChange(startx, starty, (int)(image1.Margin.Left + 25) / 50, (int)(image1.Margin.Top + 25) / 50, cheespoint);
                gr.Children.Remove(images[init.Number]);
                image1.Margin = new Thickness((int)(image1.Margin.Left + 25) / 50 * 50, (int)(image1.Margin.Top + 25) / 50 * 50, 0, 0);

                Number = Logic.BlackFigures(cheespoint);
                if (!init.IsOpponent(Logic.moves1[Number, 1], Logic.moves1[Number, 0], Logic.moves1[Number, 3], Logic.moves1[Number, 2], cheespoint))
                {
                    images[init.ObjectNum[Logic.moves1[Number, 0], Logic.moves1[Number, 1]] - 1].Margin = new Thickness(Logic.moves1[Number, 3] * 50, Logic.moves1[Number, 2] * 50, 0, 0);
                    b = init.PointChange(Logic.moves1[Number, 1], Logic.moves1[Number, 0], Logic.moves1[Number, 3], Logic.moves1[Number, 2], cheespoint);
                }
                else
                {



                    gr.Children.Remove(images[init.ObjectNum[Logic.moves1[Number, 2], Logic.moves1[Number, 3]] - 1]);
                    images[init.ObjectNum[Logic.moves1[Number, 0], Logic.moves1[Number, 1]] - 1].Margin = new Thickness(Logic.moves1[Number, 3] * 50, Logic.moves1[Number, 2] * 50, 0, 0);
                    b = init.PointChange(Logic.moves1[Number, 1], Logic.moves1[Number, 0], Logic.moves1[Number, 3], Logic.moves1[Number, 2], cheespoint);

                }

            }
            else
            {
                image1.Margin = new Thickness(startx * 50, starty * 50, 0, 0);
            }

            if (b||w)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (cheespoint[7, i] == -90 && images[init.ObjectNum[i, 7]].Source.ToString() != "b4.gif")//.Source.ToString != "b4.gif")
                    {
                        gr.Children.Remove(images[init.ObjectNum[7, i] - 1]);
                        images[init.ObjectNum[7,i]-1].Source = new BitmapImage(new Uri("b4.gif", UriKind.Relative));
                        gr.Children.Add(images[init.ObjectNum[7,i] - 1]);
                    }
                    if (cheespoint[0, i] == 90 && images[init.ObjectNum[i, 0]].Source.ToString() != "w4.gif")//.Source.ToString != "b4.gif")
                    {
                        gr.Children.Remove(images[init.ObjectNum[0, i] - 1]);
                        images[init.ObjectNum[0, i] - 1].Source = new BitmapImage(new Uri("w4.gif", UriKind.Relative));
                        gr.Children.Add(images[init.ObjectNum[0, i] - 1]);
                    }
                }
            }
            Logic.RatingPosition(cheespoint);
           // tb.Text = " ";
            /* tb.Text = " " + Convert.ToString(logic.moves1[, 0] + 1) + " " + Convert.ToString(logic.moves1[k, 1] + 1) + " " + Convert.ToString(logic.moves1[k, 2] + 1) 
                 + " " + Convert.ToString(logic.moves1[k, 3]+1) + " " + Convert.ToString(logic.moves1[k, 4]) + " " + Convert.ToString(logic.sum(init.cheespoint));*/
            string s="";
            for (int i = 0; i < Logic.count; i++)
            {
                s += "\r\n" + Convert.ToString(i + 1) + " :  ";
               // tb.Text += "\r\n" + Convert.ToString(i + 1) + " :  ";
                for (int j = 0; j < 4; j++)
                {
                    s += Convert.ToString(Logic.moves1[i, j] + 1);
                    s += " ";
                    //tb.Text += Convert.ToString(Logic.moves1[i, j] + 1);
                    //tb.Text += " ";
                }
                s+= "    " + Logic.moves1[i, 4] + "   :   " + Logic.moves1[i, 5];
               // tb.Text += "    " + Logic.moves1[i, 4] + "   :   " + Logic.moves1[i, 5];
            }
            s += "\r\n" + "\r\n";
            s+= Logic.BlackRating;
            //tb.Text += "\r\n" + "\r\n";
            //tb.Text += Logic.BlackRating;
            tb.Text = s;
        }
        //  tb.Text += Logic.WhiteRating + "   :   " + Logic.BlackRating;
        //    for (int i = 0; i < 8; i++)
        //    {
        //        tb.Text += "\r\n";
        //        for (int j = 0; j < 8; j++)
        //        {
        //            tb.Text += Convert.ToString(cheespoint[i, j]);
        //            tb.Text += " ";
        //        }
        //    }
        //    tb.Text += "\r\n" + "\r\n";
        //    for (int i = 0; i < 8; i++)
        //    {
        //        tb.Text += "\r\n";
        //        for (int j = 0; j < 8; j++)
        //        {
        //            tb.Text += Convert.ToString(Logic.cheespoint[i, j]);
        //            tb.Text += " ";
        //        }
        //    }
        //}


        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FigureCliced = true;
            image1 = (Image)e.Source;
            deltax = e.GetPosition(this).X - image1.Margin.Left;
            deltay = e.GetPosition(this).Y - image1.Margin.Top;
            startx = (int)image1.Margin.Left / 50;
            starty = (int)image1.Margin.Top / 50;
        }
    }
}
