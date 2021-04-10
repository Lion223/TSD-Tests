using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSD_Tests.First;
using TSD_Tests.Second;

namespace TSD_Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            FirstTask();
            SecondTask();
        }

        static void FirstTask()
        {
            var text = "WEAREDISCOVEREDFLEEATONCE";
            var rails = 3;
            var encoded = RFC.Encode(text, rails);
            var decoded = RFC.Decode(encoded, rails);

            Console.WriteLine($"Input:\t\t{text}\nEncoded:\t{encoded}\nDecoded:\t{decoded}\n\n");
        }

        static void SecondTask()
        {
            var side = 1.1234d;
            var radius = 1.1234d;
            var @base = 5d;
            var height = 2d;
            var width = 1d;

            var shapes = new List<Shape> {
                new Square(side),
                new Circle(radius),
                new Triangle(@base, height),
                new Rectangle(width, height),
                new Square(5)
            };

            shapes.Sort();

            foreach (var shape in shapes)
            {
                Console.WriteLine(shape);
            }
        }
    }

    namespace First
    {
        /// <summary>
        /// Rail Fence Cipher implementation
        /// </summary>
        public static class RFC
        {
            /// <summary>
            /// Encoding method
            /// </summary>
            /// <param name="text">The input to encode</param>
            /// <param name="rails">Number of rails/levels</param>
            /// <returns>Encoded string</returns>
            public static string Encode(string text, int rails)
            {
                // List of StringBuilder objects to store a divided text, each in its rail/level
                // Instead of jagged or two-dimensional array, which is more predetermined way
                var data = new List<StringBuilder>();

                for (int i = 0; i < rails; i++)
                {
                    data.Add(new StringBuilder());
                }

                // The starter position: upper left -> bottom right
                int currLine = 0;
                // "1" - move down, "-1" - move up
                int direction = 1;

                for (int i = 0; i < text.Length; i++)
                {
                    data[currLine].Append(text[i]);

                    if (currLine == 0)
                    {
                        direction = 1;
                    }
                    else if (currLine == rails - 1)
                    {
                        direction = -1;
                    }

                    currLine += direction;
                }

                var result = new StringBuilder();

                // Each of StringBuilder objects in the list contain one of the rails
                // In order to get the encoded string, each one of them must be appended, starting from the upper one
                for (int i = 0; i < rails; i++)
                {
                    result.Append(data[i]);
                }

                return result.ToString();
            }

            /// <summary>
            /// Decoding method
            /// </summary>
            /// <param name="text">The input to decode</param>
            /// <param name="rails">Number of rails/levels</param>
            /// <returns>Decoded string</returns>
            public static string Decode(string text, int rails)
            {
                // Store the length information about each of the rails
                int[] linesLength = Enumerable.Repeat(0, rails).ToArray();

                // Repeat the encoding algorithm to find the length of each of the rails
                int currLine = 0;
                int direction = 1;

                for (int i = 0; i < text.Length; i++)
                {
                    linesLength[currLine]++;

                    if (currLine == 0)
                    {
                        direction = 1;
                    }
                    else if (currLine == rails - 1)
                    {
                        direction = -1;
                    }

                    currLine += direction;
                }

                var data = new List<StringBuilder>();

                for (int i = 0; i < rails; i++)
                {
                    data.Add(new StringBuilder());
                }

                // Divide the text to each of the rails
                int currChar = 0;

                for (int line = 0; line < rails; line++)
                {
                    for (int c = 0; c < linesLength[line]; c++)
                    {
                        data[line].Append(text[currChar]);
                        currChar++;
                    }
                }

                // Replicate the zig-zag movement of the encoded rails, to assemble a decoded version
                currLine = 0;
                direction = 1;
                // Remember character position of every rail
                int[] linePos = Enumerable.Repeat(0, rails).ToArray();

                var result = new StringBuilder();

                for (int i = 0; i < text.Length; i++)
                {
                    result.Append(data[currLine][linePos[currLine]]);

                    if (currLine == 0)
                    {
                        direction = 1;
                    }
                    else if (currLine == rails - 1)
                    {
                        direction = -1;
                    }

                    linePos[currLine]++;
                    currLine += direction;
                }

                return result.ToString();
            }
        }
    }

    namespace Second
    {
        /// <summary>
        /// Base class for all shapes
        /// </summary>
        public abstract class Shape : IComparable<Shape>
        {
            /// <summary>
            /// Area of a shape
            /// </summary>
            protected double _area;

            /// <summary>
            /// Custom comparer for the List.Sort() method, which will sort elements based on their _area fields
            /// </summary>
            /// <param name="other">Second operand for compare</param>
            /// <returns>Boolean result of compare</returns>
            public int CompareTo(Shape other)
            {
                return _area.CompareTo(other?._area);
            }
        }

        /// <summary>
        /// Square shape
        /// </summary>
        public class Square : Shape
        {
            /// <summary>
            /// Square object's string representation
            /// </summary>
            /// <returns>Shape figure and its area</returns>
            public override string ToString()
            {
                return $"Square. Area: { _area }";
            }

            /// <summary>
            /// Constructor that receives a variable needed to evaluate the area of the shape
            /// </summary>
            /// <param name="side">Square's area is evaluated by side squared</param>
            public Square(double side)
            {
                _area = side * side;
            }
        }

        /// <summary>
        /// Rectangle shape
        /// </summary>
        public class Rectangle : Shape
        {
            /// <summary>
            /// Square object's string representation
            /// </summary>
            /// <returns>Shape figure and its area</returns>
            public override string ToString()
            {
                return $"Rectangle. Area: { _area }";
            }

            /// <summary>
            /// Constructor that receives variables needed to evaluate the area of the shape
            /// </summary>
            /// <param name="width">The first multiplier for the area's evaluation</param>
            /// <param name="height">The second multiplier for the area's evaluation</param>
            public Rectangle(double width, double height)
            {
                _area = width * height;
            }
        }

        /// <summary>
        /// Triangle shape
        /// </summary>
        public class Triangle : Shape
        {
            /// <summary>
            /// Square object's string representation
            /// </summary>
            /// <returns>Shape figure and its area</returns>
            public override string ToString()
            {
                return $"Triangle. Area: { _area }";
            }

            /// <summary>
            /// Constructor that receives variables needed to evaluate the area of the shape
            /// </summary>
            /// <param name="base">The first multiplier for the area's evaluation</param>
            /// <param name="height">The second multiplier is divided by 2 for the area's evaluation</param>
            public Triangle(double @base, double height)
            {
                _area = @base * (height / 2);
            }
        }

        /// <summary>
        /// Circle shape
        /// </summary>
        public class Circle : Shape
        {
            /// <summary>
            /// Square object's string representation
            /// </summary>
            /// <returns>Shape figure and its area</returns>
            public override string ToString()
            {
                return $"Circle. Area: { _area }";
            }

            /// <summary>
            /// Constructor that receives variables needed to evaluate the area of the shape
            /// </summary>
            /// <param name="radius">π's multiplier for the area's evaluation</param>
            public Circle(double radius)
            {
                _area = radius * Math.PI;
            }
        }
    }

}
