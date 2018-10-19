using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalculadoraWeb.Models
{
    public class Calculadora
    {
        private int a, b;

        public int A
        {
            get
            {
                return a;
            }

            set
            {
                a = value;
            }
        }

        public int B
        {
            get
            {
                return b;
            }

            set
            {
                b = value;
            }
        }

    

        public Calculadora()
        {
          
        }

        public decimal Soma(int a, int b)
        {
            int total = 0;

            total = a + b;

            return total;


        }

        public decimal Subtracao(int a, int b)
        {
            int total = 0;

            total = a - b;

            return total;


        }

        public decimal Multiplicacao(int a, int b)
        {
            int total = 0;

            total = a * b;

            return total;


        }

        public decimal Divisao(int a, int b)
        {
            int total = 0;

            total = a / b;

            return total;

		}
		public decimal Porcentagem(int a, int b)
		{
			int total = 0;

			total = a * b /100;

			return total;

		}

	}
}
