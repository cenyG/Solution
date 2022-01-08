using System;
using System.IO;
using Matrix;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class MatrixTest
    {
        private readonly string? _testDir = $"{Path.GetDirectoryName(TestContext.CurrentContext.TestDirectory)}\\..\\..\\test_data";
        
        [Test]
        public void IntMatrix()
        {
            var rw = new ReaderWriter<int>();
            var m1 = rw.ReadFile($"{_testDir}\\matrix1.txt");
            var m2 = rw.ReadFile($"{_testDir}\\matrix2.txt");
            
            var mm1 = new Matrix<int>(m1);
            var mm2 = new Matrix<int>(m2);
            
            Console.WriteLine("First matrix:");
            Console.WriteLine(mm1);
            Console.WriteLine("Second matrix:");
            Console.WriteLine(mm2);

            var mm3 = mm1.Mul(mm2);

            Console.WriteLine("Result matrix:");
            Console.WriteLine(mm3);
            
            var expected = new[] {new[]{3, 2340}, new[]{0, 1000}};
            Assert.AreEqual(expected, mm3.Data);

            var resFileDir = $"{_testDir}\\resMatrix_int.txt";
            rw.WriteFile(mm3.Data, resFileDir);

            Assert.True(File.Exists(resFileDir));

            var resFileContent = File.ReadAllLines(resFileDir);
            Assert.AreEqual("3;2340", resFileContent[0]);
            Assert.AreEqual("0;1000", resFileContent[1]);
            
            File.Delete(resFileDir);
        }
        
        [Test]
        public void DoubleMatrix()
        {
            var rw = new ReaderWriter<double>();
            var m1 = rw.ReadFile($"{_testDir}\\matrix_double1.txt");
            var m2 = rw.ReadFile($"{_testDir}\\matrix_double2.txt");
            
            // convert to MyInt

            var mm1 = new Matrix<double>(m1);
            var mm2 = new Matrix<double>(m2);
            
            Console.WriteLine("First matrix:");
            Console.WriteLine(mm1);
            Console.WriteLine("Second matrix:");
            Console.WriteLine(mm2);

            var mm3 = mm1.Mul(mm2);

            Console.WriteLine("Result matrix:");
            Console.WriteLine(mm3);
            

            var expected = new[] {new[]{43.5, 75.75}, new[]{2.0, 0.5}};
            Assert.AreEqual(expected, mm3.Data);

            var resFileDir = $"{_testDir}\\resMatrix_double.txt";
            rw.WriteFile(mm3.Data, resFileDir);

            Assert.True(File.Exists(resFileDir));

            var resFileContent = File.ReadAllLines(resFileDir);
            Assert.AreEqual("43.5;75.75", resFileContent[0]);
            Assert.AreEqual("2;0.5", resFileContent[1]);
            
            File.Delete(resFileDir);
        }

        [Test]
        public void MulMatrix()
        {
            var a = new bool[4][]
            {
                new bool[4]
                {
                    false, true, false, true
                },
                new bool[4]
                {
                    false, true, false, true
                },
                new bool[4]
                {
                    false, true, false, true
                },
                new bool[4]
                {
                    false, true, false, true
                },
            };
            
            var b = new bool[4][]
            {
                new bool[4]
                {
                    false, true, false, true
                },
                new bool[4]
                {
                    false, true, false, true
                },
                new bool[4]
                {
                    false, true, false, true
                },
                new bool[4]
                {
                    false, true, false, true
                },
            };
            
            var mx1 = new Matrix<bool>(a);
            var mx2 = new Matrix<bool>(b);

            var mx3 = mx1.Mul(mx2);
            
            Console.WriteLine(mx3);
        }
    }
}