using System.Linq;

namespace MatrixCalculator.Objects
{
    public class Vector<T> {
        public Vector(T[] body) {
            Body = body;
        }
        private T[] Body { get; set; }
        public int Size() => Body.Length;
        public T this[int key] {
           get => Body[key];
           set => SetValue(key, value);
        }
        public string PrintLikeColumn() => Body.Aggregate("", (current, t) => current + (t + "\n"));
        public string PrintLikeRow() => Body.Aggregate("", (current, t) => current + (t + " "));
        private void SetValue(int key, T value) {
            Body[key] = value;
        }
    }
}