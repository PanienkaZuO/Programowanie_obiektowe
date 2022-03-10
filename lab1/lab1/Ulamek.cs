using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    class Ulamek: IEquatable<T>, IComparable<T>
    {
        private int licznik, mianownik;

        public Ulamek(int Licznik, int Mianownik)
        {
            licznik = Licznik;
            mianownik = Mianownik;
        }
        public Ulamek()
        {
            int licznik = 0;
            int mianownik = 1;
        }
        public Ulamek(Ulamek ulamek)
        {
            licznik = ulamek.licznik;
            mianownik = ulamek.mianownik;
        }
        public override string ToString()
        {

        }


    }
}
