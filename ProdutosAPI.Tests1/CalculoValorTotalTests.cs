using System;
using System.Collections.Generic;
using System.Text;

namespace ProdutosAPI.Tests
{
    public class CalculoValorTotalTests
    {
        [Fact]
        public void DeveCalcularValorTotalCorretamente()
        {
            // Arrange
            decimal precoUnitario = 800.00m;
            int quantidade = 2;

            // Act
            decimal resultado = quantidade * precoUnitario;

            // Assert
            Assert.Equal(1600.00m, resultado);
        }
    }
}
