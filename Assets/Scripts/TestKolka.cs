using System;
using System.Collections.Generic;
using UnityEngine;


public class TestKolka : MonoBehaviour
{
    public class PlaneParams
    {
        public float a;
        public float b;
        public float c;
    }

    void Start()
    {
        print("cokolwiek");
        Debug.Log("cokolwiek2");
        Console.WriteLine("cokolwiek3");
        int n = 50;
        float r = 5;
        float thetaStep = (2 * Mathf.PI) / n;
        List<Vector3> points = new List<Vector3>(n);

        for (int i = 0; i < n; i++)
        {
            float theta = i * thetaStep;
            float x = r * Mathf.Cos(theta);
            float y = r * Mathf.Sin(theta);
            float z = UnityEngine.Random.Range(-0.1f, 0.1f);
            points.Add(new Vector3(x, y, z));
        }

        PlaneParams planeParams = LeastSquaresPlaneFit(points);

        Vector3 center = MeanPoint(points);

        List<float> distances = new List<float>(n);
        foreach (Vector3 point in points)
        {
            distances.Add(Vector3.Distance(point, center));
        }
        float mean_distance = Mean(distances);

        Vector3 normal_vector = new Vector3(planeParams.a, planeParams.b, -1).normalized;

        Vector3 axis_start = center - mean_distance * normal_vector;
        Vector3 axis_end = center + mean_distance * normal_vector;
        Debug.Log("Axis Start: " + axis_start);
        Debug.Log("Axis End: " + axis_end);
    }

    private PlaneParams LeastSquaresPlaneFit(List<Vector3> points)
    {
        // Assuming that the equation of the plane is ax + by + c = z.
        // We can find the parameters (a, b, c) by minimizing the sum of squares of the residuals.
        // Here we solve the system using the normal equations, which is not always numerically stable.
        // In a production environment, more stable methods like QR decomposition should be considered.

        float sum_x = 0;
        float sum_y = 0;
        float sum_z = 0;
        float sum_xx = 0;
        float sum_xy = 0;
        float sum_xz = 0;
        float sum_yy = 0;
        float sum_yz = 0;

        foreach (Vector3 point in points)
        {
            sum_x += point.x;
            sum_y += point.y;
            sum_z += point.z;
            sum_xx += point.x * point.x;
            sum_xy += point.x * point.y;
            sum_xz += point.x * point.z;
            sum_yy += point.y * point.y;
            sum_yz += point.y * point.z;
        }

        int n = points.Count;
        Matrix4x4 A = new Matrix4x4(
            new Vector4(sum_xx, sum_xy, sum_x, 0),
            new Vector4(sum_xy, sum_yy, sum_y, 0),
            new Vector4(sum_x, sum_y, n, 0),
            new Vector4(0, 0, 0, 0)
        );
        Vector3 b = new Vector3(sum_xz, sum_yz, sum_z);

        Vector3 planeParamsVec = A.inverse * b;
        return new PlaneParams { a = planeParamsVec.x, b = planeParamsVec.y, c = planeParamsVec.z };
    }

    private Vector3 MeanPoint(List<Vector3> points)
    {
        Vector3 sum = Vector3.zero;
        foreach (Vector3 point in points)
        {
            sum += point;
        }
        return sum / points.Count;
    }

    private float Mean(List<float> values)
    {
        float sum = 0;
        foreach (float value in values)
        {
            sum += value;
        }
        return sum / values.Count;
    }
}
