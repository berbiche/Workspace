using System.Collections.Generic;
using System.Linq;

namespace Collections
{
    public class Stack<T>
    {
        public int Count => elements.Count;
        public bool IsEmpty => elements.Count > 0;
        protected IList<T> elements;

        public Stack() => elements = new List<T>();

        public void Push(T element) => elements.Add(element);

        public T Peek() => elements.Last();

        public T Pop()
        {
            T tmp = Peek();
            elements.RemoveAt(Count - 1);
            return tmp;
        }
    }
}
