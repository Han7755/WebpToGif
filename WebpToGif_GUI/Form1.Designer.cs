namespace WebpToGif_GUI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ConvertButton = new Button();
            textBox1 = new TextBox();
            openFileDialog1 = new OpenFileDialog();
            fileSelectButton = new Button();
            SuspendLayout();
            // 
            // ConvertButton
            // 
            ConvertButton.Location = new Point(12, 41);
            ConvertButton.Name = "ConvertButton";
            ConvertButton.Size = new Size(322, 23);
            ConvertButton.TabIndex = 0;
            ConvertButton.Text = "ㄱㄱ";
            ConvertButton.UseVisualStyleBackColor = true;
            ConvertButton.Click += ConvertButton_Click;
            // 
            // textBox1
            // 
            textBox1.Enabled = false;
            textBox1.Location = new Point(12, 12);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(290, 23);
            textBox1.TabIndex = 1;
            // 
            // openFileDialog1
            // 
            openFileDialog1.Filter = "webp 파일|*.webp|모든 파일|*.*";
            // 
            // fileSelectButton
            // 
            fileSelectButton.Location = new Point(308, 12);
            fileSelectButton.Name = "fileSelectButton";
            fileSelectButton.Size = new Size(26, 23);
            fileSelectButton.TabIndex = 2;
            fileSelectButton.Text = "...";
            fileSelectButton.UseVisualStyleBackColor = true;
            fileSelectButton.Click += fileSelectButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(346, 74);
            Controls.Add(fileSelectButton);
            Controls.Add(textBox1);
            Controls.Add(ConvertButton);
            Name = "Form1";
            Text = "webp to gif ";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button ConvertButton;
        private TextBox textBox1;
        private OpenFileDialog openFileDialog1;
        private Button fileSelectButton;
    }
}