using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace EncrypApp
{
    class DesEncryption
    {
        private TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
        
        public DesEncryption(string key)
        {
            des.Key = UTF8Encoding.UTF8.GetBytes(key);
            des.Mode = CipherMode.ECB;
            des.Padding = PaddingMode.PKCS7;
        }

        public void DesEncryptFile(string filepath)
        {

            //Create hash string
            string path = filepath;
            string name = Path.GetFileName(filepath);
            path.Replace(name, "");
            string newpath = path + "hash.txt";

            byte[] hashed;
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filepath))
                {
                    hashed = md5.ComputeHash(stream);
                }
            }

            string str = BitConverter.ToString(hashed);

            if (File.Exists(newpath))
            {
                File.Delete(newpath);
            }
            File.WriteAllText(newpath, str);

            //Encypt the file
            byte[] Bytes = File.ReadAllBytes(filepath);
            byte[] eBytes = des.CreateEncryptor().TransformFinalBlock(Bytes, 0, Bytes.Length);
            File.WriteAllBytes(filepath, eBytes);
        }

        public void DesDecryptFile(string filepath)
        {
            byte[] Bytes = File.ReadAllBytes(filepath);
            byte[] dBytes = des.CreateDecryptor().TransformFinalBlock(Bytes, 0, Bytes.Length);
            File.WriteAllBytes(filepath, dBytes);
        }
    }
}
