namespace WinFormsApp1
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            label1 = new Label();
            button1 = new Button();
            openFileDialog1 = new OpenFileDialog();
            saveFileDialog1 = new SaveFileDialog();
            dataGridView1 = new DataGridView();
            dataGridView2 = new DataGridView();
            label4 = new Label();
            label6 = new Label();
            label8 = new Label();
            textBoxN = new TextBox();
            textBoxnn = new TextBox();
            textBoxv = new TextBox();
            button2 = new Button();
            label9 = new Label();
            richTextBox1 = new RichTextBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(1298, 934);
            label1.Margin = new Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new Size(185, 32);
            label1.TabIndex = 1;
            label1.Text = "Выберите файл";
            // 
            // button1
            // 
            button1.Location = new Point(1505, 928);
            button1.Margin = new Padding(5, 5, 5, 5);
            button1.Name = "button1";
            button1.Size = new Size(202, 46);
            button1.TabIndex = 2;
            button1.Text = "Найти";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.BackgroundColor = Color.AliceBlue;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(1284, 19);
            dataGridView1.Margin = new Padding(5, 5, 5, 5);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(613, 710);
            dataGridView1.TabIndex = 3;
            dataGridView1.RowPostPaint += dataGridView1_RowPostPaint_1;
            // 
            // dataGridView2
            // 
            dataGridView2.BackgroundColor = Color.AliceBlue;
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(661, 19);
            dataGridView2.Margin = new Padding(5, 5, 5, 5);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.RowHeadersWidth = 51;
            dataGridView2.Size = new Size(613, 710);
            dataGridView2.TabIndex = 4;
            dataGridView2.RowPostPaint += dataGridView2_RowPostPaint;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(692, 800);
            label4.Margin = new Padding(5, 0, 5, 0);
            label4.Name = "label4";
            label4.Size = new Size(244, 32);
            label4.TabIndex = 7;
            label4.Text = "Длина программы N";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(692, 910);
            label6.Margin = new Padding(5, 0, 5, 0);
            label6.Name = "label6";
            label6.Size = new Size(263, 32);
            label6.TabIndex = 9;
            label6.Text = "Словарь программы n";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(692, 858);
            label8.Margin = new Padding(5, 0, 5, 0);
            label8.Name = "label8";
            label8.Size = new Size(247, 32);
            label8.TabIndex = 11;
            label8.Text = "Объем программы V";
            // 
            // textBoxN
            // 
            textBoxN.Location = new Point(1086, 795);
            textBoxN.Margin = new Padding(5, 5, 5, 5);
            textBoxN.Name = "textBoxN";
            textBoxN.Size = new Size(118, 39);
            textBoxN.TabIndex = 14;
            // 
            // textBoxnn
            // 
            textBoxnn.Location = new Point(1086, 906);
            textBoxnn.Margin = new Padding(5, 5, 5, 5);
            textBoxnn.Name = "textBoxnn";
            textBoxnn.Size = new Size(118, 39);
            textBoxnn.TabIndex = 17;
            // 
            // textBoxv
            // 
            textBoxv.Location = new Point(1086, 853);
            textBoxv.Margin = new Padding(5, 5, 5, 5);
            textBoxv.Name = "textBoxv";
            textBoxv.Size = new Size(118, 39);
            textBoxv.TabIndex = 18;
            // 
            // button2
            // 
            button2.BackColor = Color.LightSkyBlue;
            button2.Enabled = false;
            button2.Location = new Point(1298, 800);
            button2.Margin = new Padding(5, 5, 5, 5);
            button2.Name = "button2";
            button2.Size = new Size(385, 104);
            button2.TabIndex = 19;
            button2.Text = "Расcчитать";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(1212, 1022);
            label9.Margin = new Padding(5, 0, 5, 0);
            label9.Name = "label9";
            label9.Size = new Size(0, 32);
            label9.TabIndex = 20;
            // 
            // richTextBox1
            // 
            richTextBox1.BackColor = SystemColors.Control;
            richTextBox1.Location = new Point(20, 19);
            richTextBox1.Margin = new Padding(5, 5, 5, 5);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(630, 1012);
            richTextBox1.TabIndex = 21;
            richTextBox1.Text = "";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Silver;
            ClientSize = new Size(1916, 1053);
            Controls.Add(richTextBox1);
            Controls.Add(label9);
            Controls.Add(button2);
            Controls.Add(textBoxv);
            Controls.Add(textBoxnn);
            Controls.Add(textBoxN);
            Controls.Add(label8);
            Controls.Add(label6);
            Controls.Add(label4);
            Controls.Add(dataGridView2);
            Controls.Add(dataGridView1);
            Controls.Add(button1);
            Controls.Add(label1);
            Margin = new Padding(5, 5, 5, 5);
            Name = "Form1";
            Text = "Lab 1";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button button1;
        private OpenFileDialog openFileDialog1;
        private SaveFileDialog saveFileDialog1;
        private DataGridView dataGridView1;
        private DataGridView dataGridView2;
        private Label label4;
        private Label label6;
        private Label label8;
        private TextBox textBoxN;
        private TextBox textBoxnn;
        private TextBox textBoxv;
        private Button button2;
        private Label label9;
        private RichTextBox richTextBox1;
    }
}