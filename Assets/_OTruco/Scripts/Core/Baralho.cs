using System;
using System.Collections.Generic;

namespace OTruco.Core
{
    public sealed class Baralho
    {
        public int Restantes => _cartas.Count;

        private readonly Random _gerador;
        private readonly List<Carta> _cartas = new();

        public Baralho(Random random)
        {
            _gerador = random;
            MontarBaralho();
            Embaralhar();
        }

        private void MontarBaralho()
        {
            foreach (Naipe naipe in Enum.GetValues(typeof(Naipe))) 
                foreach (Valor valor in Enum.GetValues(typeof(Valor))) 
                    _cartas.Add(new Carta(naipe, valor));
        }

        public void Embaralhar()
        {
            for (int i = 0; i < _cartas.Count; i++)
            {
                int j = _gerador.Next(i, _cartas.Count);
                (_cartas[i], _cartas[j]) = (_cartas[j], _cartas[i]);
            }
        }

        public Carta Comprar()
        {
            if (_cartas.Count == 0)
                throw new InvalidOperationException("O baralho está vazio.");

            Carta carta = _cartas[^1];
            _cartas.RemoveAt(_cartas.Count - 1);
            return carta;
        }
    }
}
