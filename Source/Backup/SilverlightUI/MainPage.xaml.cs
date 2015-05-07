using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Media.Imaging;
using System.IO;

namespace SilverlightUI
{
    public partial class MainPage : UserControl
    {
        private PlaneProjection project = new PlaneProjection();
        private DispatcherTimer rotatetimer;
        //private int ts = 50;
        BitmapImage bmp = null;
        private byte[] SourceImageByte;
        public MainPage()
        {
            // Required to initialize variables
            InitializeComponent();
            //rotatetimer=new Threading.Timer(funtimercallback,this,0,0);
            rotatetimer = new DispatcherTimer();
            TimeSpan span = new TimeSpan(0, 0, 0, 0, 1);
            this.rotatetimer.Interval = span;
            //rotatetimer.Interval=new TimeSpan(0,0,0,0,ts);
            rotatetimer.Tick += new System.EventHandler(rotatetimer_Tick);
            //BHeart.MouseEnter += new System.Windows.Input.MouseEventHandler(Bside_MouseEnter);
            //BHeart.MouseLeave += new System.Windows.Input.MouseEventHandler(Bside_MouseLeave);
            Bside.MouseEnter += new System.Windows.Input.MouseEventHandler(Bside_MouseEnter);
            Bside.MouseLeave += new System.Windows.Input.MouseEventHandler(Bside_MouseLeave);
            BHeart.MouseEnter += new MouseEventHandler(ellipse1_MouseEnter);
            //ellipse1.MouseLeave += new MouseEventHandler(ellipse1_MouseLeave);
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
        }

        void ellipse1_MouseLeave(object sender, MouseEventArgs e)
        {
            BHeart.Fill = new SolidColorBrush(Colors.Green);
        }

        void ellipse1_MouseEnter(object sender, MouseEventArgs e)
        {
            //ellipse1.Fill = new SolidColorBrush(Colors.Red);
            rotatetimer.Stop();
        }

        private void Bside_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //rotatetimer.Change(0,ts);
            rotatetimer.Start();
        }

        private void Bside_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //rotatetimer.Change(0,0);
            rotatetimer.Stop();
        }

        private delegate void deldorotate();

        private void setUI()
        {
            //Image img= (Image) BHeart.Children[0];

            //object obj=img.RenderTransform.GetValue(RotateTransform.AngleProperty);
            //mytextbox.Text=BHeart.RenderTransform.GetValue(RotateTransform.AngleProperty).ToString();
            /*
            double dangle=Convert.ToDouble(obj);
            mytextbox.Text=obj.ToString();
            dangle+=10;
            if(dangle>360)
                dangle=dangle%360;
            RotateTransform rt=new RotateTransform();
            rt.Angle=dangle;
            BHeart.RenderTransform=rt;
            */
        }

        private void rotatetimer_Tick(object sender, System.EventArgs e)
        {
            //deldorotate del=new deldorotate(setUI);
            //this.Dispatcher.BeginInvoke(del,null);
            //project.RotationY = (project.RotationY + 5) % 360;
            project.RotationZ = (project.RotationZ + 2) % 360;
            //project.RotationX = (project.RotationX + 5) % 360;
            BHeart.Projection = project;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog fd = new OpenFileDialog();
                fd.Multiselect = false;
                fd.Filter = "jpg 图片 (*.jpg)|*.jpg|png 图片 (*.png)|*.png";
                bool? fdIsShow = fd.ShowDialog();
                if (Convert.ToBoolean(fdIsShow))
                {
                    bmp = new BitmapImage();
                    FileStream fs = fd.File.OpenRead();
                    SourceImageByte = StreamToBytes(fs);
                    bmp.SetSource(fs);
                    fs.Close();

                    ImageBrush berriesBrush = new ImageBrush();
                    berriesBrush.ImageSource = bmp;
                    BHeart.Fill = berriesBrush;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始   
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }
    }
}
