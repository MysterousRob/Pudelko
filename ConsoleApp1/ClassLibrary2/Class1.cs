using NameBox;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;


namespace Box
{
    public sealed class Box : IFormattable, IEquatable<Box>, IEnumerable<double>
    {
        private readonly double a;
        private readonly double b;
        private readonly double c;

        public double A => Math.Round(a, 3);
        public double B => Math.Round(b, 3);
        public double C => Math.Round(c, 3);

        public Box(double a = 0.1, double b = 0.1, double c= 0.1, UnitOfMeasure unit = UnitOfMeasure.meter)
        {
            double ConvertToMeters(double meters) => unit switch
            {

            }
        }
    }
}