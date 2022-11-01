using Org.BouncyCastle.Bcpg.OpenPgp;
using Org.BouncyCastle.Bcpg;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Math;
using System.Diagnostics;

namespace WINTEST1
{
    internal class CreatKey
    {

        public int CreateThePAndVKey(char[] passwordS, string @a)
        {
            int state = 0;
            Debug.WriteLine("==============START===============");
            //RSA密鑰產生器
            IAsymmetricCipherKeyPairGenerator kpgS = GeneratorUtilities.GetKeyPairGenerator("RSA");
            //Key 構造使用參數        
            kpgS.Init(new RsaKeyGenerationParameters(
                   BigInteger.ValueOf(0x10001), new SecureRandom(),
            2048,// key 的長度
             25));
            AsymmetricCipherKeyPair kpS = kpgS.GenerateKeyPair();
            Stream out1, out2;
            string @aa = @a + "/key/priv.asc";
            string @ab = @a + "/key/pub.asc";
            if (System.IO.File.Exists(@aa))
            {
                MessageBox.Show(@aa + " 路徑中已存在金鑰");
            }
            if (System.IO.File.Exists(@ab))
            {
                MessageBox.Show(@ab + " 路徑中已存在金鑰");
            }
            else
            {
                try
                {
                    out1 = File.Create(@aa);//傳送方私鑰放置位置
                    out2 = File.Create(@ab); //傳送方公鑰放置位置
                    ExportKeyPair(out1, out2, kpS.Public,
                    kpS.Private, "Sender", passwordS, true);
                    Debug.WriteLine("==============end===============");
                    state = 1;
                }
                catch (Exception e)
                {
                    
                    MessageBox.Show("建立金鑰失敗，錯誤如下：可能有一組金鑰於此資料夾中" + e.ToString());
                    return state;
                }
            }
            return state;

        }
        //--------------------------------------------------------------------------------
        //======================輸出公私鑰到指定資料夾以共後續使用========================
        private static void ExportKeyPair(
          Stream secretOut,//私鑰放置位置
          Stream publicOut,//公鑰放置位置
          AsymmetricKeyParameter publicKey,//私鑰
          AsymmetricKeyParameter privateKey,//公鑰
          string identity,//身分
          char[] passPhrase,//密碼
          bool armor)
        {
            if (armor)
            {
                secretOut = new ArmoredOutputStream(secretOut);
            }
            PgpSecretKey secretKey = new PgpSecretKey(
                PgpSignature.DefaultCertification,
                PublicKeyAlgorithmTag.RsaGeneral,
                publicKey,
                privateKey,
                DateTime.UtcNow,
                identity,
                SymmetricKeyAlgorithmTag.Cast5,
                passPhrase,
                null,
                null,
                new SecureRandom()
                );
            secretKey.Encode(secretOut);
            if (armor)
            {
                secretOut.Close();
                publicOut = new ArmoredOutputStream(publicOut);
            }
            PgpPublicKey key = secretKey.PublicKey;
            key.Encode(publicOut);
            if (armor)
            {
                publicOut.Close();
            }


        }
    }
}
