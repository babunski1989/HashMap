using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerickaListaSaMnogoElemenata.Buisness
{
    public class KompleksniBroj : IEquatable<KompleksniBroj>
    {
        #region Data
        private int re;
        private int im;        
        #endregion

        #region Construstors
        public KompleksniBroj(int re, int im)
        {
            this.Re = re;
            this.Im = im;
        }
        #endregion

        #region Properties
        public int Re { get => re; set => re = value; }
        public int Im { get => im; set => im = value; }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"{this.Re}, {this.Im}";
        }
        public override bool Equals(object obj)
        {
            KompleksniBroj kb = obj as KompleksniBroj;
            return this == kb;
        }
        public override int GetHashCode()
        {
            return Re.GetHashCode() ^ Im.GetHashCode();
        }
        public bool Equals(KompleksniBroj obj)
        {
            return this == obj;
        }
        #endregion
    }
}
