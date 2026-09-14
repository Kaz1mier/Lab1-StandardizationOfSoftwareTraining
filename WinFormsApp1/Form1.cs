using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Parser parser = new Parser();

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Выберите файл";
            openFileDialog.Filter = "Go Files (*.go)|*.go|Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                FileReadResult result = parser.ReadCode(filePath);
                if (result.ErrorCode == 0)
                {
                    button2.Enabled = true;
                    richTextBox1.Text = parser.Code;
                }
                else
                {
                    button2.Enabled = false;
                }
                MessageBox.Show(result.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView2.Columns.Clear();

            dataGridView2.RowHeadersVisible = true;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView1.AllowUserToAddRows = false;

            dataGridView2.Columns.Add("Column1", "Операторы");
            dataGridView2.Columns.Add("Column2", "F1j");
            dataGridView1.Columns.Add("Column1", "Операнды");
            dataGridView1.Columns.Add("Column2", "F2i");

            Dictionary<string, int> operators = parser.CountOperators();
            Dictionary<string, int> operands = parser.CountOperands();

            foreach (var item in operators)
            {
                dataGridView2.Rows.Add(item.Key, item.Value);
            }

            int sumIdx1 = dataGridView2.Rows.Add();
            parser.CalculateHalsteadMetrics(operators, operands);
            dataGridView2.Rows[sumIdx1].Cells[0].Value = "";
            dataGridView2.Rows[sumIdx1].Cells[1].Value = $"N₁ = {parser.N1}";
            dataGridView2.RowHeadersWidth = 100;
            dataGridView2.Rows[sumIdx1].HeaderCell.Value = $"η₁ = {parser.n1}";

            foreach (var item in operands)
            {
                dataGridView1.Rows.Add(item.Key, item.Value);
            }

            int sumIdx2 = dataGridView1.Rows.Add();
            dataGridView1.Rows[sumIdx2].Cells[0].Value = "";
            dataGridView1.Rows[sumIdx2].Cells[1].Value = $"N₂ = {parser.N2}";
            dataGridView1.RowHeadersWidth = 100;
            dataGridView1.Rows[sumIdx2].HeaderCell.Value = $"η₂ = {parser.n2}";

            textBoxN.Text = parser.N.ToString();
            textBoxnn.Text = parser.n.ToString();
            textBoxv.Text = parser.V.ToString("F2");
        }

        private void dataGridView1_RowPostPaint_1(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = (DataGridView)sender;

            int last = dgv.AllowUserToAddRows
                ? dgv.Rows.Count - 2
                : dgv.Rows.Count - 1;

            if (e.RowIndex == last)
                return;

            string rowIndex = (e.RowIndex + 1).ToString();
            using (var sf = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            })
            {
                var headerBounds = new Rectangle(
                    e.RowBounds.Left,
                    e.RowBounds.Top,
                    dgv.RowHeadersWidth,
                    e.RowBounds.Height);

                e.Graphics.DrawString(
                    rowIndex,
                    dgv.Font,
                    Brushes.Black,
                    headerBounds,
                    sf);
            }
        }

        private void dataGridView2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = (DataGridView)sender;

            int last = dgv.AllowUserToAddRows
                ? dgv.Rows.Count - 2
                : dgv.Rows.Count - 1;

            if (e.RowIndex == last)
                return;

            string rowIndex = (e.RowIndex + 1).ToString();
            using (var sf = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            })
            {
                var headerBounds = new Rectangle(
                    e.RowBounds.Left,
                    e.RowBounds.Top,
                    dgv.RowHeadersWidth,
                    e.RowBounds.Height);

                e.Graphics.DrawString(
                    rowIndex,
                    dgv.Font,
                    Brushes.Black,
                    headerBounds,
                    sf);
            }
        }
    }
}