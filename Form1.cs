using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace rssv_lv1
{
    public partial class Form1 : Form
    {
        private System.Timers.Timer t;
        int i = 0;
        public Form1()
        {
            InitializeComponent();
            button1.Enabled = false;
            t = new System.Timers.Timer(1000);
            t.Elapsed += new System.Timers.ElapsedEventHandler(vrijeme);

        }
        


        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
            checkInputValue(richTextBox2.Text);
        }

        public void checkInputValue(string value)
        {
            if (!int.TryParse(value, out int i))
            {
                button1.Enabled = false;
                label1.Text = "Niste unijeli broj ili je prazno polje";
                return;
            }
            label1.Text = "Kritična vrijednost";
            button1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (t.Enabled == false)
            {
                //Pokretanje timer-a.
                t.Start(); //Odgovara: t.Enabled = true;
                button1.Text = "Zaustavi";
            }
            else
            {
                //Zaustavljanje timer-a
                t.Stop(); //Odgovara: t.Enabled = false;
                button1.Text = "Pokreni";
            }
        }

        
        private void vrijeme(object s, EventArgs e)
        {
                StreamReader citac = new StreamReader("value.txt");
                int criticalValue = Int32.Parse(richTextBox2.Text);
                int[] valueArray =
                Array.ConvertAll(citac.ReadLine().Split(',').ToArray(), int.Parse);
                citac.Close();


            Invoke((MethodInvoker)delegate //Anonimna metoda
            {
                if (i >= valueArray.Length)
                {
                    t.Stop();
                    MessageBox.Show("Provjera gotova, nema vrijednosti vecih od kriticne.");
                    i = 0;
                    richTextBox1.Text = "";
                    button1.Text = "Pokreni";
                    return;

                }
                richTextBox1.Text = valueArray[i].ToString();
                Application.DoEvents();
                if (valueArray[i] > criticalValue)
                {
                    t.Stop();
                    MessageBox.Show(valueArray[i].ToString() + " je veci od kriticne vrijednosti, prekidam program.");
                    i = 0;
                    richTextBox1.Text = "" ;
                    button1.Text = "Pokreni";
                    return;
                }
                i++;
            });
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
