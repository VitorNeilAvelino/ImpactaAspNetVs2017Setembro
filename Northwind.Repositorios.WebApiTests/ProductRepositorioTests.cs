using Microsoft.VisualStudio.TestTools.UnitTesting;
using Northwind.Repositorios.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Repositorios.WebApi.Tests
{
    [TestClass()]
    public class ProductRepositorioTests
    {
        private ProductRepositorio repositorio = new ProductRepositorio();

        [TestMethod()]
        public void PostTest()
        {
            var produto = new ProductViewModel();
            produto.ProductName = "Frigerante Diet";
            produto.UnitsInStock = 27;
            produto.UnitPrice = 17.27m;
            produto.CategoryID = 1;
            produto.SupplierID = 1;

            var response = repositorio.Post(produto).Result;

            Console.WriteLine(response.ProductID);
        }

        [TestMethod()]
        public void PutTest()
        {
            var produto = repositorio.Get(78).Result;
            produto.ProductName = "Café com leite";

            repositorio.Put(produto).Wait();

            produto = repositorio.Get(78).Result;

            Assert.AreEqual(produto.ProductName, "Café com leite");
        }

        [TestMethod]
        public void GetAllTest()
        {
            var produtos = repositorio.Get().Result;

            foreach (var produto in produtos)
            {
                Console.WriteLine($"{produto.ProductID} - {produto.ProductName}");
            }   
        }

        [TestMethod]
        public void DeleteTest()
        {
            repositorio.Delete(78).Wait();

            var produto = repositorio.Get(78).Result;

            Assert.IsNull(produto);
        }
    }
}