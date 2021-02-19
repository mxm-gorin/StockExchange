using System.Threading;

namespace Infrastructure.Models
{
    public class Counter
    {
        private long _value;
        public long Value => _value;

        public void Increment()
        {
            Interlocked.Increment(ref _value);
        }
        
        public void Add(ref int number)
        {
            Interlocked.Add(ref _value, number);
        }
    }
}