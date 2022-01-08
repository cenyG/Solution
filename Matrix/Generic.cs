namespace Matrix;

public static class Generic
{
    public static T Add<T>(T a, T b) where T: struct
    {
        return typeof(T).Name switch
        {
            "Int32" => (T) (object) ((int) (object) a + (int) (object) b),
            "Double" => (T) (object) ((double) (object) a + (double) (object) b),
            "Boolean" => (T) (object) ((bool) (object) a || (bool) (object) b),
            _ => default(T)
        };
    }
    
    public static T Mul<T>(T a, T b) where T: struct
    {
        return typeof(T).Name switch
        {
            "Int32" => (T) (object) ((int) (object) a * (int) (object) b),
            "Double" => (T) (object) ((double) (object) a * (double) (object) b),
            "Boolean" => (T) (object) ((bool) (object) a && (bool) (object) b),
            _ => default(T)
        };
    }
    
    public static bool IsNil<T>(T a) where T: struct
    {
        return typeof(T).Name switch
        {
            "Int32" => (int) (object) a == 0,
            "Double" => (double) (object) a == 0.0,
            "Boolean" => (bool) (object) a == false,
            _ => false
        };
    }
    
    public static double ToDouble<T>(T a) where T: struct
    {
        return typeof(T).Name switch
        {
            "Int32" => Convert.ToDouble((int) (object) a),
            "Double" => (double) (object) a,
            "Boolean" => Convert.ToDouble((bool) (object) a),
            _ => 0.0
        };
    }
}