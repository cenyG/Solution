
namespace Matrix
{
    internal class Program
    {
        private static readonly string FilesPath = $"{Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName}\\test_data";
        
        public static void Main(string[] args)
        {
            
            var rw = new ReaderWriter<int>();
            var m1 = rw.ReadFile($"{FilesPath}\\matrix1.txt");
            var m2 = rw.ReadFile($"{FilesPath}\\matrix2.txt");
            
            var mm1 = new Matrix<int>(m1);
            var mm2 = new Matrix<int>(m2);
            
            Console.WriteLine("First matrix:");
            Console.WriteLine(mm1);
            Console.WriteLine("Second matrix:");
            Console.WriteLine(mm2);

            var mm3 = mm1.Mul(mm2);

            Console.WriteLine("Result matrix:");
            Console.WriteLine(mm3);
            
            rw.WriteFile(mm3.Data, $"{FilesPath}\\resMatrix.txt");
        }
    }
}