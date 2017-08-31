namespace Collections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class PriorityStack<T>: Stack<T>
    {
        protected IList<T> priorityElements;

        public new int Count => priorityElements.Count + elements.Count;

        public new bool IsEmpty => Count > 0 && base.Count > 0;
        public PriorityStack() : base() => priorityElements = new List<T>();

        public bool PriorityIsEmpty => priorityElements.Count > 0;

        public void PriorityPush(T element) => priorityElements.Add(element);

        public new T Pop()
        {
            if (PriorityIsEmpty)
            {
                return base.Pop();
            }
            T tmp = priorityElements.Last();
            priorityElements.RemoveAt(Count - 1);
            return tmp;
        }

        public new T Peek() => PriorityIsEmpty ? base.Peek() : priorityElements.Last();
    }
}
