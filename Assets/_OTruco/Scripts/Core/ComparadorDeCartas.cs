using System;
using System.Collections.Generic;

namespace OTruco.Core
{
    public sealed class ComparadorDeCartas : IComparer<Carta>
    {
        public Valor ValorDaManilha { get; }

        public ComparadorDeCartas(Carta vira)
        {
            if (vira is null)
                throw new ArgumentNullException(nameof(vira));
            ValorDaManilha = vira.Valor == Valor.Tres ? Valor.Quatro : vira.Valor + 1;
        }

        public bool EhManilha(Carta carta)
        {
            if (carta is null)
                throw new ArgumentNullException(nameof(carta));
            return carta.Valor == ValorDaManilha;
        }

        public int Forca(Carta carta)
        {
            if (carta is null)
                throw new ArgumentNullException(nameof(carta));
            return EhManilha(carta) ? 100 + (int)carta.Naipe : (int)carta.Valor;
        }

        public int Compare(Carta x, Carta y)
        {
            if (x is null)
                throw new ArgumentNullException(nameof(x));
            if (y is null)
                throw new ArgumentNullException(nameof(y));
            return Forca(x).CompareTo(Forca(y));
        }
    }
}
