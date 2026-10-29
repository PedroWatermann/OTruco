using System;
using OTruco.Core;
using NUnit.Framework;
using System.Collections.Generic;

public class BaralhoTests
{
    private static List<Carta> ComprarTodas(Baralho baralho)
    {
        List<Carta> cartasCompradas = new();
        while (baralho.Restantes > 0)
            cartasCompradas.Add(baralho.Comprar());
        return cartasCompradas;
    }

    [Test]
    public void RN01_BaralhoNovo_Contem40Cartas()
    {
        // Arrange && Act
        Baralho baralho = new(new Random(123));

        // Assert
        Assert.That(baralho.Restantes, Is.EqualTo(40));
    }
        
    [Test]
    public void Comprar_UmaCarta_DiminuiRestantesEmUm()
    {
        // Arrange
        Baralho baralho = new(new Random(132));
        
        // Act
        baralho.Comprar();
        
        // Assert
        Assert.That(baralho.Restantes, Is.EqualTo(39));
    }
    
    [Test]
    public void Comprar_UmaCarta_DevolveCartaNaoNula()
    {
        // Arrange
        Baralho baralho = new(new Random(213));
        
        // Act
        Carta carta = baralho.Comprar();
        
        // Assert
        Assert.That(carta, Is.Not.Null);
    }
    
    [Test]
    public void RN01_ComprarTodas_NenhumaCartaRepetida()
    {
        // Arrange
        Baralho baralho = new(new Random(231));
        List<Carta> cartasCompradas;

        // Act
        cartasCompradas = ComprarTodas(baralho);

        // Assert
        Assert.That(cartasCompradas, Is.Unique);
    }
    
    [Test]
    public void RN01_ComprarTodas_ContemTodasAsCombinacoes()
    {
        // Arrange
        Baralho baralho = new(new Random(321));
        List<Carta> cartasCompradas;

        // Act
        cartasCompradas = ComprarTodas(baralho);

        // Assert
        foreach (Naipe naipe in Enum.GetValues(typeof(Naipe)))
            foreach (Valor valor in Enum.GetValues(typeof(Valor)))
                Assert.That(cartasCompradas, Does.Contain(new Carta(naipe, valor)));
    }

    [Test]
    public void Comprar_BaralhoVazio_LancaInvalidOperationException()
    {
        // Arrange
        Baralho baralho = new(new Random(312));
        for (int i = 0; i < 40; i++)
            baralho.Comprar();

        // Act && Assert
        Assert.Throws<InvalidOperationException>(() => baralho.Comprar());
    }
    
    [Test]
    public void Construtor_MesmaSemente_MesmaOrdem()
    {
        // Arrange
        Baralho baralho1 = new(new Random(123));
        Baralho baralho2 = new(new Random(123));

        // Act
        List<Carta> cartas1 = ComprarTodas(baralho1);
        List<Carta> cartas2 = ComprarTodas(baralho2);

        // Assert
        Assert.That(cartas1, Is.EqualTo(cartas2));
    }
    
    [Test]
    public void Construtor_SementesDiferentes_OrdensDiferentes()
    {
        // Arrange
        Baralho baralho1 = new(new Random(123));
        Baralho baralho2 = new(new Random(321));

        // Act 
        List<Carta> cartas1 = ComprarTodas(baralho1);
        List<Carta> cartas2 = ComprarTodas(baralho2);

        // Assert
        Assert.That(cartas1, Is.Not.EqualTo(cartas2));
    }
}
