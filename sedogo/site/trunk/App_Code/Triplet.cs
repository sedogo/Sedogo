using System;

[Serializable]
public class Triplet<T, U, V>
    {
        public T First;

        public U Second;

        public V Third;

        public Triplet()
            : this(default(T))
        {
        }

        public Triplet(T x)
            : this(x, default(U))
        {
        }

        public Triplet(T x, U y)
            : this(x, y, default(V))
        {
        }

        public Triplet(T x, U y, V z)
        {
            First = x;
            Second = y;
            Third = z;
        }

    /// <summary>
    /// Determines are all parameters of Triplet equals to its default value.
    /// </summary>
    /// <returns>Is Triplet's parameters initialized by its defulat values</returns>
    public bool IsEmpty()
    {
        return First.Equals(default(T)) && Second.Equals(default(U)) && Third.Equals(default(V));
    }
}
