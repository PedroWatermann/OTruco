using System;

namespace OTruco.Core
{
    public sealed class Carta : IEquatable<Carta>
    {
        public Naipe Naipe { get; }
        public Valor Valor { get; }

        public Carta(Naipe naipe, Valor valor)
        {
            Naipe = naipe;
            Valor = valor;
        }

        public override string ToString() => $"{this.Valor} de {this.Naipe}";

        public bool Equals(Carta other) => other is not null && ((ReferenceEquals(this, other)) || (Naipe == other.Naipe && Valor == other.Valor));

        public override bool Equals(object obj) => Equals(obj as Carta);

        public override int GetHashCode() => HashCode.Combine(Naipe, Valor);

        public static bool operator ==(Carta left, Carta right) => Equals(left, right);
        public static bool operator !=(Carta left, Carta right) => !Equals(left, right);
    }
}
