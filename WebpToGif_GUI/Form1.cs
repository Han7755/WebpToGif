using ImageMagick;
using System.ComponentModel;
using System.Numerics;

namespace WebpToGif_GUI
{
    public partial class Form1 : Form
    {
        List<string> webpFilePaths; // ���� ��� ����� ����

        public Form1()
        {
            webpFilePaths = new List<string>();
            InitializeComponent();
        }


        // ���� ���� ��ư Ŭ���� �۵�
        private void fileSelectButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK && openFileDialog1.FileNames.Length != 0)
                {
                    // ���õ� ��� ���� �� �ؽ�Ʈ �ڽ��� ǥ��
                    foreach (var filePath in openFileDialog1.FileNames)
                    {
                        if (!File.Exists(filePath))
                        {
                            MessageBox.Show("������ ã�� �� �����ϴ�: " + filePath, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        webpFilePaths.Add(filePath);
                        textBox1.Text += filePath + "\n";

                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("������ ���� ���� ������ �߻��߽��ϴ�: " + ex.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConvertButton_Click(object sender, EventArgs e)
        {
            if (webpFilePaths.Count == 0)
            {
                MessageBox.Show("Webp ������ �������ּ���.", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ConvertButton.Enabled = false;
            backgroundWorker1.RunWorkerAsync();

        }

        // ���� �巡�� �� ������� ��� ���� ���
        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] filePath = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (var file in filePath)
            {
                if (!string.IsNullOrEmpty(file) && File.Exists(file))
                {
                    textBox1.Text += file + "\n";
                    webpFilePaths.Add(file);
                }
            }
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private async void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            await WorkerJob();
            return;
        }
        private async Task WorkerJob()
        {
            List<Task> tasks = new List<Task>();
            foreach (string webpPath in webpFilePaths.ToArray())
            {
                if (string.IsNullOrEmpty(webpPath))
                    continue;

                // �ߺ��Ǵ� �̸��� ���� ��� �̸� ����
                string directory = Path.GetDirectoryName(webpPath);
                string originalGifFileName = Path.ChangeExtension(Path.GetFileName(webpPath), ".gif");
                string gifFilePath = Path.Combine(directory, FileNameHelper.GetUniqueFileName(directory, originalGifFileName));
                Task task = Task.Run(async () =>
                {
                    try
                    {
                        await Task.Run(() => Converter.ConvertWebPToGif(webpPath, gifFilePath));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("���� �߻�: " + ex.Message + "\n" + ex.StackTrace, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                });
                tasks.Add(task);
            }

            await Task.WhenAll(tasks);
            if (ConvertButton.InvokeRequired)
            {
                ConvertButton.Invoke(new Action(() =>
                {
                    ConvertButton.Enabled = true;
                    webpFilePaths.Clear();
                    textBox1.Text = string.Empty;
                }));
            }
            else
            {
                ConvertButton.Enabled = true;
                webpFilePaths.Clear();
                textBox1.Text = string.Empty;
            }
            MessageBox.Show("Done!", "Done", MessageBoxButtons.OK);
            return;
        }
    }
}