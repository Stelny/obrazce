using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace obrazce
{
    /// <summary>
    /// Interakční logika pro ctverec.xaml
    /// </summary>
    public partial class ctverec : Page
    {

        private string tvar = "ctverec";
        private string strany_text = "";
        private float a;
        private float b;
        private float c;
        private float S;
        private float O;
        private float polomer_s;

        public ctverec(string tvar)
        {
            InitializeComponent();


            this.tvar = tvar;

            resetIt();
        }


        public void resetIt()
        {
            polomer_div.Visibility = Visibility.Collapsed;
            strana_c_div.Visibility = Visibility.Collapsed;
            strana_b_div.Visibility = Visibility.Collapsed;
            strana_a_div.Visibility = Visibility.Collapsed;

            switch (this.tvar)
            {
                case "ctverec":

                    createSquare();

                    break;

                case "kruh":

                    createCircle();

                    break;

                case "trojuhelnik":

                    createTriangle();

                    break;

                case "obdelnik":

                    createRectangle();

                    break;


            }

            obsah.Text = "S = " + this.S;
            obvod.Text = "O = " + this.O;
            strany.Text = this.strany_text;
        }

        private void createSquare()
        {
            strana_a_div.Visibility = Visibility.Visible;

            Ctverec ctverec = new Ctverec("yellow", 2, false,this.a);

            this.O = ctverec.Vrat_Obvod();
            this.S = ctverec.Vrat_Obsah();

            Rectangle draw_rectangle = new Rectangle();

            draw_rectangle.Width = this.a;
            draw_rectangle.Height = this.a;
            draw_rectangle.Fill = new SolidColorBrush(Colors.White);
            draw_rectangle.Stroke = new SolidColorBrush(Colors.Black);

            Canvas.SetLeft(draw_rectangle, draw_canvas.Width/2 - this.a/2);
            Canvas.SetTop(draw_rectangle, draw_canvas.Height/2 - this.a/2);


            draw_canvas.Children.Clear();
            draw_canvas.Children.Add(draw_rectangle);
            this.strany_text = "A: " + this.a;
        }


        private void createCircle()
        {
            polomer_div.Visibility = Visibility.Visible;

            Kruh kruh = new Kruh("yellow", 2, false, this.polomer_s);
            
            this.O = kruh.Vrat_Obvod();
            this.S = kruh.Vrat_Obsah();

            Ellipse draw_kruh = new Ellipse();

            draw_kruh.Width = 2 * this.polomer_s;
            draw_kruh.Height = 2 * this.polomer_s;

            draw_kruh.Fill = new SolidColorBrush(Colors.White);
            draw_kruh.Stroke = new SolidColorBrush(Colors.Black);

            Canvas.SetLeft(draw_kruh, draw_canvas.Width/2 - this.polomer_s / 2);
            Canvas.SetTop(draw_kruh, draw_canvas.Height/2 - this.polomer_s / 2);


            draw_canvas.Children.Clear();
            draw_canvas.Children.Add(draw_kruh);
            this.strany_text = "r: " + this.polomer_s;



        }

        private void createRectangle()
        {
            strana_b_div.Visibility = Visibility.Visible;
            strana_a_div.Visibility = Visibility.Visible;

            Obdelnik obdelnik = new Obdelnik("yellow", 2, false, this.a, this.b);

            this.O = obdelnik.Vrat_Obvod();
            this.S = obdelnik.Vrat_Obsah();

            Rectangle draw_rectangle = new Rectangle();

            draw_rectangle.Width = this.a;
            draw_rectangle.Height = this.b;
            draw_rectangle.Fill = new SolidColorBrush(Colors.White);
            draw_rectangle.Stroke = new SolidColorBrush(Colors.Black);

            Console.WriteLine(this.a);

            Canvas.SetLeft(draw_rectangle, draw_canvas.Width/2 - this.a / 2);
            Canvas.SetTop(draw_rectangle, draw_canvas.Height/2 - this.b / 2);

         

            draw_canvas.Children.Clear();
            draw_canvas.Children.Add(draw_rectangle);
            this.strany_text = "A: "+this.a + ", B: " + this.b;


        }


        private void createTriangle()
        {
            strana_a_div.Visibility = Visibility.Visible;
            strana_b_div.Visibility = Visibility.Visible;
            strana_c_div.Visibility = Visibility.Visible;
            
            if (this.a + this.b < this.c)
            {
                draw_canvas.Children.Clear();
                return;   
            }
            if (this.b + this.c < this.a)
            {
                draw_canvas.Children.Clear();
                return;
            }
            if (this.a + this.c < this.b)
            {
                draw_canvas.Children.Clear();
                return;
            }

            if (this.a > 0 && this.b > 0 && this.c > 0)
            {
                double a_a = (Math.Pow(this.b, 2) + Math.Pow(this.c, 2) - Math.Pow(this.a, 2)) / (2 * this.b * this.c) * 2;

                float vc = (float)Math.Sin(a_a) * this.a;


                float w = GetRatio_(this.a, this.b, true); //a
                float x = GetRatio_(this.a, this.b, false); //b1
                float y = GetRatio_(this.b, this.c, true); //b2
                float z = GetRatio_(this.b, this.c, false); //c

                float AA = solveProportion(w, x, y, z, "A");
                float BB = solveProportion(w, x, y, z, "B");
                float CC = solveProportion(w, x, y, z, "C");

                float x_nahore = 0;
                float x_navic = 0;
                if (CC >= BB)
                {
                    float jedna_cast = this.a / AA;
                    x_nahore = this.a / 2 + ((CC - BB) * jedna_cast);
                }
                else if (BB >= CC)
                {
                    float jedna_cast = this.a / AA;
                    x_nahore = this.a / 2 - ((BB - CC) * jedna_cast);
                    if (x_nahore < 0)
                    {
                        x_navic = x_nahore * -1;
                        x_nahore += x_navic;
                    }
                }

                Trojuhelnik trojuhelnik = new Trojuhelnik("yellow", 2, false, this.a, this.b, this.c, vc);

                this.O = trojuhelnik.Vrat_Obvod();
                this.S = trojuhelnik.Vrat_Obsah();

                Polygon trojuhelnik_draw = new Polygon();

                trojuhelnik_draw.Stroke = new SolidColorBrush(Colors.Black);
                trojuhelnik_draw.Fill = new SolidColorBrush(Colors.White);

                Point Point1 = new System.Windows.Point(x_nahore + draw_canvas.Width / 2 - this.a / 2, draw_canvas.Height / 2 - vc / 2);
                Point Point2 = new System.Windows.Point(0 + x_navic + draw_canvas.Width / 2 - this.a / 2, draw_canvas.Height / 2 + vc / 2);
                Point Point3 = new System.Windows.Point(0 + this.a + x_navic + draw_canvas.Width / 2 - this.a / 2, draw_canvas.Height / 2 + vc / 2);
                PointCollection myPointCollection = new PointCollection();
                myPointCollection.Add(Point1);
                myPointCollection.Add(Point2);
                myPointCollection.Add(Point3);
                trojuhelnik_draw.Points = myPointCollection;


                draw_canvas.Children.Clear();
                draw_canvas.Children.Add(trojuhelnik_draw);
            } else
            {
                draw_canvas.Children.Clear();
            }

            
          
            

        }

        private void strana_a_changed(object sender, TextChangedEventArgs e)
        {
            

            if (!String.IsNullOrEmpty(strana_a.Text))
            {
                var num = float.Parse(strana_a.Text);

                if (num > 200)
                {
                    this.a = 200;
                    strana_a.Text = "200";
                    
                } else
                {
                    this.a = float.Parse(strana_a.Text);
                }
                
            } else
            {
                this.a = 0;
                
            }

            resetIt();
        }

        private void strana_b_changed(object sender, TextChangedEventArgs e)
        {


            if (!String.IsNullOrEmpty(strana_b.Text))
            {
                var num = float.Parse(strana_b.Text);

                if (num > 200)
                {
                    this.b = 200;
                    strana_b.Text = "200";

                }
                else
                {
                    this.b = float.Parse(strana_b.Text);
                }

            }
            else
            {
                this.b = 0;

            }

            resetIt();
        }


        private void strana_c_changed(object sender, TextChangedEventArgs e)
        {


            if (!String.IsNullOrEmpty(strana_c.Text))
            {
                var num = float.Parse(strana_c.Text);

                if (num > 200)
                {
                    this.c = 200;
                    strana_c.Text = "200";
                }
                else
                {
                    this.c = float.Parse(strana_c.Text);
                }

            }
            else
            {
                this.c = 0;

            }

            resetIt();
        }

        private void strana_polomer_changed(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrEmpty(polomer.Text))
            {
                var num = float.Parse(polomer.Text);

                if (num > 100)
                {
                    this.polomer_s = 100;
                    polomer.Text = "100";

                }
                else
                {
                    this.polomer_s = float.Parse(polomer.Text);
                }

            }
            else
            {
                this.polomer_s = 0;

            }

            resetIt();
        }

        public float GetRatio_(float a, float b, bool a_b)
        {
            var gcd_ = GCD_(a, b);
            if (a_b == true)
            {
                return a / gcd_;
            }
            else
            {
                return b / gcd_;
            }
        }
        public float GCD_(float a, float b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                {
                    a %= b;
                }
                else
                {
                    b %= a;
                }
            }
            if (a == 0)
            {
                return b;
            }
            else
            {
                return a;
            }
        }

        public float __gcd(float a, float b)
        {
            return b == 0 ? a : __gcd(b, a % b);
        }

        public float solveProportion(float a, float b1, float b2, float c, string strana)
        {
            float A = a * b2;
            float B = b1 * b2;
            float C = b1 * c;

            // To print the given proportion
            // in simplest form.
            float gcd = __gcd(__gcd(A, B), C);

            if (strana == "B")
            {
                return (B / gcd);
            }
            else if (strana == "C")
            {
                return (C / gcd);
            }
            else if (strana == "A")
            {
                return (A / gcd);
            }
            return A;
        }

    }



}
