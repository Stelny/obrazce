using System;
using System.Collections.Generic;
using System.Text;

namespace obrazce
{
    public class Tvar
    {
        // barva, tloustka, vypln, obsah, obvod - atributy tridy Tvar, ktere jsou pristupne z dedenych trid "protected"
        // tvar - privatni atribut tridy Tvar

        protected string barva;
        protected string tloustka;
        protected bool vypln;
        protected float obsah;
        protected float obvod;
        protected string nejvetsi_strana;
        private string tvar;

        public string tvarp
        {
            get { return tvar; }   // get umoznuje nacist hodnotu z tvar (private), lze pouzit i set na jeji zmenu
        }

        public Tvar(string barva, float tloustka, bool vypln)
        {
            this.barva = barva;
            this.tloustka = tloustka.ToString();
            this.vypln = vypln;


            Zadej_Tvar();
        }

        // Vrat_Barvu, Vrat_Obsah, Vrat_Obvod, Vrat_Tloustku - public metody tridy Tvar, lze k nim pristupovat zvenci
        public string Vrat_Barvu()
        {
            return this.barva;
        }

        public float Vrat_Obsah()
        {
            return this.obsah;
        }

        public float Vrat_Obvod()
        {
            return this.obvod;
        }

        // pokud je metoda "virtual" lze ji nasledne nahradit v dedenych objektech
        public virtual string Vrat_Tloustku()
        {
            return this.tloustka;
        }
        public virtual string Vrat_nejvetsi_strana()
        {
            return this.nejvetsi_strana;
        }

        // Zadej_Tvar - privatni metody tridy Tvar - neni pristupny z venci, definuje atribut tvar dle tridy Ctverec, Obdelnik, Kruh ktere vychazí z tridy Tvar (dedicnost)
        private void Zadej_Tvar()
        {
            if (this is Ctverec)
            {
                this.tvar = "ctverec";
            }

            if (this is Obdelnik)
            {
                this.tvar = "obdelnik";
            }

            if (this is Kruh)
            {
                this.tvar = "kruh";
            }
            if (this is Trojuhelnik)
            {
                this.tvar = "trojuhelnik";
            }
        }
    }

    // trida Ctverec dedi atributy a metody z tridy Tvar
    public class Ctverec : Tvar
    {
        // strana - privatni atribut tridy Ctverec
        // barva, tloustka, vypln, obsah, obvod - dedene atributy z tridy Tvar
        private float strana;

        public Ctverec(string barva, float tloustka, bool vypln, float strana) : base(barva, tloustka, vypln)
        {
            this.strana = strana;

            // vypocet obsahu a obvodu - privatni metody tridy Ctverec
            Vypocti_Obsah();
            Vypocti_Obvod();
            Vypocti_nej_stranu();
        }

        // metody tridy Kruh - Vypocti_Obsah, Vypocti_Obvod
        // metody Vrat_Barvu, Vrat_Obsah, Vrat_Obvod, Vrat_Tloustku jsou dedeny z tridy Tvar
        private void Vypocti_Obsah()
        {
            this.obsah = strana * strana;
        }

        private void Vypocti_Obvod()
        {
            this.obvod = 4 * strana;
        }
        private void Vypocti_nej_stranu()
        {
            this.nejvetsi_strana = strana.ToString();
        }
    }

    // trida Obdelnik dedi atributy a metody z tridy Tvar
    public class Obdelnik : Tvar
    {
        // strana_a, strana_b - privatni atriubuty tridy Obdelnik
        // barva, tloustka, vypln, obsah, obvod - dedene atributy z tridy Tvar
        private float strana_a;
        private float strana_b;

        // konstruktor tridy Obdelnik
        public Obdelnik(string barva, float tloustka, bool vypln, float strana_a, float strana_b) : base(barva, tloustka, vypln)
        {
            this.strana_a = strana_a;
            this.strana_b = strana_b;

            // vypocet obsahu a obvodu - privatni metody tridy Obdelnik
            Vypocti_Obsah();
            Vypocti_Obvod();
            Vypocti_nej_stranu();
        }

        // metody tridy Kruh - Vypocti_Obsah, Vypocti_Obvod
        // metody Vrat_Barvu, Vrat_Obsah, Vrat_Obvod, Vrat_Tloustku jsou dedeny z tridy Tvar
        private void Vypocti_Obsah()
        {
            this.obsah = strana_a * strana_b;
        }

        private void Vypocti_Obvod()
        {
            this.obvod = (2 * strana_a) + (2 * strana_b);
        }
        private void Vypocti_nej_stranu()
        {
            if (strana_a > strana_b)
            {
                this.nejvetsi_strana = strana_a.ToString();
            }
            else
            {
                this.nejvetsi_strana = strana_b.ToString();
            }
        }
    }

    // trida Kruh dedi atributy a metody z tridy Tvar
    public class Kruh : Tvar
    {
        // polomer - privatni atriubuty tridy Kruh
        // barva, tloustka, vypln, obsah, obvod - dedene atributy z tridy Tvar
        private float polomer;

        // konstruktor tridy Kruh, atributy (barva, tloustka, vypln) jsou dedeny z tridy tvar, trida Kruh pridava navic atribut polomer
        public Kruh(string barva, float tloustka, bool vypln, float polomer) : base(barva, tloustka, vypln)
        {
            this.polomer = polomer;

            // vypocet obsahu a obvodu - privatni metody tridy Kruh
            Vypocti_Obsah();
            Vypocti_Obvod();
        }

        // metody tridy Kruh - Vypocti_Obsah, Vypocti_Obvod, Vrat_Tloustku (prepisuje puvodni metodu z tridy Tvar)
        // metody Vrat_Barvu, Vrat_Obsah, Vrat_Obvod jsou dedeny z tridy Tvar
        // metoda Vrat_Tloustku (prepsana puvodni metoda "override" z tridy tvar, ve tride tvar musi byt tzn. oznacena jako "Virtual") = polymorfismus
        private void Vypocti_Obsah()
        {
            this.obsah = (float)Math.PI * (float)Math.Pow(polomer, 2);
        }

        // Math.PI a Math.Pow vraci double, proto pretypvani na float - atribut obsah je float (vypocet neni presny kvuli pretypovani)
        private void Vypocti_Obvod()
        {
            this.obvod = 2 * (float)Math.PI * polomer;
        }

        // nahradi metodu Vrat_Tloustku() z objektu Tvar -> "override", ve objektu tvar musi byt Vrat_Tloustku oznaceno "virtual"
        public override string Vrat_Tloustku()
        {
            return "neexsituje tloustka pro tento tvar";
        }

        public override string Vrat_nejvetsi_strana()
        {
            return "nejdelší strana nemá stranu -> radius";
        }
    }
    public class Trojuhelnik : Tvar
    {
        // strana - privatni atribut tridy Ctverec
        // barva, tloustka, vypln, obsah, obvod - dedene atributy z tridy Tvar
        private float strana_a;
        private float strana_b;
        private float strana_c;
        private float vyska;

        public Trojuhelnik(string barva, float tloustka, bool vypln, float strana_a, float strana_b, float strana_c, float vyska) : base(barva, tloustka, vypln)
        {
            this.strana_a = strana_a;
            this.strana_b = strana_b;
            this.strana_c = strana_c;
            this.vyska = vyska;

            // vypocet obsahu a obvodu - privatni metody tridy trojuhelnik
            Vypocti_Obsah();
            Vypocti_Obvod();
            Vypocti_nej_stranu();
        }

        // metody tridy Kruh - Vypocti_Obsah, Vypocti_Obvod
        // metody Vrat_Barvu, Vrat_Obsah, Vrat_Obvod, Vrat_Tloustku jsou dedeny z tridy Tvar
        private void Vypocti_Obsah()
        {
            this.obsah = (strana_a * vyska) / 2;
        }

        private void Vypocti_Obvod()
        {
            this.obvod = strana_a + strana_b + strana_c;
        }
        private void Vypocti_nej_stranu()
        {
            if (strana_a > strana_b && strana_a > strana_c)
            {
                this.nejvetsi_strana = strana_a.ToString();
            }
            else if (strana_b > strana_a && strana_b > strana_c)
            {
                this.nejvetsi_strana = strana_b.ToString();
            }
            else
            {
                this.nejvetsi_strana = strana_c.ToString();
            }
        }

    }
}
