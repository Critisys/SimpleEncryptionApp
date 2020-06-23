using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace EncrypApp
{
    public partial class Form1 : Form
    {
        private byte[] Hashed_value;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog OD = new OpenFileDialog();
            OD.Filter = "All Files|*";
            OD.FileName = "";
            if (OD.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = OD.FileName;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog OD = new OpenFileDialog();
            OD.Filter = "TXT Files|*.txt";
            OD.FileName = "";
            if (OD.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = OD.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string key;
            try
            {
                if (textBox2.Text == "") throw new ArgumentException("Key link cannot be empty");
                else key = File.ReadAllText(textBox2.Text);
                DesEncryption Des = new DesEncryption(key);
                Des.DesEncryptFile(textBox1.Text);
                GC.Collect();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string key;
            try
            {
                if (textBox2.Text == "") throw new ArgumentException("Key link cannot be empty");
                else key = File.ReadAllText(textBox2.Text);
                DesEncryption Des = new DesEncryption(key);
                Des.DesDecryptFile(textBox1.Text);

                byte[] hased;
                using(var md5 = MD5.Create())
                {
                    using(var stream = File.OpenRead(textBox1.Text))
                    {
                        hased = md5.ComputeHash(stream);
                    }
                }
                Hashed_value = hased;
                textBox4.Text = BitConverter.ToString(hased);
                GC.Collect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RSAToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                RSAEncryption rsa = new RSAEncryption();
                rsa.RSAEncrypt(textBox1.Text);
                GC.Collect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string key;
            try
            {
                if (textBox2.Text == "") throw new ArgumentException("Key link cannot be empty");
                else key = File.ReadAllText(textBox2.Text);
                RSAEncryption rsa = new RSAEncryption();
                rsa.RSADecrypt(textBox1.Text, key);

                byte[] hased;
                using (var md5 = MD5.Create())
                {
                    using (var stream = File.OpenRead(textBox1.Text))
                    {
                        hased = md5.ComputeHash(stream);
                    }
                }
                Hashed_value = hased;
                textBox4.Text = BitConverter.ToString(hased);
                GC.Collect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string key;
            try
            {
                if (textBox2.Text == "") throw new ArgumentException("Key link cannot be empty");
                else key = File.ReadAllText(textBox2.Text);
                RSAEncryption rsa = new RSAEncryption();
                rsa.RSA_Encrypt(textBox1.Text, key);
                GC.Collect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            OpenFileDialog OD = new OpenFileDialog();
            OD.Filter = "TXT Files|*.txt";
            OD.FileName = "";
            if (OD.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = OD.FileName;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                string hash1, hash2;
                if (textBox3.Text == "") throw new ArgumentException("Hash link cannot be empty");
                else hash1 = File.ReadAllText(textBox3.Text);

                if (textBox4.Text == "") throw new ArgumentException("No hash value to compare");
                hash2 = textBox4.Text;
                if (hash1 == hash2)
                {
                    textBox5.Text = "Match";
                }
                else textBox5.Text = "Unmatch";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
