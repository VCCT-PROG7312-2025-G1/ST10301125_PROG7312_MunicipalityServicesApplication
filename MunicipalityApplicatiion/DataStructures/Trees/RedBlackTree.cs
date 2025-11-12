namespace MunicipalityApplicatiion.DataStructures.Trees
{
    public enum RbColor { Red, Black }

    public class RbNode<TKey, TValue> where TKey : IComparable<TKey>
    {
        public TKey Key;
        public TValue Value;
        public RbColor Color;
        public RbNode<TKey, TValue>? Left, Right, Parent;

        public RbNode(TKey key, TValue value)
        {
            Key = key; Value = value; Color = RbColor.Red;
        }
    }

    public class RedBlackTree<TKey, TValue> where TKey : IComparable<TKey>
    {
        public RbNode<TKey, TValue>? Root { get; private set; }

        // Rotations (Left)
        void RotateLeft(RbNode<TKey, TValue> x)
        {
            var y = x.Right!;
            x.Right = y.Left; if (y.Left != null) y.Left.Parent = x;
            y.Parent = x.Parent;
            if (x.Parent == null) Root = y;
            else if (x == x.Parent.Left) x.Parent.Left = y;
            else x.Parent.Right = y;
            y.Left = x; x.Parent = y;
        }

        // Rotations (Right)
        void RotateRight(RbNode<TKey, TValue> y)
        {
            var x = y.Left!;
            y.Left = x.Right; if (x.Right != null) x.Right.Parent = y;
            x.Parent = y.Parent;
            if (y.Parent == null) Root = x;
            else if (y == y.Parent.Left) y.Parent.Left = x;
            else y.Parent.Right = x;
            x.Right = y; y.Parent = x;
        }

        // Insertion
        public void Insert(TKey key, TValue value)
        {
            var z = new RbNode<TKey, TValue>(key, value);
            RbNode<TKey, TValue>? y = null; var x = Root;
            while (x != null)
            {
                y = x; int cmp = z.Key.CompareTo(x.Key);
                if (cmp < 0) x = x.Left;
                else if (cmp > 0) x = x.Right;
                else { x.Value = value; return; }
            }
            z.Parent = y;
            if (y == null) Root = z;
            else if (z.Key.CompareTo(y.Key) < 0) y.Left = z; else y.Right = z;
            InsertFixup(z);
        }

        // Insertion Fixup
        void InsertFixup(RbNode<TKey, TValue> z)
        {
            while (z.Parent != null && z.Parent.Color == RbColor.Red)
            {
                if (z.Parent == z.Parent.Parent!.Left)
                {
                    var y = z.Parent.Parent.Right; // uncle
                    if (y != null && y.Color == RbColor.Red)
                    {
                        z.Parent.Color = RbColor.Black;
                        y.Color = RbColor.Black;
                        z.Parent.Parent.Color = RbColor.Red;
                        z = z.Parent.Parent;
                    }
                    else
                    {
                        if (z == z.Parent.Right) { z = z.Parent; RotateLeft(z); }
                        z.Parent!.Color = RbColor.Black;
                        z.Parent.Parent!.Color = RbColor.Red;
                        RotateRight(z.Parent.Parent!);
                    }
                }
                else
                {
                    var y = z.Parent.Parent!.Left;
                    if (y != null && y.Color == RbColor.Red)
                    {
                        z.Parent.Color = RbColor.Black;
                        y.Color = RbColor.Black;
                        z.Parent.Parent!.Color = RbColor.Red;
                        z = z.Parent.Parent!;
                    }
                    else
                    {
                        if (z == z.Parent.Left) { z = z.Parent; RotateRight(z); }
                        z.Parent!.Color = RbColor.Black;
                        z.Parent.Parent!.Color = RbColor.Red;
                        RotateLeft(z.Parent.Parent!);
                    }
                }
            }
            Root!.Color = RbColor.Black;
        }

        // Search
        public bool TryGet(TKey key, out TValue value)
        {
            var n = Root;
            while (n != null)
            {
                int cmp = key.CompareTo(n.Key);
                if (cmp == 0) { value = n.Value; return true; }
                n = cmp < 0 ? n.Left : n.Right;
            }
            value = default!; return false;
        }
    }
}
