namespace UGF.DebugTools.Runtime
{
    public static class DebugGLUtility
    {
        // public static Vector3[] MultiplyByMatrix(Vector3[] points, Matrix4x4 matrix)
        // {
        //     for (int i = 0, count = points.Length; i < count; i++)
        //     {
        //         points[i] = matrix.MultiplyPoint3x4(points[i]);
        //     }
        //
        //     return points;
        // }
        //
        // public static Vector3[] CreateXArcPoints(float xpos, float ypos, float size, float height)
        // {
        //     return new[]
        //     {
        //         new Vector3(xpos, ypos, size),
        //         new Vector3(xpos, ypos, -size),
        //         new Vector3(0, height, 0)
        //     };
        // }
        //
        // public static Vector3[] CreateXQuadPoints(float xpos, float size)
        // {
        //     return new[]
        //     {
        //         new Vector3(xpos, -size, size),
        //         new Vector3(xpos, size, size),
        //         new Vector3(xpos, size, -size),
        //         new Vector3(xpos, -size, -size)
        //     };
        // }
        //
        // public static Vector3[] CreateYQuadPoints(float ypos, float size)
        // {
        //     return new[]
        //     {
        //         new Vector3(-size, ypos, size),
        //         new Vector3(size, ypos, size),
        //         new Vector3(size, ypos, -size),
        //         new Vector3(-size, ypos, -size)
        //     };
        // }
        //
        // public static Vector3[] CreateXCirclePoints(float xpos, int segments)
        // {
        //     var list = new List<Vector3>();
        //     float angle = 360f / segments;
        //
        //     for (float i = 0; i < 360f; i += angle)
        //     {
        //         var pos = Quaternion.Euler(i, 0, 0) * Vector3.up * 0.5f;
        //         pos.x = xpos;
        //         list.Add(pos);
        //     }
        //
        //     return list.ToArray();
        // }
        //
        // public static Vector3[] CreateYCirclePoints(float ypos, int segments)
        // {
        //     var list = new List<Vector3>();
        //     float angle = 360f / segments;
        //
        //     for (float i = 0; i < 360f; i += angle)
        //     {
        //         var pos = Quaternion.Euler(0, i, 0) * Vector3.forward * 0.5f;
        //         pos.y = ypos;
        //         list.Add(pos);
        //     }
        //
        //     return list.ToArray();
        // }
        //
        // public static Vector3[] CreateZCirclePoints(float zpos, int segments)
        // {
        //     var list = new List<Vector3>();
        //     float angle = 360f / segments;
        //
        //     for (float i = 0; i < 360f; i += angle)
        //     {
        //         var pos = Quaternion.Euler(0, 0, i) * Vector3.up * 0.5f;
        //         pos.z = zpos;
        //         list.Add(pos);
        //     }
        //
        //     return list.ToArray();
        // }
    }
}
