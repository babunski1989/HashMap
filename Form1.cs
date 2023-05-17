using GenerickaListaSaMnogoElemenata.Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenerickaListaSaMnogoElemenata
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ListaSaMnogoElemenata<int> lista1 = new ListaSaMnogoElemenata<int>(8);
            IEnumerator<int> enumerator = lista1.GetEnumerator();
            bool uspesno = enumerator.MoveNext();

            lista1.AddRange(new int[] { 3, 2, 1, 9, 8, 7 });
            int brojIteracija = 0;
            foreach (int i in lista1)
            {
                int x = i;
                brojIteracija++;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //pravljenje kolekcija koje se porede
            int broj = 1000;
            int duzinaKolekcije = 1024;
            ListaSaMnogoElemenata<KompleksniBroj> listaSaMnogoElemenata = new ListaSaMnogoElemenata<KompleksniBroj>(duzinaKolekcije, duzinaKolekcije);
            List<KompleksniBroj> list = new List<KompleksniBroj>(broj * broj + 1);
            for (int re = 0; re < broj; re++)
            {
                for (int im = 0; im < broj; im++)
                {
                    KompleksniBroj kb = new KompleksniBroj(re, im);
                    listaSaMnogoElemenata.Add(kb);
                    list.Add(kb);
                }
            }
            int granica = 20;
            KompleksniBroj[] nizObjekata = new KompleksniBroj[400];
            for (int re = 0; re < granica; re++)
            {
                for (int im = 0; im < granica; im++)
                {
                    bool negativan = re % 2 == 1;
                    int tmpRe = negativan ? -re : re;
                    KompleksniBroj kb = new KompleksniBroj(tmpRe, im);
                    nizObjekata[re * granica + im] = kb;
                }
            }
            Stopwatch stopwatch1 = new Stopwatch();
            stopwatch1.Start();
            //petlja za provjeru u listi
            int brojElemenataKojiSuUListi = 0;
            foreach (KompleksniBroj kb in nizObjekata)
            {
                bool jesteUListi = list.Contains(kb);
                if(jesteUListi) brojElemenataKojiSuUListi++;
            }
            long msLista = stopwatch1.ElapsedMilliseconds;
            stopwatch1.Stop();

            Stopwatch stopwatch2 = new Stopwatch();
            stopwatch2.Start();
            //petlja za provjeru u listi sa mnogo elemenata
            int brojElemenataKojiSuUListiSaMnogoElemenata = 0;
            foreach (KompleksniBroj kb in nizObjekata)
            {
                bool jesteUListi=listaSaMnogoElemenata.Contains(kb);
                if(jesteUListi) brojElemenataKojiSuUListiSaMnogoElemenata++;
            }
            long msListaSaMnogoElemenata = stopwatch2.ElapsedMilliseconds;
            stopwatch2.Stop();
        }
    }
}
