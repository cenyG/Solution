namespace Matrix
{
    public class Matrix<T> where T: struct
    {
        public readonly T[][] Data;

        public Matrix(T[][] data)
        {
            Data = data;
        }

        public Matrix<T> Mul(Matrix<T> mx)
        {
            var m1 = Data;
            var m2 = mx.Data;

            if (m1[0].Length != m2.Length)
            {
                throw new Exception("Bad input matrices");
            }

            var resIMax = m1.Length;
            var resJMax = m2[0].Length;

            var res = new T[resIMax][];

            for (var i = 0; i < resIMax; i++)
            {
                res[i] = new T[resJMax];
                for (var j = 0; j < resJMax; j++)
                {
                    res[i][j] = CalcResValue(m1, m2, i, j);
                }
            }

            return new Matrix<T>(res);
        }

        public override string ToString()
        {
            return Data.Select(
                    ints =>
                        ints.Select(
                            i => i.ToString()
                        ).Aggregate((acc, cur) => $"{acc}, {cur}"))
                .Aggregate((acc, cur) => $"{acc}{Environment.NewLine}{cur}") + Environment.NewLine;
        }

        private T CalcResValue(T[][] a, T[][] b, int i, int j) 
        {
            
            var res = new T();
            for (var k = 0; k < b.Length; k++)
            {
                var tmp = Generic.Mul(a[i][k], b[k][j]);
                res = Generic.Add(res, tmp);
            }

            return res;
        }
    }
}