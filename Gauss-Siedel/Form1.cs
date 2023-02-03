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
        private static string filePath = "bigDataSet.txt";
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
            int colCount = results.Max(x => x.Count);
            for (int i = 0; i < colCount; i++)
                dt.Columns.Add();
            foreach (List<float> systemResult in results)
            {
                var row = dt.NewRow();
                int varCounter = 0;
                foreach (float varResult in systemResult)
                {
                    row[varCounter] = varResult;
                    varCounter++;
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
            //string filePath = "equations.txt"; //TEST
            systems = readFromFile(filePath);
            Semaphore tempSemaphore = new Semaphore(amountOfThreads, amountOfThreads);
            threadsSemaphore= tempSemaphore;
            progressBar1.Maximum = systems.Count;
            progressBar1.Minimum = 0; progressBar1.Value = 0;
            amountOfThreads = trackBar1.Value;
            maxIterations = iterationsBar.Value;
            toleranceValue = 1/Convert.ToSingle(trackBar2.Value);
            float tempTol = toleranceValue;
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
                    threadsSemaphore.WaitOne();
                    List<float> systemIn1D = new List<float>();
                    for (int i = 0; i < system.Count; i++)
                        for (int j = 0; j < system[i].Count; j++)
                            systemIn1D.Add(system[i][j]);
                    float[] eqArray = systemIn1D.Select(a=>a).ToArray();
                    Thread thread = new Thread(() => SolveInAsm(eqArray,system.Count(), maxIterations));
                    thread.Start();
                    progressBar1.Value++;
                }
            }
            sw.Stop();
            timeElapsedBox.Text = sw.ElapsedMilliseconds.ToString();
            resultGridView.DataSource = convertResults(results);
            results.Clear();
        }
        public unsafe static void SolveInAsm(float[] equations, int n, int maxIterations)
        {
            List<float> x = new List<float>(); // lista nieznanych
            for (int i = 0; i < n; i++)
                    x.Add(0); // ustawienie początkowych wartości nieznanych
                
            float[] xArray = x.Select(a => a).ToArray();
            MyProc1(n, maxIterations, equations, xArray, toleranceValue);
            for (int i=0;i<n;i++)
                x[i]=xArray[i];
            results.Add(x);
            threadsSemaphore.Release();
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
                    }
                    x[j] = sum / equations[j][j]; // obliczanie nowej wartości nieznanej
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

        private void helpButton_Click(object sender, EventArgs e)
        {
            Form helpForm = new Form();
            helpForm.Text = "Help";
            helpForm.Size = new Size(300, 200);
            helpForm.StartPosition = FormStartPosition.CenterScreen;

            Label helpText = new Label();
            helpText.Text = "Tolerance: Value that determines whether execution of algorithm should end sooner depending on difference between last calculated result and recent one.\n If the difference is smaller than tolerance value, algorithm ends earlier.\n\n Max iterations: Maximum amount of iterations, that algorithm will do in order to calculate results.";

            helpText.Location = new Point(10, 10);
            helpText.Size = new Size(280, 180);
            helpForm.Controls.Add(helpText);

            helpForm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
                // Do something with the file here (e.g. read its contents)
            }
        }
    }
}
