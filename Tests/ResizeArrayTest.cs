using Matrix;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class ResizeArrayTest
    {
        [Test]
        public void Create()
        {
            var resizeArray = new ResizeArray<int>();
            Assert.AreEqual(resizeArray.Length, 0);
        }
        
        [Test]
        public void CreateWithElems()
        {
            var resizeArray = new ResizeArray<int>(new [] {1,2,3,4});
            Assert.AreEqual(resizeArray.Length, 4);
        }
        
        [Test]
        public void Get()
        {
            var resizeArray = new ResizeArray<int>(new [] {1,2,3,4});
            Assert.AreEqual(resizeArray.Get(0), 1);
        }
        
        [Test]
        public void Set()
        {
            var resizeArray = new ResizeArray<int>(new [] {1,2,3,4});
            resizeArray.Set(0, 1000);
            Assert.AreEqual(resizeArray.Get(0), 1000);
        }
        
        [Test]
        public void Add()
        {
            var resizeArray = new ResizeArray<int>();
            resizeArray.Add(5);
            
            Assert.AreEqual(resizeArray.Get(0), 5);
            Assert.AreEqual(resizeArray.Length, 1);
        }
    }
}