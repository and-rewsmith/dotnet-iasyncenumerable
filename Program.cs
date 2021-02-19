using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_iasyncenumerable
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            IAsyncEnumerable<string> enumerable = new CustomEnumerable<string>("ronald");
            IAsyncEnumerator<string> enumerator = enumerable.GetAsyncEnumerator();
            DummyAbstraction dummyAbstraction = new DummyAbstraction(enumerator);
            await dummyAbstraction.Run();
        }
    }

    class DummyAbstraction
    {
        IAsyncEnumerator<string> Enumerator;
        public DummyAbstraction(IAsyncEnumerator<string> enumerator)
        {
            this.Enumerator = enumerator;
        }

        public async Task Run()
        {
            bool hasValue = await this.Enumerator.MoveNextAsync();

            if (hasValue)
            {
                Console.WriteLine(this.Enumerator.Current);
            }
        }
    }
}
