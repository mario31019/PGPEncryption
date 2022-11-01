
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
                Title = "文件位置"
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
                    string filetodecodepath = choosefiletodecodeDialog.FileName;//待加密的檔案名稱跟位置
                    Debug.WriteLine("檔案filetodecodepath======" + filetodecodepath);
                    StreamReader streamReader = new StreamReader(filetodecodepath);

                    string keyFileName = System.Environment.CurrentDirectory+ "/key/pub.asc";
                    //不覆蓋原檔
                    string decodedfilepath = filetodecodepath.Replace("." + ext, "_Lock." + ext);
                    string result;
                    result = Path.ChangeExtension(decodedfilepath, Path.GetExtension(filetodecodepath));//新位置
                    
                    LockTest lll = new LockTest();
                    lll.LockT(filetodecodepath, keyFileName, result);
                    message = "加密成功，檔案已儲存為" + result;

                }
                catch (Exception ex)
                {
                    message = "加密失敗，錯誤如下：" + ex.ToString();
                }
            }
            else
                message = "請選擇檔案";
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
                Title = "文件位置"
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
                    string filetodecodepath = choosefiletodecodeDialog.FileName;//待解密的檔案名稱跟位置
                    StreamReader streamReader = new StreamReader(filetodecodepath);

                    //======================================================================
                    string keyFileName = System.Environment.CurrentDirectory + "/key/priv.asc";
                    String s = Interaction.InputBox("輸入金鑰密碼", "標題", null, -1, -1);
                    if (String.IsNullOrWhiteSpace(s))
                    {
                        Debug.WriteLine("未輸入密碼2");
                        MessageBox.Show("未輸入密碼1");
                    }
                    else {
                        char[] passwd = s.ToCharArray();//接收方R私鑰的密碼


                        //覆蓋原檔
                        string decodedfilepath = filetodecodepath.Replace("." + ext, "_UnLock." + ext);
                        string result;
                        result = Path.ChangeExtension(decodedfilepath, Path.GetExtension(filetodecodepath));//新位置
                        UnLockTest uuu = new UnLockTest();
                        uuu.UnLockT(filetodecodepath, keyFileName, passwd, result);
                        
                    }



                }
                catch (Exception ex)
                {
                    message = "解密失敗，錯誤如下：" + ex.ToString();
                }
            }
            else
                message = "請選擇檔案";
        }
        private void button5_Click(object sender, EventArgs e)
        {

            String s = Interaction.InputBox("輸入金鑰密碼", "標題", null, -1, -1);
            if (String.IsNullOrWhiteSpace(s))
            {
                Debug.WriteLine("未輸入密碼");
                MessageBox.Show("未輸入密碼");
            }
            else {
                char[] c = s.ToCharArray();//接收方R私鑰的密碼
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