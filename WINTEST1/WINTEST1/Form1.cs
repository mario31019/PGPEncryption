
using System.Diagnostics;
using PGPTEST3;
using PGPTEST2;
using Microsoft.VisualBasic;

namespace WINTEST1
{
    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog choosefiletodecodeDialog = new OpenFileDialog()
            {
                Title = "����m"
            };
            DialogResult dialogResult = choosefiletodecodeDialog.ShowDialog();
            string message = "";
            if (dialogResult == DialogResult.Cancel)
            {
                return;
            }
            else if (choosefiletodecodeDialog.CheckFileExists)
            {
                try
                {
                    string ext = choosefiletodecodeDialog.DefaultExt;
                    string filetodecodepath = choosefiletodecodeDialog.FileName;//�ݥ[�K���ɮצW�ٸ��m
                    Debug.WriteLine("�ɮ�filetodecodepath======" + filetodecodepath);
                    StreamReader streamReader = new StreamReader(filetodecodepath);

                    string keyFileName = System.Environment.CurrentDirectory+ "/key/pub.asc";
                    //���л\����
                    string decodedfilepath = filetodecodepath.Replace("." + ext, "_Lock." + ext);
                    string result;
                    result = Path.ChangeExtension(decodedfilepath, Path.GetExtension(filetodecodepath));//�s��m
                    
                    LockTest lll = new LockTest();
                    lll.LockT(filetodecodepath, keyFileName, result);
                    message = "�[�K���\�A�ɮפw�x�s��" + result;

                }
                catch (Exception ex)
                {
                    message = "�[�K���ѡA���~�p�U�G" + ex.ToString();
                }
            }
            else
                message = "�п���ɮ�";
            MessageBox.Show(message);

        }
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
            Environment.Exit(Environment.ExitCode);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog choosefiletodecodeDialog = new OpenFileDialog()
            {
                Title = "����m"
            };
            DialogResult dialogResult = choosefiletodecodeDialog.ShowDialog();
            string message = "";
            if (dialogResult == DialogResult.Cancel)
            {
                return;
            }
            else if (choosefiletodecodeDialog.CheckFileExists)
            {
                try
                {
                    string ext = choosefiletodecodeDialog.DefaultExt;
                    string filetodecodepath = choosefiletodecodeDialog.FileName;//�ݸѱK���ɮצW�ٸ��m
                    StreamReader streamReader = new StreamReader(filetodecodepath);

                    //======================================================================
                    string keyFileName = System.Environment.CurrentDirectory + "/key/priv.asc";
                    String s = Interaction.InputBox("��J���_�K�X", "���D", null, -1, -1);
                    if (String.IsNullOrWhiteSpace(s))
                    {
                        Debug.WriteLine("����J�K�X2");
                        MessageBox.Show("����J�K�X1");
                    }
                    else {
                        char[] passwd = s.ToCharArray();//������R�p�_���K�X


                        //�л\����
                        string decodedfilepath = filetodecodepath.Replace("." + ext, "_UnLock." + ext);
                        string result;
                        result = Path.ChangeExtension(decodedfilepath, Path.GetExtension(filetodecodepath));//�s��m
                        UnLockTest uuu = new UnLockTest();
                        uuu.UnLockT(filetodecodepath, keyFileName, passwd, result);
                        
                    }



                }
                catch (Exception ex)
                {
                    message = "�ѱK���ѡA���~�p�U�G" + ex.ToString();
                }
            }
            else
                message = "�п���ɮ�";
        }
        private void button5_Click(object sender, EventArgs e)
        {

            String s = Interaction.InputBox("��J���_�K�X", "���D", null, -1, -1);
            if (String.IsNullOrWhiteSpace(s))
            {
                Debug.WriteLine("����J�K�X");
                MessageBox.Show("����J�K�X");
            }
            else {
                char[] c = s.ToCharArray();//������R�p�_���K�X
                CreatKey ccc = new CreatKey();
                int ss = 0;
                string str = System.Environment.CurrentDirectory;
                ss=ccc.CreateThePAndVKey(c, str);
                if (ss == 1) {
                    Close();
                    Environment.Exit(Environment.ExitCode);
                }

            }
            


        }
    }
}