using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GenerickaListaSaMnogoElemenata.Buisness
{
    public class ListaSaMnogoElemenata<T> : IEnumerable<T>, IEnumerable where T : IEquatable<T>
    {
        #region Data
        private List<T>[] nizKolekcija;
        public const int PodrazumevaniBrojKolekcija = 256;
        public const int MinimalniBrojKolekcija = 8;
        public const int PodrazumevaniBrojElemenataUJednojKolekciji = 256;
        public const int MinimalniBrojElemenataUJednojKolekciji = 8;
        private int brojElemenataUKolekciji;
        #endregion

        #region Constructors
        public ListaSaMnogoElemenata()
        {
            this.nizKolekcija = new List<T>[PodrazumevaniBrojKolekcija];
            this.brojElemenataUKolekciji = PodrazumevaniBrojElemenataUJednojKolekciji;
            this.NapraviKolekcije();
        }
        public ListaSaMnogoElemenata(int brojKolekcija, int brojElemenataUKolekciji = PodrazumevaniBrojElemenataUJednojKolekciji)
        {
            if (brojKolekcija < MinimalniBrojKolekcija)
                brojKolekcija = MinimalniBrojKolekcija;
            this.nizKolekcija = new List<T>[brojKolekcija];
            if (brojElemenataUKolekciji < MinimalniBrojElemenataUJednojKolekciji)
                this.brojElemenataUKolekciji = MinimalniBrojElemenataUJednojKolekciji;
            else this.brojElemenataUKolekciji = brojElemenataUKolekciji;
            this.NapraviKolekcije();
        }
        #endregion

        #region Methods
        private void NapraviKolekcije()
        {
            for (int i = 0; i < nizKolekcija.Length; i++)
            {
                this.nizKolekcija[i] = new List<T>(brojElemenataUKolekciji);
            }
        }
        private int IndeksKolekcijeZaElement(T element)
        {
            int hashCode = element.GetHashCode();
            int indeksKolekcije = hashCode % this.nizKolekcija.Length;
            if (indeksKolekcije < 0)
                indeksKolekcije += this.nizKolekcija.Length;
            return indeksKolekcije;
        }
        public void Add(T element)
        {
            int indeksKolekcije = this.IndeksKolekcijeZaElement(element);
            this.nizKolekcija[indeksKolekcije].Add(element);
        }
        public void AddRange(IEnumerable<T> elements)
        {
            foreach (T element in elements)
            {
                this.Add(element);
            }
        }
        public bool Contains(T element)
        {
            int indeksKolekcije = this.IndeksKolekcijeZaElement(element);
            bool postoji = this.nizKolekcija[indeksKolekcije].Contains(element);
            return postoji;
        }
        #endregion

        #region Implementacija interfejsa
        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator_ZaListuSaMnogoElemenata<T>(nizKolekcija);
        }
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
        #endregion
    }

    //klasa koja je enumerator(odnosno iterator) za prolazenje kroz kolekciju ListaSaMnogoElemenata<T>
    public class Enumerator_ZaListuSaMnogoElemenata<T> : IEnumerator<T>, IEnumerator
    {
        #region Data
        private List<T>[] nizKolekcija;
        private int indeksKolekcije = 0;
        private int indeksElementaUKolekciji = -1;
        #endregion

        #region Constructors
        public Enumerator_ZaListuSaMnogoElemenata(List<T>[] nizKolenkcija)
        {
            this.nizKolekcija = nizKolenkcija;
        }
        #endregion

        #region Methods
        //private bool PredjiNaSledecuKolekciju()
        //{
        //    indeksKolekcije++;
        //    if (indeksKolekcije >= nizKolekcija.Length)
        //        return false;
        //    indeksElementaUKolekciji = -1;
        //    if (nizKolekcija[indeksKolekcije].Count > 0)
        //        return true;
        //    return PredjiNaSledecuKolekciju();
        //}
        #endregion

        #region Implementacija interfejsa
        //implementacija generickog interfejsa IEnumerator<T>
        public T Current
        {
            get
            {
                if (this.indeksKolekcije < 0 || this.indeksKolekcije >= this.nizKolekcija.Length)
                    throw new InvalidOperationException();
                if (this.indeksElementaUKolekciji < 0 || this.indeksElementaUKolekciji >= this.nizKolekcija[indeksKolekcije].Count)
                    throw new InvalidOperationException();
                return this.nizKolekcija[indeksKolekcije][this.indeksElementaUKolekciji];
                
            }
        }
        object IEnumerator.Current => this.Current;
        public bool MoveNext()
        {
            //bool postojiSledeciElement = true;
            //if (indeksKolekcije == -1)
            //    postojiSledeciElement = PredjiNaSledecuKolekciju();
            //if (!postojiSledeciElement) return false;
            indeksElementaUKolekciji++;
            if (indeksElementaUKolekciji < nizKolekcija[indeksKolekcije].Count) return true;
            indeksElementaUKolekciji = 0;
            indeksKolekcije++;
            while (indeksKolekcije < nizKolekcija.Length && nizKolekcija[indeksKolekcije].Count == 0)
                indeksKolekcije++;
            return indeksKolekcije < nizKolekcija.Length;
            //postojiSledeciElement = PredjiNaSledecuKolekciju();
            //this.indeksElementaUKolekciji++;
            //return postojiSledeciElement;
        }
        public void Reset()
        {
            this.indeksElementaUKolekciji = -1;
            this.indeksKolekcije = 0;
        }
        #endregion
        public void Dispose() { }
    }
}
