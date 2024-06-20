namespace WebpToGif_GUI
{
    public partial class Form1 : Form
    {
        string webpFilePath; // 파일 경로 저장용 변수
        public Form1()
        {
            webpFilePath = string.Empty;
            InitializeComponent();
        }


        // 파일 선택 버튼 클릭시 작동
        private void fileSelectButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    // 선택된 경로 저장 및 텍스트 박스에 표시
                    webpFilePath = openFileDialog1.FileName;
                    textBox1.Text = webpFilePath;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("파일을 여는 도중 에러가 발생했습니다: " + ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConvertButton_Click(object sender, EventArgs e)
        {
            if (!File.Exists(webpFilePath))
            {
                MessageBox.Show("파일을 찾을 수 없습니다: " + webpFilePath, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string gifFilePath = Path.ChangeExtension(webpFilePath, ".gif"); // gif 파일이 출력 될 경로
                Converter.ConvertWebPToGif(webpFilePath, gifFilePath);
                MessageBox.Show("변환 완료", "완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("에러 발생: " + ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}