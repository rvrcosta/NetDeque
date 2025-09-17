using FluentAssertions;
using FluentAssertions.Execution;

namespace NetDeque.Test
{
    public class DequeTest
    {
        [Fact]
        public void NewDeque_ShouldBeEmpty()
        {
            //Arrange
            var deque = new Deque<int>();

            //Assert
            //Assert.True(deque.IsEmpty);
            //Assert.Equal(0, deque.Count);
            
            using(new AssertionScope())
            {
                deque.IsEmpty.Should().BeTrue();
                deque.Count.Should().Be(0);
            }            
        }

        [Fact]
        public void AddBeg_ShouldIncreaseCount_AndAddToFront()
        {
            //Arrange
            var deque = CreateDequeWith(10);

            //Assert
            //Assert.False(deque.IsEmpty);
            //Assert.Equal(1, deque.Count);            
            //Assert.Equal(10, deque.PeekBeg());
            //Assert.Equal(10, deque.PeekEnd());

            using (new AssertionScope())
            {
                deque.IsEmpty.Should().BeFalse();
                deque.Count.Should().Be(1);
                deque.PeekBeg().Should().Be(10);
                deque.PeekEnd().Should().Be(10);
            }
        }

        [Fact]
        public void AddEndSameValue_ShouldIncreaseCount_AndAddToEnd()
        {
            //Arrange
            var deque = CreateDequeWith(20);
            
            //Act
            deque.AddEnd(20);

            //Assert
            //Assert.False(deque.IsEmpty);            
            //Assert.Equal(20, deque.PeekBeg());
            //Assert.Equal(20, deque.PeekEnd());
            //Assert.Equal(2, deque.Count);

            using (new AssertionScope())
            {
                deque.IsEmpty.Should().BeFalse();
                deque.PeekBeg().Should().Be(20).And.Be(deque.PeekEnd());
                deque.Count.Should().Be(2);
            }
        }

        [Fact]
        public void AddBeg_And_AddEnd_ShouldMaintainOrder()
        {
            //Arrange
            var deque = CreateDequeWith(30,10,20);

            //Act
            deque.AddBeg(15);
            deque.AddEnd(5);

            //Assert
            //Assert.Equal(15, deque.PeekBeg());
            //Assert.Equal(5, deque.PeekEnd());
            //Assert.Equal(5, deque.Count);

            using (new AssertionScope())
            {
                deque.PeekBeg().Should().Be(15);
                deque.PeekEnd().Should().Be(5);
                deque.Count.Should().Be(5);
            }
        }

        [Fact]
        public void RemBeg_ShouldRemoveAndReturnFirst()
        {
            //Arrange
            var deque = CreateDequeWith(30, 10, 20);

            //Act
            deque.AddEnd(20);
            deque.AddEnd(10);

            var removed = deque.RemBeg();

            //Assert
            //Assert.Equal(30, removed);
            //Assert.Equal(4, deque.Count);
            //Assert.Equal(10, deque.PeekBeg());

            using (new AssertionScope())
            {
                removed.Should().Be(30);
                deque.Count.Should().Be(4);
                deque.PeekBeg().Should().Be(10);
            }

        }

        [Fact]
        public void RemEnd_ShouldRemoveAndReturnLast()
        {
            //Arrange
            var deque = new Deque<int>();

            //Act
            deque.AddEnd(1);
            deque.AddEnd(2);
            var removed = deque.RemEnd();

            //Assert
            //Assert.Equal(2, removed);
            //Assert.Equal(1, deque.Count);
            //Assert.Equal(1, deque.PeekEnd());

            using (new AssertionScope())
            {
                removed.Should().Be(2);
                deque.Count.Should().Be(1);
                deque.PeekEnd().Should().Be(1);
            }

        }

        [Fact]
        public void PeekBeg_And_PeekEnd_ShouldNotRemove()
        {
            //Arrange
            var deque = new Deque<int>();

            //Act
            deque.AddEnd(10);
            deque.AddEnd(20);

            //Assert
            //Assert.Equal(10, deque.PeekBeg());
            //Assert.Equal(20, deque.PeekEnd());
            //Assert.Equal(2, deque.Count);

            using (new AssertionScope())
            {
                deque.PeekBeg().Should().Be(10);
                deque.PeekEnd().Should().Be(20);
                deque.Count.Should().Be(2);
            }

        }

        [Fact]
        public void RemBeg_OnEmptyDeque_ShouldThrow()
        {
            //Arrange
            var deque = new Deque<int>();

            //Act
            var act = () => deque.RemBeg();

            //Assert
            //Assert.Throws<InvalidOperationException>(() => deque.RemBeg());

            act.Should().Throw<InvalidOperationException>();

        }

        [Fact]
        public void RemEnd_OnEmptyDeque_ShouldThrow()
        {
            //Arrange
            var deque = new Deque<int>();

            //Act
            var act = () => deque.RemEnd();

            //Assert
            //Assert.Throws<InvalidOperationException>(() => deque.RemEnd());

            act.Should().Throw<InvalidOperationException>();

        }

        [Fact]
        public void PeekBeg_OnEmptyDeque_ShouldThrow()
        {
            //Arrange
            var deque = new Deque<int>();

            //Act
            var act = () => deque.PeekBeg();

            //Assert
            //Assert.Throws<InvalidOperationException>(() => deque.PeekBeg());

            act.Should().Throw<InvalidOperationException>();

        }

        [Fact]
        public void PeekEnd_OnEmptyDeque_ShouldThrow()
        {
            //Arrange
            var deque = new Deque<int>();

            //Act
            var act = () => deque.PeekEnd();

            //Assert
            //Assert.Throws<InvalidOperationException>(() => deque.PeekEnd());

            act.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void MixedOperations_ShouldMaintainConsistency()
        {
            //Arrange
            var deque = new Deque<int>();

            //Act
            // Intercala inserções no início e fim
            deque.AddBeg(10); 
            deque.AddEnd(20); 
            deque.AddBeg(0);
            deque.AddEnd(30);

            //Assert
            //Assert.Equal(0, deque.PeekBeg()); // O início é 0
            //Assert.Equal(30, deque.PeekEnd()); // O fim é 30
            //Assert.Equal(4, deque.Count); // Total de 4 Deque

            // Remove do início e fim
            //Assert.Equal(0, deque.RemBeg()); // Remove do inicio, valor 0
            //Assert.Equal(30, deque.RemEnd()); // Remove do fim, valor 30
            //Assert.Equal(10, deque.PeekBeg()); // O início fica com 10
            //Assert.Equal(20, deque.PeekEnd()); // O fim fica com 20
            //Assert.Equal(2, deque.Count); // Restam 2 Deque

            // Mais operações
            //deque.AddEnd(40); // Adiciona 40 no fim
            //Assert.Equal(10, deque.RemBeg()); // Remove do inicio, valor 10
            //Assert.Equal(40, deque.RemEnd()); // Remove do fim valor 40
            //Assert.Equal(20, deque.PeekBeg()); // O início fica com 20
            //Assert.Equal(20, deque.PeekEnd()); // O fim fica com 20
            //Assert.Equal(1, deque.Count); // Restam 1 Deque

            // Limpa tudo
            //Assert.Equal(20, deque.RemBeg());
            //Assert.True(deque.IsEmpty);

            //Fiz pequenas alterações para utilizar o .And o máximo possível.
            using (new AssertionScope())
            {
                deque.PeekBeg().Should().Be(0).And
                    .Be(deque.RemBeg()); // O início é 0 e remove 0

                deque.PeekEnd().Should().Be(30).And
                    .Be(deque.RemEnd()); // O fim é 30 e remove 30

                deque.PeekBeg().Should().Be(10); // O início fica com 10
                deque.PeekEnd().Should().Be(20); // O fim fica com 20
                deque.Count.Should().Be(2); // Restam 2 Deque

                // Mais operações
                deque.AddEnd(40); // Adiciona 40 no fim
                deque.RemBeg().Should().Be(10); // Remove do inicio, valor 10
                deque.RemEnd().Should().Be(40); // Remove do fim valor 40
                deque.PeekBeg().Should().Be(20).And
                    .Be(deque.PeekEnd()); // O início e o fim fica com 20 
                deque.Count.Should().Be(1); // Restam 1 Deque

                // Limpa tudo
                deque.RemBeg().Should().Be(20); // Remove 20
                deque.IsEmpty.Should().BeTrue(); //Verifica se está vazio
            }

        }

        [Fact]
        public void IntegrityTest_LargeNumberOfOperations()
        {
            //Arrange
            var deque = new Deque<int>();
            int n = 10000;

            using (new AssertionScope())
            {
                // Enfileira no final
                for (int i = 0; i < n; i++)
                    deque.AddEnd(i);

                //Assert.Equal(n, deque.Count);

                deque.Count.Should().Be(n);

                // Desenfileira do início e verifica ordem
                for (int i = 0; i < n; i++)
                    //Assert.Equal(i, deque.RemBeg());
                    deque.RemBeg().Should().Be(i);

                //Assert.True(deque.IsEmpty);
                deque.IsEmpty.Should().BeTrue();

                // Enfileira no início
                for (int i = 0; i < n; i++)
                    deque.AddBeg(i);

                //Assert.Equal(n, deque.Count);
                deque.Count.Should().Be(n);

                // Desenfileira do final e verifica ordem reversa
                for (int i = 0; i < n; i++)
                    //Assert.Equal(i, deque.RemEnd());
                    deque.RemEnd().Should().Be(i);

                //Assert.True(deque.IsEmpty);
                deque.IsEmpty.Should().BeTrue();
            }
        }

        #region Auxiliares
        private Deque<int> CreateDequeWith(params int[] values)
        {
            var deque = new Deque<int>();
            foreach (var v in values)
                deque.AddEnd(v);
            return deque;
        }

        #endregion


    }
}