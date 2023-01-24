using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Gauss_Seidel
{
    /*
     * Dispatcher.Run(()=>{tu wklej kod dodający coś do widoku});
     */
    public partial class Form1 : Form
    {
        public static Semaphore threadsSemaphore;
        public static List<List<double>> results = new List<List<double>>();
        public Form1()
        {
            InitializeComponent();
            iterationsBar.ValueChanged += iterationsBar_ValueChanged;
        }
        private List<List<List<double>>> readFromFile (string filename)
        {
            List<List<List<double>>> readSystems = new List<List<List<double>>>();
            List<List<double>> systemOfEquations = new List<List<double>>();
            using (StreamReader sr = new StreamReader(filename))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line == "###")
                    {
                        readSystems.Add(systemOfEquations);
                        systemOfEquations = new List<List<double>>();
                        
                        //systemOfEquations.Clear();
                    }
                    else
                    {
                        string[] components = Regex.Matches(line, @"(-?\d*\,?\d+)(?=x|$)")
                                                    .Cast<Match>()
                                                    .Select(m => m.Value)
                                                    .ToArray();

                        List<double> equation = new List<double>();
                        foreach (string component in components)
                        {
                            double value = double.Parse(component);
                            equation.Add(value);
                        }
                        systemOfEquations.Add(equation);
                    }
                }
            }
            
            return readSystems;
        }
        private DataTable convertResults(List<List<double>> results)
        {
            var dt = new DataTable();
            int rowCount = results.Count;
            int colCount = results[0].Count;
            for (int i = 0; i < colCount; i++)
            {
                dt.Columns.Add();
            }
            for (int i = 0; i < rowCount; i++)
            {
                var row = dt.NewRow();
                for (int j = 0; j < colCount; j++)
                {
                    row[j] = results[i][j];
                }
                dt.Rows.Add(row);
            }
            for (int i = 0; i < colCount; i++)
            {
                dt.Columns[i].ColumnName = "x" + (i + 1).ToString();
            }
            return dt;
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            List<List<List<double>>> systems = new List<List<List<double>>>();
            string filePath = "equations.txt"; //TEST
            systems = readFromFile(filePath);
            int amountOfThreads = 2;
            //int amountOfThreads = trackBar1.Value;
            
            Semaphore tempSemaphore = new Semaphore(amountOfThreads, amountOfThreads);
            threadsSemaphore= tempSemaphore;
            int maxIterations = 10; // maksymalna liczba iteracji
            double tolerance = 0.0001; // tolerancja
            Stopwatch sw = Stopwatch.StartNew();
            foreach (List<List<double>> system in systems)
            {
                threadsSemaphore.WaitOne();
                Thread thread = new Thread(() => Solve(system, maxIterations, tolerance));
                thread.Start();
            }
            sw.Stop();
            timeElapsedBox.Text = sw.ElapsedMilliseconds.ToString();
            resultGridView.DataSource = convertResults(results);
        }
        public static void SolveInAsm(List<List<double>> equations, int maxIterations, double tolerance)
        {
            
            int n = equations.Count; // liczba nieznanych
            List<double> x = new List<double>(); // lista nieznanych
            for (int i = 0; i < n; i++) x.Add(0); // ustawienie początkowych wartości nieznanych
            bool done;
            for(int j = 0; j < n; j++)
            {
                double temp = x[j]; // zapamiętanie poprzedniej wartości nieznanej
                double sum = equations[j][n]; // suma wyrazów wolnych
            }
        }
        public static void ASMSolving()
        {
            
        }
        public static void Solve(List<List<double>> equations, int maxIterations, double tolerance)
        {
            int n = equations.Count; // liczba nieznanych
            List<double> x = new List<double>(); // lista nieznanych
            for (int i = 0; i < n; i++) x.Add(0); // ustawienie początkowych wartości nieznanych
            for (int i = 0; i < maxIterations; i++)
            {
                bool done = true;
                for (int j = 0; j < n; j++)
                {
                    double temp = x[j]; // zapamiętanie poprzedniej wartości nieznanej
                    double sum = equations[j][n]; // suma wyrazów wolnych
                    for (int k = 0; k < n; k++)
                    {
                        if (k != j) sum -= equations[j][k] * x[k];
                    }
                    x[j] = sum / equations[j][j]; // obliczanie nowej wartości nieznanej
                    if (Math.Abs(x[j] - temp) > tolerance) done = false; // sprawdzanie czy wartość nieznanej się zmieniła
                }
                if (done) break; // jeśli wartości nieznanych nie uległy zmianie, to przerwij pętlę
            }
            results.Add(x);
            threadsSemaphore.Release();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void x3_coefficient_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void x2_coefficient_TextChanged(object sender, EventArgs e)
        {

        }

        private void x4_coefficient_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label101_Click(object sender, EventArgs e)
        {

        }

        private void label104_Click(object sender, EventArgs e)
        {

        }

        private void iterationsBar_Scroll(object sender, EventArgs e)
        {

        }
        private void iterationsBar_ValueChanged(object sender, EventArgs e)
        {
            label105.Text = iterationsBar.Value.ToString();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
