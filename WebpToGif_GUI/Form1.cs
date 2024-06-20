namespace WebpToGif_GUI
{
    public partial class Form1 : Form
    {
        string webpFilePath; // ���� ��� ����� ����
        public Form1()
        {
            webpFilePath = string.Empty;
            InitializeComponent();
        }


        // ���� ���� ��ư Ŭ���� �۵�
        private void fileSelectButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    // ���õ� ��� ���� �� �ؽ�Ʈ �ڽ��� ǥ��
                    webpFilePath = openFileDialog1.FileName;
                    textBox1.Text = webpFilePath;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("������ ���� ���� ������ �߻��߽��ϴ�: " + ex.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConvertButton_Click(object sender, EventArgs e)
        {
            if (!File.Exists(webpFilePath))
            {
                MessageBox.Show("������ ã�� �� �����ϴ�: " + webpFilePath, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string gifFilePath = Path.ChangeExtension(webpFilePath, ".gif"); // gif ������ ��� �� ���
                Converter.ConvertWebPToGif(webpFilePath, gifFilePath);
                MessageBox.Show("��ȯ �Ϸ�", "�Ϸ�", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("���� �߻�: " + ex.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}