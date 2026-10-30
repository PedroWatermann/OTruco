using System;
using System.Collections.Generic;
using OTruco.Core;
using NUnit.Framework;

public class ComparadorDeCartasTest
{
    private static ComparadorDeCartas ComVira(Valor vira) => new(new Carta(Naipe.Paus, vira));

    private static ComparadorDeCartas ComViraParaComuns() => ComVira(Valor.Tres);

    [TestCase(Valor.As, Valor.Dois)]
    [TestCase(Valor.Dois, Valor.Tres)]
    [TestCase(Valor.Tres, Valor.Quatro)]
    [TestCase(Valor.Quatro, Valor.Cinco)]
    [TestCase(Valor.Cinco, Valor.Seis)]
    [TestCase(Valor.Seis, Valor.Sete)]
    [TestCase(Valor.Sete, Valor.Dama)]
    [TestCase(Valor.Dama, Valor.Valete)]
    [TestCase(Valor.Valete, Valor.Rei)]
    [TestCase(Valor.Rei, Valor.As)]
    public void RN05_Vira_DefineValorDaManilha(Valor vira, Valor manilha)
    {
        // Arrange
        Carta cartaVira = new(Naipe.Paus, vira);

        // Act
        ComparadorDeCartas comparador = new(cartaVira);

        // Assert
        Assert.That(comparador.ValorDaManilha, Is.EqualTo(manilha));
    }

    [TestCase(Valor.As, Valor.Dois, Naipe.Ouros)]
    [TestCase(Valor.As, Valor.Dois, Naipe.Espadas)]
    [TestCase(Valor.As, Valor.Dois, Naipe.Copas)]
    [TestCase(Valor.As, Valor.Dois, Naipe.Paus)]
    public void EhManilha_CartaComValorDaManilha_RetornaTrue(Valor vira, Valor valorManilha, Naipe naipeManilha)
    {
        // Arrange
        Carta cartaVira = new(Naipe.Paus, vira);
        Carta cartaManilha = new(naipeManilha, valorManilha);

        // Act
        ComparadorDeCartas comparador = new(cartaVira);

        // Assert
        Assert.That(comparador.EhManilha(cartaManilha), Is.True);
    }

    [TestCase(Valor.As, Valor.As, Naipe.Ouros)]
    [TestCase(Valor.As, Valor.Tres, Naipe.Espadas)]
    [TestCase(Valor.As, Valor.Quatro, Naipe.Copas)]
    [TestCase(Valor.As, Valor.Cinco, Naipe.Paus)]
    [TestCase(Valor.As, Valor.Seis, Naipe.Ouros)]
    [TestCase(Valor.As, Valor.Sete, Naipe.Espadas)]
    [TestCase(Valor.As, Valor.Dama, Naipe.Copas)]
    [TestCase(Valor.As, Valor.Valete, Naipe.Paus)]
    [TestCase(Valor.As, Valor.Rei, Naipe.Ouros)]
    public void EhManilha_CartaSemValorDaManilha_RetornaFalse(Valor vira, Valor valorCarta, Naipe naipeCarta)
    {
        // Arrange
        Carta cartaVira = new(Naipe.Paus, vira);
        Carta cartaComum = new(naipeCarta, valorCarta);

        // Act
        ComparadorDeCartas comparador = new(cartaVira);

        // Assert
        Assert.That(comparador.EhManilha(cartaComum), Is.False);
    }

    [TestCase(Valor.Tres, Valor.Dois)]
    [TestCase(Valor.Dois, Valor.As)]
    [TestCase(Valor.As, Valor.Rei)]
    [TestCase(Valor.Rei, Valor.Valete)]
    [TestCase(Valor.Valete, Valor.Dama)]
    [TestCase(Valor.Dama, Valor.Sete)]
    [TestCase(Valor.Sete, Valor.Seis)]
    [TestCase(Valor.Seis, Valor.Cinco)]
    public void RN02_ComunsDeValoresDiferentes_MaisForteGanha(Valor maisForte, Valor maisFraca)
    {
        // Arrange
        ComparadorDeCartas comparador = ComViraParaComuns();
        Carta cartaForte = new(Naipe.Ouros, maisForte);
        Carta cartaFraca = new(Naipe.Paus, maisFraca);

        // Act
        int resultado = comparador.Compare(cartaForte, cartaFraca);

        // Assert
        Assert.That(resultado, Is.GreaterThan(0));
    }

    [Test]
    public void RN02_ComunsDeMesmoValor_Empatam()
    {
        // Arrange
        ComparadorDeCartas comparador = ComViraParaComuns();
        Carta reiDeOuros = new(Naipe.Ouros, Valor.Rei);
        Carta reiDePaus = new(Naipe.Paus, Valor.Rei);

        // Act
        int resultado = comparador.Compare(reiDeOuros, reiDePaus);

        // Assert
        Assert.That(resultado, Is.Zero);
    }

    [Test]
    public void RN05_ManilhaMaisFracaContraComumMaisForte_ManilhaGanha()
    {
        // Arrange
        ComparadorDeCartas comparador = ComVira(Valor.Rei);
        Carta asDeOuros = new(Naipe.Ouros, Valor.As);
        Carta tresDePaus = new(Naipe.Paus, Valor.Tres);

        // Act
        int resultado = comparador.Compare(asDeOuros, tresDePaus);

        // Assert
        Assert.That(resultado, Is.GreaterThan(0));
    }

    [Test]
    public void RN05_Manilhas_OrdenadasPorNaipe()
    {
        // Arrange
        ComparadorDeCartas comparador = ComVira(Valor.Sete);
        List<Carta> manilhas = new()
        {
            new Carta(Naipe.Copas, Valor.Dama),
            new Carta(Naipe.Paus, Valor.Dama),
            new Carta(Naipe.Ouros, Valor.Dama),
            new Carta(Naipe.Espadas, Valor.Dama)
        };
        List<Carta> ordemEsperada = new()
        {
            new Carta(Naipe.Ouros, Valor.Dama),
            new Carta(Naipe.Espadas, Valor.Dama),
            new Carta(Naipe.Copas, Valor.Dama),
            new Carta(Naipe.Paus, Valor.Dama)
        };

        // Act
        manilhas.Sort(comparador);

        // Assert
        Assert.That(manilhas, Is.EqualTo(ordemEsperada));
    }

    [Test]
    public void Compare_MesmaCarta_RetornaZero()
    {
        // Arrange
        ComparadorDeCartas comparador = ComViraParaComuns();
        Carta carta = new(Naipe.Copas, Valor.Valete);

        // Act
        int resultado = comparador.Compare(carta, carta);

        // Assert
        Assert.That(resultado, Is.Zero);
    }

    [Test]
    public void Compare_ArgumentosInvertidos_InverteOSinal()
    {
        // Arrange
        ComparadorDeCartas comparador = ComViraParaComuns();
        Carta tresDeCopas = new(Naipe.Copas, Valor.Tres);
        Carta cincoDeEspadas = new(Naipe.Espadas, Valor.Cinco);

        // Act
        int direto = comparador.Compare(tresDeCopas, cincoDeEspadas);
        int invertido = comparador.Compare(cincoDeEspadas, tresDeCopas);

        // Assert
        Assert.That(direto, Is.GreaterThan(0));
        Assert.That(invertido, Is.LessThan(0));
    }

    [Test]
    public void Construtor_ViraNula_LancaArgumentNullException()
    {
        // Act && Assert
        ArgumentNullException excecao = Assert.Throws<ArgumentNullException>(() => new ComparadorDeCartas(null));
        Assert.That(excecao.ParamName, Is.EqualTo("vira"));
    }

    [Test]
    public void Forca_CartaNula_LancaArgumentNullException()
    {
        // Arrange
        ComparadorDeCartas comparador = ComViraParaComuns();

        // Act && Assert
        ArgumentNullException excecao = Assert.Throws<ArgumentNullException>(() => comparador.Forca(null));
        Assert.That(excecao.ParamName, Is.EqualTo("carta"));
    }

    [Test]
    public void Compare_PrimeiraCartaNula_LancaArgumentNullException()
    {
        // Arrange
        ComparadorDeCartas comparador = ComViraParaComuns();
        Carta carta = new(Naipe.Ouros, Valor.Rei);

        // Act && Assert
        ArgumentNullException excecao = Assert.Throws<ArgumentNullException>(() => comparador.Compare(null, carta));
        Assert.That(excecao.ParamName, Is.EqualTo("x"));
    }

    [Test]
    public void Compare_SegundaCartaNula_LancaArgumentNullException()
    {
        // Arrange
        ComparadorDeCartas comparador = ComViraParaComuns();
        Carta carta = new(Naipe.Ouros, Valor.Rei);

        // Act && Assert
        ArgumentNullException excecao = Assert.Throws<ArgumentNullException>(() => comparador.Compare(carta, null));
        Assert.That(excecao.ParamName, Is.EqualTo("y"));
    }
}
