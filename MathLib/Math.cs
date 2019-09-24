using System;

namespace MathLib
{
    public static class Math
    {
        public static int Fibonacci(int index) =>
            index < 2 ? 1 : Fibonacci(index - 1) + Fibonacci(index - 2);
    }
}
