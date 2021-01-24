using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;


namespace Lesson_03_Distance_Calc_Perfomance
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
        }
    }
    [MinColumn,MedianColumn,MaxColumn]
    public class BenchmarkClass
    {
        public static readonly int[,] data = { {20,20},{25,37},{10,14},{45,52},{58,3},{95,5},{2,61},{25,62},{44,44},{30,87} };
        public class PointClass
        {
            public int X;
            public int Y;
        }
        public struct PointStruct
        {
            public int X;
            public int Y;
        }

        [Benchmark]
        public void CalcDistanceFloatClass()
        {
            Random random = new Random();
            int i = random.Next(0, data.GetUpperBound(0) + 1);

            PointClass pointClassOne = new PointClass();
            pointClassOne.X = data[i, 0];
            pointClassOne.Y = data[i, 1];
            PointClass pointClassTwo = new PointClass();
            pointClassOne.X = data[i, 0];
            pointClassOne.Y = data[i, 1];

            float resFloatClass = PointDistanceCl(pointClassOne, pointClassTwo);
        }
        [Benchmark]
        public void CalcDistanceFloatStruct()
        {
            Random random = new Random();
            int i = random.Next(0, data.GetUpperBound(0) + 1);

            PointStruct pointStructOne = new PointStruct();
            pointStructOne.X = data[i, 0];
            pointStructOne.Y = data[i, 1];
            PointStruct pointStructTwo = new PointStruct();
            pointStructTwo.X = data[i, 0];
            pointStructTwo.Y = data[i, 1];

            float resFloatStruct = PointDistance(pointStructOne, pointStructTwo);
        }
        [Benchmark]
        public void CalcDistanceDoubleStruct()
        {
            Random random = new Random();
            int i = random.Next(0, data.GetUpperBound(0) + 1);

            PointStruct pointStructOne = new PointStruct();
            pointStructOne.X = data[i, 0];
            pointStructOne.Y = data[i, 1];
            PointStruct pointStructTwo = new PointStruct();
            pointStructTwo.X = data[i, 0];
            pointStructTwo.Y = data[i, 1];

            double resDoubleStruct = PointDistanceDouble(pointStructOne, pointStructTwo);
        }
        [Benchmark]
        public void CalcDistanceFloatStructShort()
        {
            Random random = new Random();
            int i = random.Next(0, data.GetUpperBound(0) + 1);

            PointStruct pointStructOne = new PointStruct();
            pointStructOne.X = data[i, 0];
            pointStructOne.Y = data[i, 1];
            PointStruct pointStructTwo = new PointStruct();
            pointStructTwo.X = data[i, 0];
            pointStructTwo.Y = data[i, 1];

            float resFloatStructShort = PointDistanceShort(pointStructOne, pointStructTwo);
        }

        public static float PointDistanceCl(PointClass pointOne, PointClass pointTwo)
        {
            float x = Math.Abs(pointOne.X - pointTwo.X);
            float y = Math.Abs(pointOne.Y - pointTwo.Y);
            return MathF.Sqrt((x * x) + (y * y));
        }
        
        public static float PointDistance(PointStruct pointOne, PointStruct pointTwo)
        {
            float x = Math.Abs(pointOne.X - pointTwo.X);
            float y = Math.Abs(pointOne.Y - pointTwo.Y);
            return MathF.Sqrt((x * x) + (y * y));
        }
        
        public static double PointDistanceDouble(PointStruct pointOne, PointStruct pointTwo)
        {
            double x = Math.Abs(pointOne.X - pointTwo.X);
            double y = Math.Abs(pointOne.Y - pointTwo.Y);
            return Math.Sqrt((x * x) + (y * y));
        }
        
        public static float PointDistanceShort(PointStruct pointOne, PointStruct pointTwo)
        {
            float x = Math.Abs(pointOne.X - pointTwo.X);
            float y = Math.Abs(pointOne.Y - pointTwo.Y);
            return (x * x) + (y * y);
        }
    }
}
