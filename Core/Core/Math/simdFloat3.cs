﻿using Unity.Mathematics;

namespace Latios
{
    public partial struct simdFloat3
    {
        internal float4x3 m_float3s;

        public float4 x
        {
            get { return m_float3s.c0; }
            set { m_float3s.c0 = value; }
        }
        public float4 y
        {
            get { return m_float3s.c1; }
            set { m_float3s.c1 = value; }
        }
        public float4 z
        {
            get { return m_float3s.c2; }
            set { m_float3s.c2 = value; }
        }

        public float3 a
        {
            get { return new float3(m_float3s.c0.x, m_float3s.c1.x, m_float3s.c2.x); }
            set { m_float3s.c0.x = value.x; m_float3s.c1.x = value.y; m_float3s.c2.x = value.z; }
        }
        public float3 b
        {
            get { return new float3(m_float3s.c0.y, m_float3s.c1.y, m_float3s.c2.y); }
            set { m_float3s.c0.y = value.x; m_float3s.c1.y = value.y; m_float3s.c2.y = value.z; }
        }
        public float3 c
        {
            get { return new float3(m_float3s.c0.z, m_float3s.c1.z, m_float3s.c2.z); }
            set { m_float3s.c0.z = value.x; m_float3s.c1.z = value.y; m_float3s.c2.z = value.z; }
        }
        public float3 d
        {
            get { return new float3(m_float3s.c0.w, m_float3s.c1.w, m_float3s.c2.w); }
            set { m_float3s.c0.w = value.x; m_float3s.c1.w = value.y; m_float3s.c2.w = value.z; }
        }

        public simdFloat3(float3 value)
        {
            m_float3s.c0 = value.xxxx;
            m_float3s.c1 = value.yyyy;
            m_float3s.c2 = value.zzzz;
        }

        public simdFloat3(float3 a, float3 b, float3 c, float3 d)
        {
            m_float3s.c0 = new float4(a.x, b.x, c.x, d.x);
            m_float3s.c1 = new float4(a.y, b.y, c.y, d.y);
            m_float3s.c2 = new float4(a.z, b.z, c.z, d.z);
        }

        public static simdFloat3 operator +(simdFloat3 lhs, float rhs)
        {
            return new simdFloat3 { m_float3s = lhs.m_float3s + rhs };
        }

        public static simdFloat3 operator +(float lhs, simdFloat3 rhs)
        {
            return new simdFloat3 { m_float3s = lhs + rhs.m_float3s };
        }

        public static simdFloat3 operator +(simdFloat3 lhs, float3 rhs)
        {
            return new simdFloat3 { m_float3s = new float4x3(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z) };
        }

        public static simdFloat3 operator +(float3 lhs, simdFloat3 rhs)
        {
            return new simdFloat3 { m_float3s = new float4x3(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z) };
        }

        public static simdFloat3 operator +(simdFloat3 lhs, simdFloat3 rhs)
        {
            return new simdFloat3 { m_float3s = lhs.m_float3s + rhs.m_float3s };
        }

        public static simdFloat3 operator -(simdFloat3 lhs, float rhs)
        {
            return new simdFloat3 { m_float3s = lhs.m_float3s - rhs };
        }

        public static simdFloat3 operator -(float lhs, simdFloat3 rhs)
        {
            return new simdFloat3 { m_float3s = lhs - rhs.m_float3s };
        }

        public static simdFloat3 operator -(simdFloat3 lhs, float3 rhs)
        {
            return new simdFloat3 { m_float3s = new float4x3(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z) };
        }

        public static simdFloat3 operator -(float3 lhs, simdFloat3 rhs)
        {
            return new simdFloat3 { m_float3s = new float4x3(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z) };
        }

        public static simdFloat3 operator -(simdFloat3 lhs, simdFloat3 rhs)
        {
            return new simdFloat3 { m_float3s = lhs.m_float3s - rhs.m_float3s };
        }

        public static simdFloat3 operator *(simdFloat3 lhs, float rhs)
        {
            return new simdFloat3 { m_float3s = lhs.m_float3s * rhs };
        }

        public static simdFloat3 operator *(float lhs, simdFloat3 rhs)
        {
            return new simdFloat3 { m_float3s = lhs * rhs.m_float3s };
        }

        public static simdFloat3 operator *(simdFloat3 lhs, float3 rhs)
        {
            return new simdFloat3 { m_float3s = new float4x3(lhs.x * rhs.x, lhs.y * rhs.y, lhs.z * rhs.z) };
        }

        public static simdFloat3 operator *(float3 lhs, simdFloat3 rhs)
        {
            return new simdFloat3 { m_float3s = new float4x3(lhs.x * rhs.x, lhs.y * rhs.y, lhs.z * rhs.z) };
        }

        public static simdFloat3 operator *(simdFloat3 lhs, simdFloat3 rhs)
        {
            return new simdFloat3 { m_float3s = lhs.m_float3s * rhs.m_float3s };
        }

        public static simdFloat3 operator /(simdFloat3 lhs, float rhs)
        {
            return new simdFloat3 { m_float3s = lhs.m_float3s / rhs };
        }

        public static simdFloat3 operator /(float lhs, simdFloat3 rhs)
        {
            return new simdFloat3 { m_float3s = lhs / rhs.m_float3s };
        }

        public static simdFloat3 operator /(simdFloat3 lhs, float3 rhs)
        {
            return new simdFloat3 { m_float3s = new float4x3(lhs.x / rhs.x, lhs.y / rhs.y, lhs.z / rhs.z) };
        }

        public static simdFloat3 operator /(float3 lhs, simdFloat3 rhs)
        {
            return new simdFloat3 { m_float3s = new float4x3(lhs.x / rhs.x, lhs.y / rhs.y, lhs.z / rhs.z) };
        }

        public static simdFloat3 operator /(simdFloat3 lhs, simdFloat3 rhs)
        {
            return new simdFloat3 { m_float3s = lhs.m_float3s / rhs.m_float3s };
        }

        public float3 this[int i]
        {
            get
            {
                switch(i)
                {
                    case 0: return a;
                    case 1: return b;
                    case 2: return c;
                    case 3: return d;
                    default: throw new System.InvalidOperationException("simdFloat3 indexer must be in the range of 0 - 3");
                }
            }
        }
    }
}

