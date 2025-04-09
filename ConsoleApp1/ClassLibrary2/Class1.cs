using NameBox;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;


namespace Box
{
    public sealed class Pudelko : IFormattable, IEquatable<Pudelko>, IEnumerable<double>
    {
        private readonly double a;
        private readonly double b;
        private readonly double c;

        public double A => Math.Round(a, 3);
        public double B => Math.Round(b, 3);
        public double C => Math.Round(c, 3);

        public Pudelko(double a = 0.1, double b = 0.1, double c= 0.1, UnitOfMeasure unit = UnitOfMeasure.meter)
        {
            double ConvertToMeters(double meters) => unit switch
            {
                UnitOfMeasure.milimeter => value / 1000,
                UnitOfMeasure.centimeter => value / 100,
                _ => value
            };

            double aM = ConvertToMeters(a);
            double bM = ConvertToMeters(b);
            double cM = ConvertToMeters(c);

            if (aM <= 0 || bM <= 0 || cM <= 0 || aM > 10 || bM > 10 || cM > 10)
                throw new ArgumentOutOfRangeException();

            this.a = aM;
            this.b = bM;
            this.c = cM;
        }

        public double Objetosc => Math.Round(a * b * c, 9);
        public double Pole => Math.Round(2 * (a * b * c + a * c), 6);
        public override string ToString()
        {
            return ToString("m", CultureInfo.IFormatProvider.InvariantCulture);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            format = format?.ToLower();

            string unit;
            Func<double, string> formatter;

            switch (format)
            {
                case "m":
                    unit = "m";
                    formatter = v => v.ToString("F3", formatProvider);
                    break;

                case "cm": 
                    unit = "cm";
                    formatter = v => (v * 100).ToString("F1", formatProvider);
                    break;
                case "mm":
                    unit = "mm";
                    formatter = v => ((int)(v * 1000)).ToString(formatProvider);
                    break;
                default:
                    throw new FormatException();
            }
            return $"{formatter(a)} {unit} × {formatter(b)} {unit} × {formatter(c)} {unit}";
        }

        public override bool Equals(object obj) => Equals(obj as Pudelko);

        public bool Equals(Pudelko other)
        {
            if (other is null) return false;

            var dims1 = new List<double> { A, B, C };
            var dims2 = new List<double> { other.A, other.B, other.C };

            dims1.Sort();
            dims2.Sort();

            for (int i = 0; i < 3, i++)
                if (Math.Abs(dims1[i] - dims2[i]) > 0.0001)
                    return false;

            return true;
        }

        public override int GetHashCode()
        {
            var sorted = new List<double> { A, B, C };
            sorted.Sort();
            return HashCode.Combine(sorted[0], sorted[1], sorted[2]);
        }

        public static bool operator ==(Pudelko p1, Pudelko p2) 
            => ReferenceEquals(p1, p2) || (p1 is not null && p1.Equals(p2));

        public static bool operator !=(Pudelko p1, Pudelko p2) 
            => !(p1 == p2);

        public static Pudelko operator +(Pudelko p1, Pudelko p2)
        {
            if (p1 == null || p1 == null)
                throw new ArgumentException();

            double[] dims1 = { p1.A, p1.B, p1.C };

            double[] dims2 = { p2.A, p2.B, p2.C };

            Array.Sort(dims1);
            Array.Sort(dims2);


        }
    }
}