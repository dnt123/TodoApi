using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class Anvandare
    {
        public string Fornamn { get; private set; }
        public string Efternamn { get; private set; }
        public string Personnummer { get; private set; }


        public class Builder
        {
            private string Fornamn { get; set; }
            private string Efternamn { get; set; }
            private string Personnummer { get; set; }

            public Builder(string fornamn, string efternamn)
            {
                Fornamn = fornamn;
                Efternamn = efternamn;
            }

            public Builder PersonnummerMethod(string pnr)
            {
                Personnummer = pnr;
                return this;
            }


            public Anvandare Build()
            {
                return new Anvandare
                {
                    Fornamn = Fornamn,
                    Efternamn = Efternamn,
                    Personnummer = Personnummer,
                };
            }
        }

        
    }

}
