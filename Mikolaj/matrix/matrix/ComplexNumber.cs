namespace matrix
{
    public class ComplexNumber : IComplexNumber
    {
        public int Real { get; set; }
        public int Imaginary { get; set; }

        public ComplexNumber(int real, int imaginary) {
            Real = real;
            Imaginary = imaginary;
        }
        public static ComplexNumber operator +(ComplexNumber z1, ComplexNumber z2)
        {
            int z1_real = z1.Real;
            int z1_imaginary = z1.Imaginary;

            int z2_real = z2.Real;
            int z2_imaginary = z2.Imaginary;

            ComplexNumber z3 = new ComplexNumber((z1_real + z2_real), (z1_imaginary + z2_imaginary));
            return z3;
        }

        public static ComplexNumber operator *(ComplexNumber z1, ComplexNumber z2)
        {
            int z1_real = z1.Real;
            int z1_imaginary = z1.Imaginary;

            int z2_real = z2.Real;
            int z2_imaginary = z2.Imaginary;

            ComplexNumber z3 = new ComplexNumber(0, 0);

            z3.Real = (z1.Real * z2.Real) + ((z1.Imaginary * z2.Imaginary)*-1);
            z3.Imaginary = (z1.Real * z2.Imaginary) + (z2.Real * z1.Imaginary);
            return z3; 
        }

        public override string ToString()
        {
            return $"{Real} {(Imaginary >= 0 ? '+' : '-')} {Math.Abs(Imaginary)}i";
        }
    }
}
