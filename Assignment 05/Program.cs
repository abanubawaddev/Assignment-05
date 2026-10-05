namespace Assignment_05
{
    internal class Program
    {
    

// Part 1 - Q9

public class PreInterfaceCar
        {
            public void MoveForward()
            {
                Console.WriteLine("Moving forward");
            }

            public void MoveBackward()
            {
                Console.WriteLine("Moving backward");
            }
        }
        //-------------------
        // Part 1 - Q10

        public interface IMoveable
        {
            void MoveForward();
            void MoveBackward();
        }

        public interface IFlyable
        {
            void MoveUp();
            void MoveDown();
        }

        // Part 1 - Q11
        //---------------
        public class Car : IMoveable
        {
            public void MoveForward()
            {
                Console.WriteLine("Car is moving forward on the ground.");
            }

            public void MoveBackward()
            {
                Console.WriteLine("Car is moving backward on the ground.");
            }
        }

        public class Airplane : IMoveable, IFlyable
        {
            public void MoveForward()
            {
                Console.WriteLine("Airplane is moving forward on the runway.");
            }

            public void MoveBackward()
            {
                Console.WriteLine("Airplane is moving backward on the runway.");
            }

            public void MoveUp()
            {
                Console.WriteLine("Airplane is flying up in the air.");
            }

            public void MoveDown()
            {
                Console.WriteLine("Airplane is flying down in the air.");
            }
        }
        //---------------------
        // Part 1 - Q13

        public class Ship : IMoveable
        {
            void IMoveable.MoveForward()
            {
                Console.WriteLine("Ship is moving forward on the sea.");
            }

            void IMoveable.MoveBackward()
            {
                Console.WriteLine("Ship is moving backward on the sea.");
            }
        }
        //-----------------
        // Part 1 - Q14

        public interface IVehicle : IMoveable, IFlyable
        {
        }

        public class Vehicle : IVehicle
        {
            public virtual void MoveForward()
            {
                Console.WriteLine("Vehicle is moving forward.");
            }

            public virtual void MoveBackward()
            {
                Console.WriteLine("Vehicle is moving backward.");
            }

            public virtual void MoveUp()
            {
                Console.WriteLine("Vehicle is moving up.");
            }

            public virtual void MoveDown()
            {
                Console.WriteLine("Vehicle is moving down.");
            }
        }

        //----------------------------------------
        
            static void Main()
            {
                // Part 1 - Q9

                Console.WriteLine("[Answer Q9]: Forcing every vehicle class to implement MoveUp() and MoveDown() violates the Interface Segregation Principle. Vehicles like Cars do not fly, so they would have to implement empty methods or throw exceptions. This leads to rigid and hard-to-maintain code.");
                Console.WriteLine();

                // Part 1 - Q12

                Car myCar = new Car();
                myCar.MoveForward();
                myCar.MoveBackward();

                Airplane myAirplane = new Airplane();
                myAirplane.MoveForward();
                myAirplane.MoveUp();

                IMoveable carRef = new Car();
                carRef.MoveForward();
                carRef.MoveBackward();

                IMoveable planeRef = new Airplane();
                planeRef.MoveForward();
                planeRef.MoveBackward();

                Console.WriteLine();
                Console.WriteLine("[Answer Q12]: You cannot call MoveUp() on planeRef because the reference type is IMoveable, which only contains MoveForward() and MoveBackward(). To call MoveUp(), you would need an IFlyable reference (IFlyable flyRef = new Airplane();) or an Airplane reference.");
                Console.WriteLine();

                // Part 1 - Q13

                Vehicle myVehicle = new Vehicle();
                myVehicle.MoveUp();

                Console.WriteLine();
                Console.WriteLine("[Answer Q13]: An interface inheriting other interfaces allows combining multiple smaller contracts into one unified contract. It makes it easier to require a type to implement all behaviors (both IMoveable and IFlyable) using a single interface name (IVehicle) without duplicating method signatures.");
                Console.WriteLine();

                // Part 1 - Q14

                Ship myShip = new Ship();

                IMoveable shipRef = myShip;
                shipRef.MoveForward();
                shipRef.MoveBackward();

                Console.WriteLine();
                Console.WriteLine("[Answer Q14]: Yes, a class can implement an interface explicitly. When you try calling it directly on a Ship object (e.g., myShip.MoveForward()), it causes a compile-time error because explicit interface members are not part of the class's public interface. You must cast the object to the interface first or use an interface reference, like: ((IMoveable)myShip).MoveForward();");
            }
        }
}
   

//  Point3D 
class Point3D : IComparable, ICloneable
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }

    // ---- Constructors with chaining (each one calls the next bigger one) ----
    public Point3D() : this(0) { }
    public Point3D(int x) : this(x, 0) { }
    public Point3D(int x, int y) : this(x, y, 0) { }
    public Point3D(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    // ---- 2. ToString ----
    public override string ToString()
    {
        return $"Point Coordinates: ({X}, {Y}, {Z})";
    }

    // ---- 4. Equality ----
    // WITHOUT overloading, "==" on classes compares REFERENCES (addresses in memory),
    // so two different objects with the same coordinates give false.
    // To make it work properly we override Equals/GetHashCode and overload == and !=.
    public override bool Equals(object obj)
    {
        return obj is Point3D p && X == p.X && Y == p.Y && Z == p.Z;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Z);
    }

    public static bool operator ==(Point3D a, Point3D b)
    {
        if (ReferenceEquals(a, b)) return true;
        if (a is null || b is null) return false;
        return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
    }

    public static bool operator !=(Point3D a, Point3D b)
    {
        return !(a == b);
    }

    // ---- 5. IComparable: sort by X then Y ----
    public int CompareTo(object obj)
    {
        if (obj is null) return 1;
        if (!(obj is Point3D other))
            throw new ArgumentException("Object is not a Point3D");

        int result = X.CompareTo(other.X);
        if (result != 0) return result;
        return Y.CompareTo(other.Y);
    }

    // ---- 6. ICloneable ----
    public object Clone()
    {
        return new Point3D(X, Y, Z); // or MemberwiseClone()
    }
}

class Pro
{
    // ---- 3. Reading input: three ways to validate ----

    // (a) int.TryParse: no exception, returns false if invalid
    static int ReadWithTryParse(string msg)
    {
        int value;
        Console.Write(msg);
        while (!int.TryParse(Console.ReadLine(), out value))
            Console.Write("Invalid number, try again: ");
        return value;
    }

    // (b) int.Parse: throws FormatException / OverflowException if invalid
    static int ReadWithParse(string msg)
    {
        while (true)
        {
            Console.Write(msg);
            try
            {
                return int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid number, try again.");
            }
        }
    }

    // (c) Convert.ToInt32: same idea as Parse (but null becomes 0)
    static int ReadWithConvert(string msg)
    {
        while (true)
        {
            Console.Write(msg);
            try
            {
                return Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid number, try again.");
            }
        }
    }

    static void Main()
    {
        // 2. ToString test
        Point3D P = new Point3D(10, 10, 10);
        Console.WriteLine(P.ToString());

        // 3. Read two points
        Console.WriteLine("\nEnter P1 (using TryParse):");
        Point3D P1 = new Point3D(
            ReadWithTryParse("X: "),
            ReadWithTryParse("Y: "),
            ReadWithTryParse("Z: "));

        Console.WriteLine("\nEnter P2 (using Parse):");
        Point3D P2 = new Point3D(
            ReadWithParse("X: "),
            ReadWithParse("Y: "),
            ReadWithConvert("Z (using Convert): "));

        Console.WriteLine("\nP1 -> " + P1);
        Console.WriteLine("P2 -> " + P2);

        // 4. Does == work?
        // (object) cast forces the DEFAULT behavior (reference comparison)
        Console.WriteLine("\nDefault == (reference comparison): " + ((object)P1 == (object)P2));
        Console.WriteLine("Our overloaded == (compares values): " + (P1 == P2));
        if (P1 == P2)
            Console.WriteLine("P1 and P2 are equal");
        else
            Console.WriteLine("P1 and P2 are NOT equal");

        // 5. Array of points sorted by X then Y
        Point3D[] points =
        {
            new Point3D(5, 3, 1),
            new Point3D(2, 9, 4),
            new Point3D(5, 1, 7),
            new Point3D(2, 4, 0),
            new Point3D(1, 8, 2)
        };

        Array.Sort(points);
        Console.WriteLine("\nSorted points (by X then Y):");
        foreach (Point3D pt in points)
            Console.WriteLine(pt);

        // 6. Clone
        Point3D copy = (Point3D)P1.Clone();
        copy.X = 999;
        Console.WriteLine("\nOriginal P1: " + P1);
        Console.WriteLine("Clone (X changed): " + copy);
    }
}




// Version 1 had normal (instance) methods, so we needed: new Maths().Add(5, 3)
// Modified version: the class and methods are static, so no instance is needed: Maths.Add(5, 3)
static class Maths
{
    public static double Add(double a, double b)
    {
        return a + b;
    }

    public static double Subtract(double a, double b)
    {
        return a - b;
    }

    public static double Multiply(double a, double b)
    {
        return a * b;
    }

    public static double Divide(double a, double b)
    {
        if (b == 0)
            throw new DivideByZeroException("Cannot divide by zero.");
        return a / b;
    }
}

class p
{
    static void Main()
    {
        Console.WriteLine("10 + 5 = " + Maths.Add(10, 5));
        Console.WriteLine("10 - 5 = " + Maths.Subtract(10, 5));
        Console.WriteLine("10 * 5 = " + Maths.Multiply(10, 5));
        Console.WriteLine("10 / 5 = " + Maths.Divide(10, 5));

        try
        {
            Console.WriteLine(Maths.Divide(10, 0));
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}



class Duration
{
    // ---- Attributes ----
    public int Hours { get; private set; }
    public int Minutes { get; private set; }
    public int Seconds { get; private set; }

    // Total time in seconds (used by the operators)
    public int TotalSeconds
    {
        get { return Hours * 3600 + Minutes * 60 + Seconds; }
    }

    // ---- Constructors ----
    public Duration() : this(0) { }

    public Duration(int hours, int minutes, int seconds)
        : this(hours * 3600 + minutes * 60 + seconds) { }

    // Main constructor: converts total seconds into Hours / Minutes / Seconds
    public Duration(int totalSeconds)
    {
        if (totalSeconds < 0) totalSeconds = 0; // a duration can't be negative

        Hours = totalSeconds / 3600;
        Minutes = (totalSeconds % 3600) / 60;
        Seconds = totalSeconds % 60;
    }

    // ---- System.Object members ----
    public override string ToString()
    {
        if (Hours > 0)
            return $"Hours: {Hours}, Minutes :{Minutes}, Seconds :{Seconds}";
        return $"Minutes :{Minutes}, Seconds :{Seconds}";
    }

    public override bool Equals(object obj)
    {
        return obj is Duration d && TotalSeconds == d.TotalSeconds;
    }

    public override int GetHashCode()
    {
        return TotalSeconds.GetHashCode();
    }

    // ---- Operators overloading ----

    // D3 = D1 + D2
    public static Duration operator +(Duration a, Duration b)
    {
        return new Duration(a.TotalSeconds + b.TotalSeconds);
    }

    // D3 = D1 + 7800
    public static Duration operator +(Duration a, int seconds)
    {
        return new Duration(a.TotalSeconds + seconds);
    }

    // D3 = 666 + D3
    public static Duration operator +(int seconds, Duration a)
    {
        return new Duration(a.TotalSeconds + seconds);
    }

    // D1 = D1 - D2   (result never goes below zero)
    public static Duration operator -(Duration a, Duration b)
    {
        return new Duration(a.TotalSeconds - b.TotalSeconds);
    }

    // D3 = ++D1  (adds one minute)
    public static Duration operator ++(Duration a)
    {
        return new Duration(a.TotalSeconds + 60);
    }

    // D3 = --D2  (removes one minute)
    public static Duration operator --(Duration a)
    {
        return new Duration(a.TotalSeconds - 60);
    }

    // Comparison operators (must come in pairs)
    public static bool operator >(Duration a, Duration b)
    {
        return a.TotalSeconds > b.TotalSeconds;
    }

    public static bool operator <(Duration a, Duration b)
    {
        return a.TotalSeconds < b.TotalSeconds;
    }

    public static bool operator >=(Duration a, Duration b)
    {
        return a.TotalSeconds >= b.TotalSeconds;
    }

    public static bool operator <=(Duration a, Duration b)
    {
        return a.TotalSeconds <= b.TotalSeconds;
    }

    // == and != (also a pair, and match our Equals)
    public static bool operator ==(Duration a, Duration b)
    {
        if (ReferenceEquals(a, b)) return true;
        if (a is null || b is null) return false;
        return a.TotalSeconds == b.TotalSeconds;
    }

    public static bool operator !=(Duration a, Duration b)
    {
        return !(a == b);
    }

    // if (D1)  -> true when the duration is not zero
    public static bool operator true(Duration a)
    {
        return a.TotalSeconds > 0;
    }

    public static bool operator false(Duration a)
    {
        return a.TotalSeconds == 0;
    }

    // DateTime Obj = (DateTime) D1  -> today's date + this duration
    public static explicit operator DateTime(Duration a)
    {
        return DateTime.Today.Add(new TimeSpan(a.Hours, a.Minutes, a.Seconds));
    }
}

class Program
{
    static void Main()
    {
        // ---- Constructors / ToString ----
        Duration D1 = new Duration(1, 10, 15);
        Console.WriteLine(D1.ToString());   // Hours: 1, Minutes :10, Seconds :15

        D1 = new Duration(3600);
        Console.WriteLine(D1.ToString());   // Hours: 1, Minutes :0, Seconds :0

        Duration D2 = new Duration(7800);
        Console.WriteLine(D2.ToString());   // Hours: 2, Minutes :10, Seconds :0

        Duration D3 = new Duration(666);
        Console.WriteLine(D3.ToString());   // Minutes :11, Seconds :6

        // ---- Operators ----
        Console.WriteLine("\n--- Operators ---");
        D1 = new Duration(1, 10, 15);
        D2 = new Duration(7800);

        D3 = D1 + D2;
        Console.WriteLine("D1 + D2   : " + D3);

        D3 = D1 + 7800;
        Console.WriteLine("D1 + 7800 : " + D3);

        D3 = 666 + D3;
        Console.WriteLine("666 + D3  : " + D3);

        D3 = ++D1;
        Console.WriteLine("++D1      : " + D3 + "   (D1 is now: " + D1 + ")");

        D3 = --D2;
        Console.WriteLine("--D2      : " + D3 + "   (D2 is now: " + D2 + ")");

        D1 = new Duration(5, 0, 0);
        D1 = D1 - D2;
        Console.WriteLine("D1 - D2   : " + D1);

        // ---- Comparison ----
        Console.WriteLine("\n--- Comparison ---");
        if (D1 > D2)
            Console.WriteLine("D1 > D2");
        else
            Console.WriteLine("D1 is not greater than D2");

        if (D1 <= D2)
            Console.WriteLine("D1 <= D2");
        else
            Console.WriteLine("D1 is not less than or equal to D2");

        // ---- if (D1) ----
        if (D1)
            Console.WriteLine("D1 is not zero (true)");
        else
            Console.WriteLine("D1 is zero (false)");

        // ---- Equals ----
        Console.WriteLine("\nnew Duration(60).Equals(new Duration(0,1,0)): "
            + new Duration(60).Equals(new Duration(0, 1, 0)));

        // ---- Explicit conversion to DateTime ----
        DateTime obj = (DateTime)D1;
        Console.WriteLine("\n(DateTime) D1 = " + obj);
    }
}

