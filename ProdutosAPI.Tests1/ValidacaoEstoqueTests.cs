using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ProdutosAPI.Tests
{
    public class ValidacaoEstoqueTests
    {
        [Fact]
        public void DeveIndicarEstoqueInsuficienteQuandoQuantidadeMaiorQueEstoque()
        {
            // Arrange
            int estoqueDisponivel = 5;
            int quantidadeSolicitada = 10;

            // Act
            bool estoqueInsuficiente = estoqueDisponivel < quantidadeSolicitada;

            // Assert
            Assert.True(estoqueInsuficiente);
        }

        [Fact]
        public void NaoDeveIndicarEstoqueInsuficienteQuandoQuantidadeMenorOuIgualAoEstoqu()
        {
            // Arrange
            int estoqueDisponivel = 10;
            int quantidadeSolicitada = 5;

            // Act
            bool estoqueInsuficiente = estoqueDisponivel < quantidadeSolicitada;

            // Assert
            Assert.False(estoqueInsuficiente);
        }
    }
}
