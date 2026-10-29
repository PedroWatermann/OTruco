using OTruco.Core;
using NUnit.Framework;

public class CartaTests
{
    [Test]
    public void Construtor_NaipeEValor_PreenchePropriedades()
    {
        // Arrange && Act
        Carta carta = new(Naipe.Paus, Valor.As);

        // Assert
        Assert.That(carta.Naipe, Is.EqualTo(Naipe.Paus));
        Assert.That(carta.Valor, Is.EqualTo(Valor.As));
    }
    
    [Test]
    public void Equals_MesmoNaipeEValor_SaoIguais()
    {
        // Arrange
        Carta carta1 = new(Naipe.Copas, Valor.Dois);
        Carta carta2 = new(Naipe.Copas, Valor.Dois);
        
        // Act && Assert
        Assert.That(carta1.Equals(carta2));
    }
    
    [Test]
    public void OperadorIgual_MesmoNaipeEValor_RetornaTrue()
    {
        // Arrange
        Carta carta1 = new(Naipe.Espadas, Valor.Tres);
        Carta carta2 = new(Naipe.Espadas, Valor.Tres);
        
        // Act && Assert
        Assert.That(carta1 == carta2, Is.True);
    }
    
    [Test]
    public void GetHashCode_CartasIguais_MesmoHash()
    {
        // Arrange
        Carta carta1 = new(Naipe.Ouros, Valor.Quatro);
        Carta carta2 = new(Naipe.Ouros, Valor.Quatro);

        // Act && Assert
        Assert.That(carta1.GetHashCode(), Is.EqualTo(carta2.GetHashCode()));
    }
    
    [Test]
    public void Equals_NaipeDiferente_NaoSaoIguais()
    {
        // Arrange
        Carta carta1 = new(Naipe.Paus, Valor.Cinco);
        Carta carta2 = new(Naipe.Copas, Valor.Cinco);

        // Act && Assert
        Assert.That(carta1.Equals(carta2), Is.False);
    }
    
    [Test]
    public void Equals_ValorDiferente_NaoSaoIguais()
    {
        // Arrange
        Carta carta1 = new(Naipe.Espadas, Valor.Seis);
        Carta carta2 = new(Naipe.Espadas, Valor.Sete);

        // Act && Assert
        Assert.That(carta1.Equals(carta2), Is.False);
    }
    
    [Test]
    public void OperadorDiferente_CartasDiferentes_RetornaTrue()
    {
        // Arrange
        Carta carta1 = new(Naipe.Ouros, Valor.Dama);
        Carta carta2 = new(Naipe.Copas, Valor.Dama);

        // Act && Assert
        Assert.That(carta1 != carta2, Is.True);
    }
    
    [Test]
    public void Equals_Null_RetornaFalse()
    {
        // Arrange
        Carta carta = new(Naipe.Paus, Valor.Valete);

        // Act && Assert
        Assert.That(carta.Equals(null), Is.False);
    }

    [Test]
    public void OperadorIgual_ComparadoComNull_RetornaFalse()
    {
        // Arrange
        Carta carta = new(Naipe.Copas, Valor.Rei);

        // Act && Assert
        Assert.That(carta == null, Is.False);
        Assert.That(null == carta, Is.False);
    }
    
    [Test]
    public void Equals_ObjetoDeOutroTipo_RetornaFalse()
    {
        // Arrange
        Carta carta = new(Naipe.Espadas, Valor.As);
        object outroObjeto = new();

        // Act && Assert
        Assert.That(carta.Equals(outroObjeto), Is.False);
    }
    
    [Test]
    public void ToString_SeteDeOuros_RetornaTextoLegivel()
    {
        // Arrange
        Carta carta = new(Naipe.Ouros, Valor.Sete);
        // Act
        string resultado = carta.ToString();
        // Assert
        Assert.That(resultado, Is.EqualTo("Sete de Ouros"));
    }
}
