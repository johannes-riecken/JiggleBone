using System;
public class Transform {
    public Vector3 eulerAngles;
    public Vector3 position { get; set; }
    public Quaternion rotation { get; set; }
    public Vector3 localScale { get; set; }
    // Rotates the transform so the forward vector points at /worldPosition/.
    public void LookAt(Vector3 worldPosition, Vector3 worldUp) { Console.WriteLine("LookAt unimplemented"); }
    // public void LookAt(Vector3 worldPosition, Vector3 worldUp) {
    //     Matrix4f rot = Matrix4f.LookAt(position, worldPosition, worldUp).Inverse().ToRotation();
    //     SetRotation(rot);
    // }

    // Transforms /direction/ from local space to world space.
    public Vector3 TransformDirection(Vector3 direction) {
        throw new Exception("TransformDirection not implemented");
    }
    // public Vector3 TransformDirection(Vector3 direction)//local to global
    // {
    //     Vector3 res = Vector3.RotateAround(direction, Vector3.right, -eulerAngles.x);
    //     res = Vector3.RotateAround(res, Vector3.up, -eulerAngles.y);
    //     res = Vector3.RotateAround(res, Vector3.forward, -eulerAngles.z);
    //     return res;
    // }
}

// public class Matrix4f {
//     public float m00, m01, m02, m03, m10, m11, m12, m13, m20, m21, m22, m23, m30,
//            m31, m32, m33;
//     public static Matrix4f Rotate(float angle, Vector3 axis, Matrix4f src, Matrix4f dest) {
//         if (dest == null)
//             dest = new Matrix4f();
//         float c = (float) Math.Cos(angle);
//         float s = (float) Math.Sin(angle);
//         float oneminusc = 1.0f - c;
//         float xy = axis.x*axis.y;
//         float yz = axis.y*axis.z;
//         float xz = axis.x*axis.z;
//         float xs = axis.x*s;
//         float ys = axis.y*s;
//         float zs = axis.z*s;
// 
//         float f00 = axis.x*axis.x*oneminusc+c;
//         float f01 = xy*oneminusc+zs;
//         float f02 = xz*oneminusc-ys;
//         // n[3] not used
//         float f10 = xy*oneminusc-zs;
//         float f11 = axis.y*axis.y*oneminusc+c;
//         float f12 = yz*oneminusc+xs;
//         // n[7] not used
//         float f20 = xz*oneminusc+ys;
//         float f21 = yz*oneminusc-xs;
//         float f22 = axis.z*axis.z*oneminusc+c;
// 
//         float t00 = src.m00 * f00 + src.m10 * f01 + src.m20 * f02;
//         float t01 = src.m01 * f00 + src.m11 * f01 + src.m21 * f02;
//         float t02 = src.m02 * f00 + src.m12 * f01 + src.m22 * f02;
//         float t03 = src.m03 * f00 + src.m13 * f01 + src.m23 * f02;
//         float t10 = src.m00 * f10 + src.m10 * f11 + src.m20 * f12;
//         float t11 = src.m01 * f10 + src.m11 * f11 + src.m21 * f12;
//         float t12 = src.m02 * f10 + src.m12 * f11 + src.m22 * f12;
//         float t13 = src.m03 * f10 + src.m13 * f11 + src.m23 * f12;
//         dest.m20 = src.m00 * f20 + src.m10 * f21 + src.m20 * f22;
//         dest.m21 = src.m01 * f20 + src.m11 * f21 + src.m21 * f22;
//         dest.m22 = src.m02 * f20 + src.m12 * f21 + src.m22 * f22;
//         dest.m23 = src.m03 * f20 + src.m13 * f21 + src.m23 * f22;
//         dest.m00 = t00;
//         dest.m01 = t01;
//         dest.m02 = t02;
//         dest.m03 = t03;
//         dest.m10 = t10;
//         dest.m11 = t11;
//         dest.m12 = t12;
//         dest.m13 = t13;
//         return dest;
//     }
// 
// 	public Matrix4f LookAt(Vector3 eye, Vector3 target, Vector3 up)
// 	{
// 		Vector3 f = Vector3.Normalize(target - eye);
// 		Vector3 s = Vector3.Normalize(Vector3.Cross(up, f));
// 		Vector3 u = Vector3.Cross(f, s);
// 
// 		Matrix4f result = new Matrix4f();
// 		result.m00 = s.x;
// 		result.m01 = s.y;
// 		result.m02 = s.z;
// 		result.m10 = u.x;
// 		result.m11 = u.y;
// 		result.m12 = u.z;
// 		result.m20 = f.x;
// 		result.m21 = f.y;
// 		result.m22 = f.z;
// 		result.m03 = - Vector3.Dot(s, eye);
// 		result.m13 = - Vector3.Dot(u, eye);
// 		result.m23 = - Vector3.Dot(f, eye);
// 		return result;
// 	}
// 
// 	Matrix4f Inverse(Matrix4f m)
// 	{
// 		float p10_21 = m.m10 * m.m21 - m.m20 * m.m11;
// 		float p10_22 = m.m10 * m.m22 - m.m20 * m.m12;
// 		float p10_23 = m.m10 * m.m23 - m.m20 * m.m13;
// 		float p11_22 = m.m11 * m.m22 - m.m21 * m.m12;
// 		float p11_23 = m.m11 * m.m23 - m.m21 * m.m13;
// 		float p12_23 = m.m12 * m.m23 - m.m22 * m.m13;
// 		float p10_31 = m.m10 * m.m31 - m.m30 * m.m11;
// 		float p10_32 = m.m10 * m.m32 - m.m30 * m.m12;
// 		float p10_33 = m.m10 * m.m33 - m.m30 * m.m13;
// 		float p11_32 = m.m11 * m.m32 - m.m31 * m.m12;
// 		float p11_33 = m.m11 * m.m33 - m.m31 * m.m13;
// 		float p12_33 = m.m12 * m.m33 - m.m32 * m.m13;
// 		float p20_31 = m.m20 * m.m31 - m.m30 * m.m21;
// 		float p20_32 = m.m20 * m.m32 - m.m30 * m.m22;
// 		float p20_33 = m.m20 * m.m33 - m.m30 * m.m23;
// 		float p21_32 = m.m21 * m.m32 - m.m31 * m.m22;
// 		float p21_33 = m.m21 * m.m33 - m.m31 * m.m23;
// 		float p22_33 = m.m22 * m.m33 - m.m32 * m.m23;
// 		Matrix4f A;
// 		A.m00 = +(m.m11 * p22_33 - m.m12 * p21_33 + m.m13 * p21_32);
// 		A.m10 = -(m.m10 * p22_33 - m.m12 * p20_33 + m.m13 * p20_32);
// 		A.m20 = +(m.m10 * p21_33 - m.m11 * p20_33 + m.m13 * p20_31);
// 		A.m30 = -(m.m10 * p21_32 - m.m11 * p20_32 + m.m12 * p20_31);
// 		A.m01 = -(m.m01 * p22_33 - m.m02 * p21_33 + m.m03 * p21_32);
// 		A.m11 = +(m.m00 * p22_33 - m.m02 * p20_33 + m.m03 * p20_32);
// 		A.m21 = -(m.m00 * p21_33 - m.m01 * p20_33 + m.m03 * p20_31);
// 		A.m31 = +(m.m00 * p21_32 - m.m01 * p20_32 + m.m02 * p20_31);
// 		A.m02 = +(m.m01 * p12_33 - m.m02 * p11_33 + m.m03 * p11_32);
// 		A.m12 = -(m.m00 * p12_33 - m.m02 * p10_33 + m.m03 * p10_32);
// 		A.m22 = +(m.m00 * p11_33 - m.m01 * p10_33 + m.m03 * p10_31);
// 		A.m32 = -(m.m00 * p11_32 - m.m01 * p10_32 + m.m02 * p10_31);
// 		A.m03 = -(m.m01 * p12_23 - m.m02 * p11_23 + m.m03 * p11_22);
// 		A.m13 = +(m.m00 * p12_23 - m.m02 * p10_23 + m.m03 * p10_22);
// 		A.m23 = -(m.m00 * p11_23 - m.m01 * p10_23 + m.m03 * p10_21);
// 		A.m33 = +(m.m00 * p11_22 - m.m01 * p10_22 + m.m02 * p10_21);
// 
// 		float det = 0;
// 		det += m.m00 * A.m00;
// 		det += m.m01 * A.m10;
// 		det += m.m02 * A.m20;
// 		det += m.m03 * A.m30;
// 		float inv_det = 1.f / det;
//         A.m00 *= inv_det;
//         A.m10 *= inv_det;
//         A.m20 *= inv_det;
//         A.m30 *= inv_det;
//         A.m01 *= inv_det;
//         A.m11 *= inv_det;
//         A.m21 *= inv_det;
//         A.m31 *= inv_det;
//         A.m02 *= inv_det;
//         A.m12 *= inv_det;
//         A.m22 *= inv_det;
//         A.m32 *= inv_det;
//         A.m03 *= inv_det;
//         A.m13 *= inv_det;
//         A.m23 *= inv_det;
//         A.m33 *= inv_det;
// 		return A;
// 	}
// 
// 	Quaternion ToRotation()
// 	{
// 		Quaternion rot;
// 		Matrix4x4.Decompose(*this, nullptr, &rot, nullptr);
// 		return rot;
// 	}
// 
// 	void Decompose(
// 		Matrix4f    transformation, 
// 		ref Vector3            outTranslation,
// 		ref Quaternion         outRotation, 
// 		ref Vector3            outScale)
// 	{
// 		Matrix4f m = transformation;
// 
// 		if (outTranslation != null)
// 			outTranslation.Set(m.m03, m.m13, m.m23);
// 
// 		float sx, sy, sz;
// 		sx = Math.Sqrt(m.m00*m.m00 + m.m10*m.m10 + m.m20*m.m20);
// 		sy = Math.Sqrt(m.m01*m.m01 + m.m11*m.m11 + m.m21*m.m21);
// 		sz = Math.Sqrt(m.m02*m.m02 + m.m12*m.m12 + m.m22*m.m22);
// 		if (outScale != null)
// 		{
// 			outScale.Set(sx, sy, sz);
// 		}
// 
// 		if (outRotation != null)
// 		{
// 			Matrix4x4 rot_mat;
// 			rot_mat.m00 = m.m00 / sx;
// 			rot_mat.m10 = m.m10 / sx;
// 			rot_mat.m20 = m.m20 / sx;
// 			rot_mat.m30 = 0;
// 			rot_mat.m01 = m.m01 / sy;
// 			rot_mat.m11 = m.m11 / sy;
// 			rot_mat.m21 = m.m21 / sy;
// 			rot_mat.m31 = 0;
// 			rot_mat.m02 = m.m02 / sz;
// 			rot_mat.m12 = m.m12 / sz;
// 			rot_mat.m22 = m.m22 / sz;
// 			rot_mat.m32 = 0;
// 			rot_mat.m03 = 0;
// 			rot_mat.m13 = 0;
// 			rot_mat.m23 = 0;
// 			rot_mat.m33 = 1;
// 			outRotation = rot_mat.ToRotation_fast();
// 		}
// 	}
// }
