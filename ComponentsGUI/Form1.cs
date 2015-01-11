using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms.DataVisualization.Charting;
namespace ComponentsGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ConfigureCharts();
            try
            {
                new Thread(new ParameterizedThreadStart(Naive)).Start(new ThreadParameter(Task.Square, new Action<int, long, string, int>(this.AddDataToChart)));
                new Thread(new ParameterizedThreadStart(Naive)).Start(new ThreadParameter(Task.Linear, new Action<int, long, string, int>(this.AddDataToChart)));
                new Thread(new ParameterizedThreadStart(Naive)).Start(new ThreadParameter(Task.Log, new Action<int, long, string, int>(this.AddDataToChart)));

                new Thread(new ParameterizedThreadStart(Ram)).Start(new ThreadParameter(Task.Square, new Action<int, long, string, int>(this.AddDataToChart)));
                new Thread(new ParameterizedThreadStart(Ram)).Start(new ThreadParameter(Task.Linear, new Action<int, long, string, int>(this.AddDataToChart)));
                new Thread(new ParameterizedThreadStart(Ram)).Start(new ThreadParameter(Task.Log, new Action<int, long, string, int>(this.AddDataToChart)));

                new Thread(new ParameterizedThreadStart(Component)).Start(new Action<int, long, string, int>(this.AddDataToChart));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

        }

        public void AddDataToChart(int x, long y, string series, int n)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<int, long, string, int>(this.AddDataToChart), new object[] { x, y, series, n });
            }
            else
            {
                switch (n)
                {
                    case 1:
                        chart1.Series[series].Points.AddXY(x, y);
                        break;
                    case 2:
                        chart2.Series[series].Points.AddXY(x, y);
                        break;
                    case 3:
                        chart3.Series[series].Points.AddXY(x, y);
                        break;
                    case 4:
                        chart4.Series[series].Points.AddXY(x, y);
                        break;
                }
            }
        }

        static private void Naive(Object obj)
        {
            Stopwatch timer;
            long elapsed;
            Graph x = new Graph(10, 10);

            ThreadParameter param = (ThreadParameter)obj;

            if (param.task == Task.Square)
            {
                for (int n = 1; n < 10000; n += 10)
                {

                    timer = System.Diagnostics.Stopwatch.StartNew();
                    x.FindComponents_Naive();
                    timer.Stop();
                    elapsed = timer.ElapsedMilliseconds;
                    param.action(n, elapsed, "Naive", 1);

                    x.AddVertex(10);
                    x.AddEdges(10);
                }
            }
            else if (param.task == Task.Linear)
            {
                for (int n = 1; n < 10000; n += 10)
                {

                    timer = System.Diagnostics.Stopwatch.StartNew();
                    x.FindComponents_Naive();
                    timer.Stop();
                    elapsed = timer.ElapsedMilliseconds;
                    param.action(n, elapsed, "Naive", 2);

                    x.AddVertex(10);
                    x.AddEdges(9);
                }
            }
            else
            {
                for (int n = 1; n < 20000; n += 10)
                {

                    timer = System.Diagnostics.Stopwatch.StartNew();
                    x.FindComponents_Naive();
                    timer.Stop();
                    elapsed = timer.ElapsedMilliseconds;
                    param.action(n, elapsed, "Naive", 3);

                    x.AddVertex(10);
                    x.AddEdges(3);
                }
            }
        }

        static private void Ram(Object obj)
        {
            Stopwatch timer;
            long elapsed;
            Graph x = new Graph(10, 10);

            ThreadParameter param = (ThreadParameter)obj;

            if (param.task == Task.Square)
            {
                for (int n = 1; n < 10000; n += 10)
                {
                    timer = System.Diagnostics.Stopwatch.StartNew();
                    x.FindComponents_Ram();
                    timer.Stop();
                    elapsed = timer.ElapsedMilliseconds;
                    param.action(n, elapsed, "Ram", 1);

                    x.AddVertex(10);
                    x.AddEdges(10);
                }
            }
            else if (param.task == Task.Linear)
            {
                for (int n = 1; n < 10000; n += 10)
                {
                    timer = System.Diagnostics.Stopwatch.StartNew();
                    x.FindComponents_Ram();
                    timer.Stop();
                    elapsed = timer.ElapsedMilliseconds;
                    param.action(n, elapsed, "Ram", 2);

                    x.AddVertex(10);
                    x.AddEdges(9);
                }
            }
            else
            {
                for (int n = 1; n < 20000; n += 10)
                {
                    timer = System.Diagnostics.Stopwatch.StartNew();
                    x.FindComponents_Ram();
                    timer.Stop();
                    elapsed = timer.ElapsedMilliseconds;
                    param.action(n, elapsed, "Ram", 3);

                    x.AddVertex(10);
                    x.AddEdges(3);
                }
            }
        }

        static private void Component(Object obj)
        {
            Graph x = new Graph(10, 10);

            var _delegate = (Action<int, long, string, int>)obj;

            for (int n = 10; n < 1001; n += 10)
            {
                while (!IsOneComponent(x.FindComponents_Ram(), n))
                    x.AddEdges(1);
                _delegate(n, x.GetNumOfEdges(), "Ram", 4);
                x.AddVertex(10);
            }
        }

        private static bool IsOneComponent(int[] component, int n)
        {
            for (int i = 0; i < n; ++i)
            {
                if (component[i] != 0)
                    return false;
            }
            return true;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            this.chart1.Width = this.Width / 2 - 30;
            this.chart2.Width = this.Width / 2 - 30;
            this.chart3.Width = this.Width / 2 - 30;
            this.chart4.Width = this.Width / 2 - 30;

            this.chart1.Height = this.Height / 2 - 30;
            this.chart2.Height = this.Height / 2 - 30;
            this.chart3.Height = this.Height / 2 - 30;
            this.chart4.Height = this.Height / 2 - 30;

            this.chart2.Location = new Point((this.Width / 2) + 5, chart2.Location.Y);
            this.chart3.Location = new Point(chart3.Location.X, (this.Height / 2) + 5);
            this.chart4.Location = new Point((this.Width / 2) + 5, (this.Height / 2) + 5);
        }

        private void ConfigureCharts()
        {
            chart1.ChartAreas[0].AxisX.Title = "Number of Vertecies";
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Title = "Time, ms";

            chart2.ChartAreas[0].AxisX.Title = "Number of Vertecies";
            chart2.ChartAreas[0].AxisX.Minimum = 0;
            chart2.ChartAreas[0].AxisY.Title = "Time, ms";

            chart3.ChartAreas[0].AxisX.Title = "Number of Vertecies";
            chart3.ChartAreas[0].AxisX.Minimum = 0;
            chart3.ChartAreas[0].AxisY.Title = "Time, ms";

            chart4.ChartAreas[0].AxisX.Title = "Number of Vertecies";
            chart4.ChartAreas[0].AxisX.Minimum = 0;
            chart4.ChartAreas[0].AxisY.Title = "Number of Edges";
        }
    }
}
