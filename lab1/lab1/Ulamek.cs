using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    class Ulamek : IEquatable<Ulamek>, IComparable<Ulamek> 
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
            int wynik = licznik / mianownik;
            Console.WriteLine(wynik);
        }
        public Ulamek(Ulamek ulamek)
        {
            licznik = ulamek.licznik;
            mianownik = ulamek.mianownik;
        }
        public override string ToString()
        {
            return ((double) (licznik / mianownik)).ToString();
        }
        public int Equals(Ulamek other)
        {
            throw new NotImplementedException();
        }
        public int CompareTo(Ulamek other)
        {
            throw int (licznik)= ; //NotImplementedException();
        }

    }
}
