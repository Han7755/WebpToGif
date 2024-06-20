using ImageMagick;
using System.ComponentModel;
using System.Numerics;

namespace WebpToGif_GUI
{
    public partial class Form1 : Form
    {
        List<string> webpFilePaths; // 파일 경로 저장용 변수

        public Form1()
        {
            webpFilePaths = new List<string>();
            InitializeComponent();
        }


        // 파일 선택 버튼 클릭시 작동
        private void fileSelectButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK && openFileDialog1.FileNames.Length != 0)
                {
                    // 선택된 경로 저장 및 텍스트 박스에 표시
                    foreach (var filePath in openFileDialog1.FileNames)
                    {
                        if (!File.Exists(filePath))
                        {
                            MessageBox.Show("파일을 찾을 수 없습니다: " + filePath, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        webpFilePaths.Add(filePath);
                        textBox1.Text += filePath + "\n";

                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("파일을 여는 도중 에러가 발생했습니다: " + ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConvertButton_Click(object sender, EventArgs e)
        {
            if (webpFilePaths.Count == 0)
            {
                MessageBox.Show("Webp 파일을 선택해주세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ConvertButton.Enabled = false;
            backgroundWorker1.RunWorkerAsync();

        }

        // 파일 드래그 앤 드롭으로 경로 전달 기능
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

                // 중복되는 이름이 있을 경우 이름 변경
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
                        MessageBox.Show("에러 발생: " + ex.Message + "\n" + ex.StackTrace, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
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