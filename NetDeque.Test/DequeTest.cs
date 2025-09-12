namespace NetDeque.Test
{
    public class DequeTest
    {
        [Fact]
        public void NewDeque_ShouldBeEmpty()
        {
            var deque = new Deque<int>();
            Assert.True(deque.IsEmpty);
            Assert.Equal(0, deque.Count);
        }

        [Fact]
        public void AddBeg_ShouldIncreaseCount_AndAddToFront()
        {
            var deque = CreateDequeWith(10);

            Assert.False(deque.IsEmpty);
            Assert.Equal(1, deque.Count);
            Assert.Equal(10, deque.PeekBeg());
            Assert.Equal(10, deque.PeekEnd());
        }

        [Fact]
        public void AddEndSameValue_ShouldIncreaseCount_AndAddToEnd()
        {
            var deque = CreateDequeWith(20);
            deque.AddEnd(20);
            Assert.False(deque.IsEmpty);            
            Assert.Equal(20, deque.PeekBeg());
            Assert.Equal(20, deque.PeekEnd());
            Assert.Equal(2, deque.Count);
        }

        [Fact]
        public void AddBeg_And_AddEnd_ShouldMaintainOrder()
        {
            var deque = CreateDequeWith(30,10,20);
            deque.AddBeg(15);
            deque.AddEnd(5);
            Assert.Equal(15, deque.PeekBeg());
            Assert.Equal(5, deque.PeekEnd());
            Assert.Equal(5, deque.Count);
        }

        [Fact]
        public void RemBeg_ShouldRemoveAndReturnFirst()
        {
            var deque = CreateDequeWith(30, 10, 20);
            deque.AddEnd(20);
            deque.AddEnd(10);

            var removed = deque.RemBeg();

            Assert.Equal(30, removed);
            Assert.Equal(4, deque.Count);
            Assert.Equal(10, deque.PeekBeg());
        }

        [Fact]
        public void RemEnd_ShouldRemoveAndReturnLast()
        {
            var deque = new Deque<int>();
            deque.AddEnd(1);
            deque.AddEnd(2);

            var removed = deque.RemEnd();

            Assert.Equal(2, removed);
            Assert.Equal(1, deque.Count);
            Assert.Equal(1, deque.PeekEnd());
        }

        [Fact]
        public void PeekBeg_And_PeekEnd_ShouldNotRemove()
        {
            var deque = new Deque<int>();
            deque.AddEnd(10);
            deque.AddEnd(20);
            Assert.Equal(10, deque.PeekBeg());
            Assert.Equal(20, deque.PeekEnd());
            Assert.Equal(2, deque.Count);
        }

        [Fact]
        public void RemBeg_OnEmptyDeque_ShouldThrow()
        {
            var deque = new Deque<int>();
            Assert.Throws<InvalidOperationException>(() => deque.RemBeg());
        }

        [Fact]
        public void RemEnd_OnEmptyDeque_ShouldThrow()
        {
            var deque = new Deque<int>();
            Assert.Throws<InvalidOperationException>(() => deque.RemEnd());
        }

        [Fact]
        public void PeekBeg_OnEmptyDeque_ShouldThrow()
        {
            var deque = new Deque<int>();
            Assert.Throws<InvalidOperationException>(() => deque.PeekBeg());
        }

        [Fact]
        public void PeekEnd_OnEmptyDeque_ShouldThrow()
        {
            var deque = new Deque<int>();
            Assert.Throws<InvalidOperationException>(() => deque.PeekEnd());
        }

        [Fact]
        public void MixedOperations_ShouldMaintainConsistency()
        {
            var deque = new Deque<int>();

            // Intercala inserções no início e fim
            deque.AddBeg(10); 
            deque.AddEnd(20); 
            deque.AddBeg(0);
            deque.AddEnd(30); 

            Assert.Equal(0, deque.PeekBeg()); // O início é 0
            Assert.Equal(30, deque.PeekEnd()); // O fim é 30
            Assert.Equal(4, deque.Count); // Total de 4 Deque

            // Remove do início e fim
            Assert.Equal(0, deque.RemBeg()); // Remove do inicio, valor 0
            Assert.Equal(30, deque.RemEnd()); // Remove do fim, valor 30
            Assert.Equal(10, deque.PeekBeg()); // O início fica com 10
            Assert.Equal(20, deque.PeekEnd()); // O fim fica com 20
            Assert.Equal(2, deque.Count); // Restam 2 Deque

            // Mais operações
            deque.AddEnd(40); // Adiciona 40 no fim
            Assert.Equal(10, deque.RemBeg()); // Remove do inicio, valor 10
            Assert.Equal(40, deque.RemEnd()); // Remove do fim valor 40
            Assert.Equal(20, deque.PeekBeg()); // O início fica com 20
            Assert.Equal(20, deque.PeekEnd()); // O fim fica com 20
            Assert.Equal(1, deque.Count); // Restam 1 Deque

            // Limpa tudo
            Assert.Equal(20, deque.RemBeg());
            Assert.True(deque.IsEmpty);
        }

        [Fact]
        public void IntegrityTest_LargeNumberOfOperations()
        {
            var deque = new Deque<int>();
            int n = 10000;

            // Enfileira no final
            for (int i = 0; i < n; i++)
                deque.AddEnd(i);

            Assert.Equal(n, deque.Count);

            // Desenfileira do início e verifica ordem
            for (int i = 0; i < n; i++)
                Assert.Equal(i, deque.RemBeg());

            Assert.True(deque.IsEmpty);

            // Enfileira no início
            for (int i = 0; i < n; i++)
                deque.AddBeg(i);

            Assert.Equal(n, deque.Count);

            // Desenfileira do final e verifica ordem reversa
            for (int i = 0; i < n; i++)
                Assert.Equal(i, deque.RemEnd());
            
            Assert.True(deque.IsEmpty);
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