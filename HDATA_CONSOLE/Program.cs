using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HDATA_CONSOLE
{
    class Program
    {
        public static string Decrypt(string cipherString, bool useHashing)
        {
            byte[] keyArray;
            //get the byte code of the string

            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            System.Configuration.AppSettingsReader settingsReader =
                                                new AppSettingsReader();
            //Get your key from config file to open the lock!
            string key = (string)settingsReader.GetValue("SecurityKey",typeof(String));

            if (useHashing)
            {
                //if hashing was used get the hash code with regards to your key
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //release any resource held by the MD5CryptoServiceProvider

                hashmd5.Clear();
            }
            else
            {
                //if hashing was not implemented get the byte code of the key
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes. 
            //We choose ECB(Electronic code Book)

            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(
                                 toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor                
            tdes.Clear();
            //return the Clear decrypted TEXT
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        public static string Encrypt(string toEncrypt, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
            // Get the key from config file

            string key = (string)settingsReader.GetValue("SecurityKey",typeof(String));
            //System.Windows.Forms.MessageBox.Show(key);
            //If hashing use get hashcode regards to your key
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //Always release the resources and flush data
                // of the Cryptographic service provide. Best Practice

                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes.
            //We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)

            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            byte[] resultArray =
              cTransform.TransformFinalBlock(toEncryptArray, 0,
              toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

        static void Main(string[] args)
        {
            CriptorafiaMD5 criptoMD5 = new CriptorafiaMD5();
            //criptoMD5.ComparaMD5();
            var novasenha = "1234";
            var OUTRAnovasenha = "123456789";
            var novasenhaCRIPTOGRAFADA = criptoMD5.RetornarMD5(novasenha);
            Console.WriteLine(novasenhaCRIPTOGRAFADA);
            Console.WriteLine(criptoMD5.ComparaMD5(OUTRAnovasenha, novasenhaCRIPTOGRAFADA));
            //if (GerarHashMd5(senhaDigitada) == registroTabela.Senha) { ... }

            //var novasenhaBD = 
            //var senhabanco = "827ccb0eea8a706c4c34a16891f84e7b";
            //var Senha = "12345";

            // Boolean ComparaSenha = criptoMD5.ComparaMD5(Senha, senhabanco);

            //  Console.WriteLine(ComparaSenha.ToString());





            //Console.WriteLine(Regex.IsMatch("mjj", "([a-z|A-Z]){2-30}") + " "+ Regex.IsMatch("Moisés 2", "([a-zA-Z])+"));
            ///Console.ReadKey();
            //DateTime data = DateTime.Now;
            //DateTime data_1 = DateTime.Now;

            //Console.WriteLine($"{data.ToShortDateString()} - Mais um mês -  {data.AddMonths(1).ToShortDateString()}");
            string reg = "002174394LA032";
            //002174394LA032 - NUM BI - OSVALDO
            //008060729LA040 - NUM BI - EMANUEL
            //       - NUM BI - MDG

            Console.WriteLine(Regex.IsMatch(reg,"(\\d){9}\\D{2}\\d{3}"));

            ////string pass = Encrypt("Deus te Ama",true);
            //string pass = MD5Hash("Deus te Ama");
            //Console.WriteLine(pass);
            //Console.WriteLine(MD5Hash(pass));
            //// Console.WriteLine(Decrypt(pass,true));


            Console.ReadKey();
        }

    }

}
