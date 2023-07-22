namespace GameEngine;

using System;
using System.Runtime.Serialization;

[Serializable]
public struct Vector3 : IEquatable<Vector3>, IEquatable<System.Numerics.Vector3>, ISerializable
{
    public float X;
    public float Y;
    public float Z;

    public Vector3(float x, float y, float z)
    {
        this.X = x;
        this.Y = y;
        this.Z = z;
    }
    public Vector3()
    {

    }
    public Vector3(float v)
    {
        this.X = v;
        this.Y = v;
        this.Z = v;
    }

    public override string ToString()
    {
        return $"Vector3({X},{Y},{Z})";
    }

    public float magnitude
    {
        get { return (float)Math.Sqrt(X * X + Y * Y + Z * Z); }
    }

    public void Normalize()
    {
        float mag = magnitude;
        if (mag > 0.0f)
        {
            X /= mag;
            Y /= mag;
            Z /= mag;
        }
    }

    public static float Dot(Vector3 lhs, Vector3 rhs)
    {
        return lhs.X * rhs.X + lhs.Y * rhs.Y + lhs.Z * rhs.Z;
    }

    public static Vector3 Cross(Vector3 lhs, Vector3 rhs)
    {
        return new Vector3(lhs.Y * rhs.Z - lhs.Z * rhs.Y,
                           lhs.Z * rhs.X - lhs.X * rhs.Z,
                           lhs.X * rhs.Y - lhs.Y * rhs.X);
    }


    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("X", X);
        info.AddValue("Y", Y);
        info.AddValue("Z", Z);
    }
    public Vector3(SerializationInfo info, StreamingContext context)
    {
        X = info.GetSingle("X");
        Y = info.GetSingle("Y");
        Z = info.GetSingle("Z");
    }

    #region Operators
    public override int GetHashCode()
    {
        return X.GetHashCode() ^ Y.GetHashCode() << 2 ^ Z.GetHashCode() >> 2;
    }
    static public implicit operator System.Numerics.Vector3(Vector3 vec)
    {
        return new(vec.X, vec.Y, vec.Z);
    }
    public static Vector3 operator +(Vector3 a, Vector3 b)
    {
        return new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    }

    public static Vector3 operator -(Vector3 a, Vector3 b)
    {
        return new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
    }
    public static Vector3 operator -(Vector3 a)
    {
        return new(-a.X, -a.Y, -a.Z);
    }

    public static Vector3 operator *(Vector3 a, float d)
    {
        return new Vector3(a.X * d, a.Y * d, a.Z * d);
    }
    public static Vector3 operator *(Vector3 a, Vector3 b)
    {
        return new Vector3(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
    }

    public static Vector3 operator /(Vector3 a, Vector3 b)
    {
        return new Vector3(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
    }

    public static Vector3 operator /(Vector3 a, float d)
    {
        return new Vector3(a.X / d, a.Y / d, a.Z / d);
    }

    public static bool operator ==(Vector3 lhs, Vector3 rhs)
    {
        return lhs.X == rhs.X && lhs.Y == rhs.Y && lhs.Z == rhs.Z;
    }

    public static bool operator !=(Vector3 lhs, Vector3 rhs)
    {
        return !(lhs == rhs);
    }

    public override bool Equals(object other)
    {
        if (other is not Vector3) return false;

        Vector3 vector = (Vector3)other;
        return X.Equals(vector.X) && Y.Equals(vector.Y) && Z.Equals(vector.Z);
    }

    public bool Equals(Vector3 other)
    {
        return X == other.X && Y == other.Y && Z == other.Z;
    }

    public bool Equals(System.Numerics.Vector3 other)
    {
        return X == other.X && Y == other.Y && Z == other.Z;
    }

    #endregion

    #region Static Values
    public static Vector3 Zero
    {
        get { return new Vector3(0.0f, 0.0f, 0.0f); }
    }

    public static Vector3 One
    {
        get { return new Vector3(1.0f, 1.0f, 1.0f); }
    }

    public static Vector3 Forward
    {
        get { return new Vector3(0.0f, 0.0f, 1.0f); }
    }

    public static Vector3 Back
    {
        get { return new Vector3(0.0f, 0.0f, -1.0f); }
    }

    public static Vector3 Up
    {
        get { return new Vector3(0.0f, 1.0f, 0.0f); }
    }

    public static Vector3 Down
    {
        get { return new Vector3(0.0f, -1.0f, 0.0f); }
    }

    public static Vector3 Left
    {
        get { return new Vector3(-1.0f, 0.0f, 0.0f); }
    }

    public static Vector3 Right
    {
        get { return new Vector3(1.0f, 0.0f, 0.0f); }
    }
    #endregion
}
