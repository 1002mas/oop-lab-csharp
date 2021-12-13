namespace ExtensionMethods
{
    using System;

    /// <inheritdoc cref="IComplex"/>
    public class Complex : IComplex
    {
        private readonly double im;
        private readonly double re;

        /// <summary>
        /// Initializes a new instance of the <see cref="Complex"/> class.
        /// </summary>
        /// <param name="re">the real part.</param>
        /// <param name="im">the imaginary part.</param>
        public Complex(double re, double im)
        {
            this.re = re;
            this.im = im;
        }

        /// <inheritdoc cref="IComplex.Real"/>
        public double Real
        {
            get => re;
        }

        /// <inheritdoc cref="IComplex.Imaginary"/>
        public double Imaginary
        {
            get => im;
        }

        /// <inheritdoc cref="IComplex.Modulus"/>
        public double Modulus
        {
            get => Math.Sqrt(Math.Pow(re, 2) + Math.Pow(im, 2));
        }

        /// <inheritdoc cref="IComplex.Phase"/>
        public double Phase
        {
            get => Math.Atan2(im, re);
        }

        /// <inheritdoc cref="IComplex.ToString"/>
        public override string ToString()
        {
            return $"{re}+i{im}";
        }

        /// <inheritdoc cref="IEquatable{T}.Equals(T)"/>
        public bool Equals(IComplex other)
        {
            return re.Equals(other?.Real) && im.Equals(other?.Imaginary);
        }

        /// <inheritdoc cref="object.Equals(object?)"/>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Complex) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(re, im);
        }
    }
}
