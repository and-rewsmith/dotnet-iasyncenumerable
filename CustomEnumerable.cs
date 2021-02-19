// Copyright (c) Microsoft. All rights reserved.
namespace dotnet_iasyncenumerable
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    class CustomEnumerable<T> : IAsyncEnumerable<string>
    {
        CustomEnumerator enumerator;

        public IAsyncEnumerator<string> GetAsyncEnumerator(CancellationToken _)
        {
            return this.enumerator;
        }

        public CustomEnumerable(string input)
        {
            this.enumerator = new CustomEnumerator(input);
        }

        public class CustomEnumerator : IAsyncEnumerator<string>
        {
            string current;
            string input;
            int index;

            internal CustomEnumerator(string input)
            {
                this.current = null;
                this.input = input;
                this.index = 0;
            }

            public ValueTask DisposeAsync()
            {
                return new ValueTask(Task.Run(() => Task.Delay(TimeSpan.FromSeconds(1))));
            }

            public string Current => this.current;

            public async ValueTask<bool> MoveNextAsync()
            {
                await Task.Delay(TimeSpan.FromSeconds(1));

                if (index >= input.Length)
                {
                    return false;
                }

                this.current = input[index].ToString();
                this.index += 1;
                return true;
            }
        }
    }
}
