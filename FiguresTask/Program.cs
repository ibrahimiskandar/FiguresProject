using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using Style;
namespace FiguresTask
{
    public class Program
    {
        static void Main(string[] args)
        {

            string fileName = @"D:\figures.bin";
            List<Figure> figures = new List<Figure>();

            using (Stream stream = File.Open(fileName, FileMode.Open))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                figures = (List<Figure>)bformatter.Deserialize(stream);
            }

            while (true)
            {
                Extenstion.Alert(ConsoleColor.DarkCyan, "1.Show all figures \n2.Create a figure \n3.Change figure \n4.Save to file \n0.Exit");
                bool parsed = int.TryParse(Console.ReadLine(), out int choice);
                switch (choice)
                {
                    case 1:
                        foreach (var figure in figures)
                        {
                            Console.WriteLine(figure);
                        }
                        break;
                    case 2:
                        Extenstion.Alert(ConsoleColor.DarkCyan, "What figure do you want to create?");
                        Extenstion.Alert(ConsoleColor.DarkCyan, "1.Rectangle \n2.Circle");
                        int choiceForFigure = Extenstion.IntIsValid();
                        switch (choiceForFigure)
                        {
                            case 1:
                                List<Point> pointsOfRectangle = new List<Point>();
                                Extenstion.Alert(ConsoleColor.DarkCyan, "You need to add points of rectangle!");
                                for (int i = 0; i < 4; i++)
                                {
                                    Extenstion.Alert(ConsoleColor.DarkCyan, "Enter X :");
                                    int X = Extenstion.IntIsValid();
                                    Extenstion.Alert(ConsoleColor.DarkCyan, "Enter Y :");
                                    int Y = Extenstion.IntIsValid();
                                    pointsOfRectangle.Add(new Point(X, Y));
                                }
                                Rectangle rectangle1 = new Rectangle(pointsOfRectangle);
                                figures.Add(rectangle1);
                                Extenstion.Alert(ConsoleColor.DarkCyan, "Rectangle created!");
                                break;
                            case 2:
                                List<Point> pointsOfCircle = new List<Point>();
                                Extenstion.Alert(ConsoleColor.DarkCyan, "You need to add points of circle!");
                                for (int i = 0; i < 2; i++)
                                {
                                    Extenstion.Alert(ConsoleColor.DarkCyan, "Enter X :");
                                    int X = Extenstion.IntIsValid();
                                    Extenstion.Alert(ConsoleColor.DarkCyan, "Enter Y :");
                                    int Y = Extenstion.IntIsValid();
                                    pointsOfCircle.Add(new Point(X, Y));
                                }
                                Circle circle = new Circle(pointsOfCircle);
                                figures.Add(circle);
                                break;
                        }
                        break;
                    case 3:
                        if (FiguresCount())
                        {
                            Extenstion.Alert(ConsoleColor.DarkCyan, "What do you want to do with the figure?");
                            Extenstion.Alert(ConsoleColor.Cyan, "Choose by ID");
                            Extenstion.Alert(ConsoleColor.DarkCyan, "1.Move figure \n2.Rotate figure \n3.Scale figure");
                            int chooseForChange = Extenstion.IntIsValid();
                            switch (chooseForChange)
                            {
                                case 1:
                                    Extenstion.Alert(ConsoleColor.DarkCyan, "Which figure do you want to move?");
                                    DisplayFigures();
                                    bool IDAndInputAreEqual = true;
                                    while (IDAndInputAreEqual)
                                    {
                                        int inputID = Extenstion.IntIsValid();
                                        for (int i = 0; i < figures.Count; i++)
                                        {
                                            if (i == inputID)
                                            {
                                                Extenstion.Alert(ConsoleColor.DarkCyan, "Type new X:");
                                                int X = Extenstion.IntIsValid();
                                                Extenstion.Alert(ConsoleColor.DarkCyan, "Type new Y:");
                                                int Y = Extenstion.IntIsValid();
                                                figures[i].MoveFigure(X, Y);
                                                IDAndInputAreEqual = false;
                                            }

                                        }
                                        if (IDAndInputAreEqual)
                                        {
                                            Extenstion.Alert(ConsoleColor.DarkYellow, "There is no figure with such ID! Try again: ");
                                        }
                                    }
                                    break;
                                case 2:
                                    Extenstion.Alert(ConsoleColor.DarkCyan, "Which figure do you want to rotate?");
                                    DisplayFigures();
                                    bool IDAndInputAreEqualForRotation = true;
                                    while (IDAndInputAreEqualForRotation)
                                    {
                                        int inputID = Extenstion.IntIsValid();
                                        for (int i = 0; i < figures.Count; i++)
                                        {
                                            if (i == inputID)
                                            {
                                                Extenstion.Alert(ConsoleColor.DarkCyan, "Type angle:");
                                                double angle = Convert.ToDouble(Console.ReadLine());
                                                figures[i].RotateFigure(angle);
                                                IDAndInputAreEqualForRotation = false;
                                            }
                                        }
                                        if (IDAndInputAreEqualForRotation)
                                        {
                                            Extenstion.Alert(ConsoleColor.DarkYellow, "There is no figure with such ID! Try again: ");
                                        }
                                    }

                                    break;
                                case 3:
                                    Extenstion.Alert(ConsoleColor.DarkCyan, "Which figure do you want to scale?");
                                    DisplayFigures();
                                    bool IDAndInputAreEqualForScaling = true;
                                    while (IDAndInputAreEqualForScaling)
                                    {
                                        int inputID = Extenstion.IntIsValid();
                                        for (int i = 0; i < figures.Count; i++)
                                        {
                                            if (i == inputID)
                                            {
                                                Extenstion.Alert(ConsoleColor.DarkCyan, "Type scale:");
                                                double scale = Convert.ToDouble(Console.ReadLine());
                                                figures[i].Scale(scale);
                                                IDAndInputAreEqualForScaling = false;
                                            }
                                        }
                                        if (IDAndInputAreEqualForScaling)
                                        {
                                            Extenstion.Alert(ConsoleColor.DarkYellow, "There is no figure with such ID! Try again: ");
                                        }
                                    }

                                    break;
                            }
                        }
                        break;
                    case 4:
                        WriteBinary(fileName, figures);

                        break;
                    default:
                        if (parsed)
                        {
                            if (choice == 0)
                            {
                                Extenstion.Alert(ConsoleColor.Yellow, "Program finished!");
                                Environment.Exit(1);
                                return;
                            }
                            Extenstion.Alert(ConsoleColor.DarkYellow, "Enter numbers according to menu!");
                        }
                        else
                        {
                            Extenstion.Alert(ConsoleColor.DarkYellow, "Enter digits only!");
                        }
                        break;
                }
            }
            void DisplayFigures()
            {
                for (int i = 0; i < figures.Count; i++)
                {
                    Console.WriteLine($"{i}.{figures[i]}");
                }
            }

            void WriteBinary(string filePath, List<Figure> list)
            {
                using (Stream stream = File.Open(filePath, FileMode.Create))
                {
                    var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                    bformatter.Serialize(stream, list);
                }
            }
            bool FiguresCount()
            {
                if (figures.Count() > 0)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
