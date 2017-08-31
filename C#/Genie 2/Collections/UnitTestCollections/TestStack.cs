using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Collections;
using Biz;

namespace UnitTestCollections
{
    [TestClass]
    public class TestStack
    {
        Stack<Client> cible;

        [TestInitialize]
        public void Initialization()
        {
            cible = new Stack<Client>();
        }

        [TestMethod]
        public void Count0ALaCreation()
        {
            Assert.AreEqual(0, cible.Count);
        }

        [TestMethod]
        public void CountAugmenteApresPush()
        {
            cible.Push(new Client());
            cible.Push(new Client());
            int compte = cible.Count;

            // Act
            cible.Push(new Client());

            // Assert
            Assert.AreEqual(compte + 1, cible.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PopVideException()
        {
            // Act
            cible.Pop();
        }

        [TestMethod]
        public void CountDecrementeApresPop()
        {
            cible.Push(new Client());
            int compte = cible.Count;

            // Act
            cible.Pop();

            // Assert
            Assert.AreEqual(compte - 1, cible.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PeekVideException()
        {
            // Assert
            cible.Peek();
        }

        [TestMethod]
        public void PopOrdreCorrecte()
        {
            Client[] clients = { new Client(), new Client() };
            cible.Push(clients[0]);
            cible.Push(clients[1]);

            // Act
            var client2 = cible.Pop();
            var client1 = cible.Pop();

            // Assert
            Assert.AreEqual(clients[0], client1);
            Assert.AreEqual(clients[1], client2);
        }

        [TestMethod]
        public void IsEmptyVide()
        {
            // Assert
            Assert.AreEqual(true, cible.IsEmpty);
        }

        [TestMethod]
        public void IsEmptyAvecElement()
        {
            // Act
            cible.Push(new Client());

            // Assert
            Assert.AreEqual(false, cible.IsEmpty);
        }
    }
}
