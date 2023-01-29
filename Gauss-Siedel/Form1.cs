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
using System.Runtime.InteropServices;


namespace Gauss_Seidel
{
    /*
     * Dispatcher.Run(()=>{tu wklej kod dodający coś do widoku});
     */
    public partial class Form1 : Form
    {
        [DllImport(@"E:\Osobiste\Projekt Asembler\Gauss-Siedel\x64\Debug\GaussSeidelASM.dll")]
        static unsafe extern void MyProc1(int n, int maxIterations, float[] equations, float[] x, float tolerance);
        public static Semaphore threadsSemaphore;
        public static List<List<float>> results = new List<List<float>>();
        private static int amountOfThreads = 1;
        private static int maxIterations = 10;
        private static bool runInCSharp = true;
        private static float toleranceValue = 0.0001F;
        public Form1()
        {
            InitializeComponent();
            iterationsBar.ValueChanged += iterationsBar_ValueChanged;
        }
        private List<List<List<float>>> readFromFile (string filename)
        {
            List<List<List<float>>> readSystems = new List<List<List<float>>>();
            List<List<float>> systemOfEquations = new List<List<float>>();
            using (StreamReader sr = new StreamReader(filename))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line == "###")
                    {
                        readSystems.Add(systemOfEquations);
                        systemOfEquations = new List<List<float>>();
                        
                        //systemOfEquations.Clear();
                    }
                    else
                    {
                        string[] components = Regex.Matches(line, @"(-?\d*\,?\d+)(?=x|$)")
                                                    .Cast<Match>()
                                                    .Select(m => m.Value)
                                                    .ToArray();

                        List<float> equation = new List<float>();
                        foreach (string component in components)
                        {
                            float value = float.Parse(component);
                            equation.Add(value);
                        }
                        systemOfEquations.Add(equation);
                    }
                }
            }
            
            return readSystems;
        }
        private DataTable convertResults(List<List<float>> results)
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
            List<List<List<float>>> systems = new List<List<List<float>>>();
            string filePath = "equations.txt"; //TEST
            systems = readFromFile(filePath);
            Semaphore tempSemaphore = new Semaphore(amountOfThreads, amountOfThreads);
            threadsSemaphore= tempSemaphore;
            progressBar1.Maximum = systems.Count;
            progressBar1.Minimum = 0; progressBar1.Value = 0;
            Stopwatch sw = Stopwatch.StartNew();
            if (runInCSharp)
            {
                foreach (List<List<float>> system in systems)
                {
                    threadsSemaphore.WaitOne();
                    Thread thread = new Thread(() => Solve(system, maxIterations, toleranceValue));
                    thread.Start();
                    progressBar1.Value++;
                }
            }
            else
            {
                foreach (List<List<float>> system in systems)
                {
                    List<float> systemIn1D = new List<float>();
                    for (int i = 0; i < system.Count; i++)
                        for (int j = 0; j < system[i].Count; j++)
                            systemIn1D.Add(system[i][j]);
                    float[] eqArray = systemIn1D.Select(a=>a).ToArray();

                    //float[][] eqArray = system.Select(a => a.ToArray()).ToArray(); //Zeby dalo sie uzywac w asmie
                    //Thread thread = new Thread(() => SolveInAsm2(eqArray,system.Count(), maxIterations));
                    //thread.Start();
                    int abc = system.Count();
                    SolveInAsm2(eqArray, system.Count(), maxIterations);
                    progressBar1.Value++;
                }
            }
            sw.Stop();
            timeElapsedBox.Text = sw.ElapsedMilliseconds.ToString();
            resultGridView.DataSource = convertResults(results);
        }
        /*public unsafe static void SolveInAsm(float[][] equations, int n,int maxIterations)
        {                      
            List<float> x = new List<float>(); // lista nieznanych
            for (int i = 0; i < n; i++) 
                x.Add(0); // ustawienie początkowych wartości nieznanych
            float[] xArray = x.ToArray();
            for (int i = 0; i < maxIterations; i++)
            {
            bool done = true;
                for (int j=0; j< n; j++)
                {
                    float temp = x[j];
                    float sum = equations[j][n];
                    float* sumPtr = &sum;
                    //docelowo to będzie w asmie
                    for (int k=0; k < n; k++)
                    {
                            MyProc1(sumPtr, equations[j][k], x[k], j, k);
                    }
                    x[j] = sum / equations[j][j];
                    if (Math.Abs(x[j] - temp) > toleranceValue) done = false;
                    //koniec asm
                }

                if (done) break;
                
            }
                results.Add(x);
                threadsSemaphore.Release();
        }*/
        public unsafe static void SolveInAsm2(float[] equations, int n, int maxIterations)
        {
            List<float> x = new List<float>(); // lista nieznanych
            for (int i = 0; i < n; i++)
                    x.Add(0); // ustawienie początkowych wartości nieznanych
                
            float[] xArray = x.Select(a => a).ToArray();
            MyProc1(n, maxIterations, equations, xArray, toleranceValue);

            //MyProc1(int n, int maxIterations, float equations[][], float x[], float x[], float tolerance)
            /*for (int i = 0; i < maxIterations; i++)
            {
                bool done = true;
                for (int j = 0; j < n; j++)
                {
                    float temp = x[j];
                    float sum = equations[j][n];
                    float* sumPtr = &sum;
                    
                    float[] neededEquation = equations[j];
                    for (int k = 0; k < n; k++)
                    {
                        
                        if (k!=j)  //if (k != j) sum -= equations[j][k] * x[k];
                        {
                        operacja:
                            wlozyc eq[j][k] do xmm0 na 0 indeks
                            zinkrementowac licznik (licznik=0)
                            zinkrementowac k
                            jesli k == 3 lub k == n skok do odejmij
                            skok operacja

                        odejmij:
                            przemnoz xmm przez x[k]
                            odejmij od zmiennej suma kazdy element xmm0
                            jesli k < n przejdz spowrotem do operacja
                            dalsza czesc programu
                        }
                    }
                    x[j] = sum / equations[j][j];
                    if (Math.Abs(x[j] - temp) > tolerance) done = false;
                    
                }
            if (done) break;
            }*/
            results.Add(x);
            //threadsSemaphore.Release();
        }
        public static void Solve(List<List<float>> equations, int maxIterations, float tolerance)
        {
            int n = equations.Count; // liczba nieznanych
            List<float> x = new List<float>(); // lista nieznanych
            for (int i = 0; i < n; i++) x.Add(0); // ustawienie początkowych wartości nieznanych
            for (int i = 0; i < maxIterations; i++)
            {
                bool done = true;
                for (int j = 0; j < n; j++)
                {
                    float temp = x[j]; // zapamiętanie poprzedniej wartości nieznanej
                    float sum = equations[j][n]; // suma wyrazów wolnych
                    for (int k = 0; k < n; k++)
                    {
                        if (k != j) sum -= equations[j][k] * x[k];
                        float eqtemp = equations[j][k];
                        float xtemp = x[k];
                    }
                    x[j] = sum / equations[j][j]; // obliczanie nowej wartości nieznanej
                    float xd = x[j];
                    float result = x[j] - temp;
                    if (Math.Abs(x[j] - temp) > tolerance) done = false; // sprawdzanie czy wartość nieznanej się zmieniła
                }
                if (done) break; // jeśli wartości nieznanych nie uległy zmianie, to przerwij pętlę
            }
            results.Add(x);
            threadsSemaphore.Release();
        }
        private void toleranceTrackbar_ValueChanged(object sender, EventArgs e)
        {
            label6.Text = (1/(double)trackBar2.Value).ToString();
            toleranceValue = 1 / Convert.ToSingle(trackBar2.Value);
        }
        private void tolerance_ValueChanged(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Checked = false;
                runInCSharp = true;
            }
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox1.Checked = false;
                runInCSharp = false;
            }
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
            maxIterations = iterationsBar.Value;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }
        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            amountOfThreads = trackBar1.Value;
            label5.Text = trackBar1.Value.ToString();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
