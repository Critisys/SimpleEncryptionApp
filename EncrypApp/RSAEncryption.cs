using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace EncrypApp
{
    class RSAEncryption
    {
        private string public_key;
        private string private_key;

        public void GeneratePair()
        {
            using (var ras = new RSACryptoServiceProvider())
            {
                public_key = ras.ToXmlString(false);
                private_key = ras.ToXmlString(true);
            }
        }

        public RSAEncryption()
        {
            GeneratePair();
        }

        //Encryption with ramdom key generated
        public void RSAEncrypt(string filepath)
        {
            byte[] Bytes = File.ReadAllBytes(filepath);
            using(var rsa = new RSACryptoServiceProvider())
            {

                string path = filepath;
                string name = Path.GetFileName(filepath);
                path.Replace(name, "");
                
                //Creat hash string
                byte[] hashed;
                using (var md5 = MD5.Create())
                {
                    using (var stream = File.OpenRead(filepath))
                    {
                        hashed = md5.ComputeHash(stream);
                    }
                }

                string str = BitConverter.ToString(hashed);

                if (File.Exists(path+"hash.txt"))
                {
                    File.Delete(path + "hash.txt");
                }
                File.WriteAllText(path+"hast.txt", str);

                //Encrypt the file
                rsa.FromXmlString(public_key);
                byte[] eBytes = rsa.Encrypt(Bytes, false);
                File.WriteAllBytes(filepath, eBytes);

                //Write pair key to text file
                if (File.Exists(path+"private_key.txt"))
                {
                    File.Delete(path+"private_key.text");
                }
                File.WriteAllText(path+"private_key.txt", private_key);

                if (File.Exists(path + "public_key.txt"))
                {
                    File.Delete(path + "public_key.text");
                }
                File.WriteAllText(path+"public_key.txt", public_key);
            }
        }


        //Encryption with given public key in text file 
        public void RSA_Encrypt(string filepath,string key)
        {
            string path = filepath;
            string name = Path.GetFileName(filepath);
            path.Replace(name, "");
            string newpath = path + "hash.txt";

            //Create hash string
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

            //Encrypt the file
            byte[] Bytes = File.ReadAllBytes(filepath);
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(key);
                byte[] eBytes = rsa.Encrypt(Bytes, false);
                File.WriteAllBytes(filepath, eBytes);
            }
        }

        public void RSADecrypt(string filepath, string key)
        {
            byte[] Bytes = File.ReadAllBytes(filepath);
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(key);
                byte[] eBytes = rsa.Decrypt(Bytes, false);
                File.WriteAllBytes(filepath, eBytes);
            }
        }

    }
}
