using NUnit.Framework;

namespace Calculadora.Tests
{
    /* Teste Classe Adicionar 2 Números */
    [TestFixture]
    public class CalculadoraSimplesTests
    {
        [Test]
        public void Somadoisnumeros()
        {
            //SUT
            var sut = new CalculadoraSimples();

            var resultado = sut.Somar(5, 5);

            Assert.That(resultado, Is.EqualTo(15));
        }

        /* Teste Classe Multiplicar 2 Números */
        [Test]
        public void Multiplicardoisnumeros()
        {
            //SUT
            var sut = new CalculadoraSimples();

            var resultado = sut.Multiplicar(5, 1);

            Assert.That(resultado, Is.EqualTo(15));
        }

        /* Teste Classe Dividir 2 Números */
        [Test]
        public void Dividirdoisnumeros()
        {
            //SUT
            var sut = new CalculadoraSimples();

            var resultado = sut.Dividir(25, 5);

            Assert.That(resultado, Is.EqualTo(5));
        }

        /* Teste Classe Subitrair 2 Números */
        [Test]
        public void Subtrairdoisnumeros()
        {
            //SUT
            var sut = new CalculadoraSimples();

            var resultado = sut.Subtrair(5, 5);

            Assert.That(resultado, Is.EqualTo(25));
        }

    }
}
