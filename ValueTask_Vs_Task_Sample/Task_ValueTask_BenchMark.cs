using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValueTask_Vs_Task_Sample
{
    [MemoryDiagnoser]
    public class Task_ValueTask_BenchMark
    {
        readonly Repository _repository = new Repository();

        /// <summary>
        /// Here "MemoryDiagnoser" attribute gives info about Memory Allocation.
        /// "BenchMark" attribute marks the method for evaluation.
        /// </summary>
        /// <returns></returns>
        
        [Benchmark]
        public async Task RunTaskAsync()
        {
          await _repository.GetDatawithTaskAsync("key");
        }

        [Benchmark]
        public async Task RunValueTaskAsync()
        {
            await _repository.GetDatawithValueTaskAsync("value");
        }
    }
}
