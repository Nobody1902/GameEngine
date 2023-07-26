using System.Runtime.Serialization;

namespace GameEngine;

[Serializable]
public struct Vector2 : IEquatable<Vector2>, IEquatable<System.Numerics.Vector2>, ISerializable
{
    public float X;
    public float Y;

    public Vector2(float x, float y)
    {
        this.X = x;
        this.Y = y;
    }

    public override string ToString()
    {
        return $"Vector2({X},{Y})";
    }

    #region Operators

    static public implicit operator System.Numerics.Vector2(Vector2 vec)
    {
        return new(vec.X, vec.Y);
    }
    public static Vector2 operator +(Vector2 a, Vector2 b)
    {
        return new Vector2(a.X + b.X, a.Y + b.Y);
    }

    public static Vector2 operator -(Vector2 a, Vector2 b)
    {
        return new Vector2(a.X - b.X, a.Y - b.Y);
    }

    public static Vector2 operator *(Vector2 a, float d)
    {
        return new Vector2(a.X * d, a.Y * d);
    }
    public static Vector2 operator *(float d, Vector2 a)
    {
        return new Vector2(a.X * d, a.Y * d);
    }
    public static Vector2 operator *(Vector2 a, Vector2 b)
    {
        return new Vector2(a.X * b.X, a.Y * b.Y);
    }
    public static Vector2 operator /(Vector2 a, Vector2 b)
    {
        return new Vector2(a.X / b.X, a.Y / b.Y);
    }

    public static Vector2 operator /(Vector2 a, float d)
    {
        return new Vector2(a.X / d, a.Y / d);
    }

    public static bool operator ==(Vector2 lhs, Vector2 rhs)
    {
        return lhs.X == rhs.X && lhs.Y == rhs.Y;
    }

    public static bool operator !=(Vector2 lhs, Vector2 rhs)
    {
        return !(lhs == rhs);
    }
    public bool Equals(Vector2 other)
    {
        return X == other.X && Y == other.Y;
    }

    public override bool Equals(object other)
    {
        if (!(other is Vector2)) return false;

        Vector2 vector = (Vector2)other;
        return X.Equals(vector.X) && Y.Equals(vector.Y);
    }
    public bool Equals(System.Numerics.Vector2 other)
    {
        return X == other.X && (Y == other.Y);
    }

    public override int GetHashCode()
    {
        return X.GetHashCode() ^ Y.GetHashCode() << 2;
    }
    #endregion

    public float magnitude
    {
        get { return (float)Math.Sqrt(X * X + Y * Y); }
    }

    public void Normalize()
    {
        float mag = magnitude;
        if (mag > 0.0f)
        {
            X /= mag;
            Y /= mag;
        }
    }


    public static float Dot(Vector2 lhs, Vector2 rhs)
    {
        return lhs.X * rhs.X + lhs.Y * rhs.Y;
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("X", X);
        info.AddValue("Y", Y);
    }
    public Vector2(SerializationInfo info, StreamingContext context)
    {
        X = info.GetSingle("X");
        Y = info.GetSingle("Y");
    }

    #region Static Values
    public static Vector2 Zero
    {
        get { return new Vector2(0.0f, 0.0f); }
    }

    public static Vector2 One
    {
        get { return new Vector2(1.0f, 1.0f); }
    }

    public static Vector2 Up
    {
        get { return new Vector2(0.0f, 1.0f); }
    }

    public static Vector2 Down
    {
        get { return new Vector2(0.0f, -1.0f); }
    }

    public static Vector2 Left
    {
        get { return new Vector2(-1.0f, 0.0f); }
    }

    public static Vector2 Right
    {
        get { return new Vector2(1.0f, 0.0f); }
    }
    #endregion
}
