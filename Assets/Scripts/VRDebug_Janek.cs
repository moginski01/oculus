using System;
using UnityEngine;
using UnityEngine.XR;
using Oculus.Interaction.Samples;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine.UI;

public class VRDebug_Janek : MonoBehaviour
{
    //to jest test do banana
    // Referencja do obiektu Banana Man
    private GameObject bananaMan;

    // Referencje do komponentów Banana Man
    private Animator animator;
    private Rigidbody rigidbody;
    //--------------------------------------------
    public GameObject UI;
    public GameObject UIAnchor;
    private bool UIActive;
    private InputData _inputData;
    private bool firstSet = false;
    private bool isGrowth = false;
    private float x1, y1, z1;
    private float x2, y2, z2;
    private Vector3 growth1;
    private Vector3 growth2;

    private bool pathBool = false;
    private List<GameObject> path = new List<GameObject>();
    private Thread pathThread = null;
    private CancellationTokenSource cancellationTokenSource;

    private Transform head = null;
    private ModelScale modelScale = null;
    private MenuList menuList = null;
    private Boolean wasTriggerMoved = false;
    
    private List<Vector3> arc = new List<Vector3>();


    void Start()
    {
        UI.SetActive(true);
        UIActive = true;
        _inputData = GetComponent<InputData>();
        
        x1 = 0.0F;
        y1 = 0.0F;
        z1 = 0.0F;
        x2 = 0.0F;
        y2 = 0.0F;
        z2 = 0.0F;
        this.menuList = new MenuList();
        this.modelScale = new ModelScale();
    }


    void Update()
    {
        



        async void StartRecordingPath()
        {
            GameObject controller = GameObject.Find("OVRPlayerController");
            Transform hand = controller.transform.Find("OVRCameraRig/TrackingSpace/RightHandAnchor");

            foreach(GameObject pathElement in this.path)
            {
                Destroy(pathElement);
            }
            this.path.Clear();
            cancellationTokenSource = new CancellationTokenSource();

            string dateTimeString = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
            string fileName = "hand_positions_" + dateTimeString + ".txt";
            fileName = Path.Combine(Application.persistentDataPath, fileName);
            Debug.Log(fileName);

            var writer = new StreamWriter(fileName, true);
            
            while (!cancellationTokenSource.IsCancellationRequested)
            {
                Vector3 vector3 = new Vector3(hand.position.x, hand.position.y, hand.position.z);
                if (_inputData._rightController.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 rightData))
                {
                    GameObject pathElement = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    pathElement.GetComponent<Collider>().enabled = false;
                    pathElement.transform.position = vector3;
                    pathElement.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                    path.Add(pathElement);
                    writer.Write(vector3.x + " " + vector3.y + " " + vector3.z + "\n");
                    this.arc.Add(vector3);
                    await Task.Delay(3);
                }
            }
        }

        void StopRecordingPath()
        {
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
            }
        }
        
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))
        {
            Debug.Log("Command_Clear");
            StartRecordingPath();
        }

        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger))
        {
            StopRecordingPath();
            var circleParams = FitCircle(this.arc);
            var circleCenter = new Vector3((float)circleParams[0], (float)circleParams[1], (float)circleParams[2]);
            var radius = circleParams[3];
            var arcLength = CalculateArcLength(this.arc);
            var arcAngle = CalculateArcAngle(arcLength, (float)radius);
            this.arc.Clear();
            Debug.Log($"Długość łuku: {arcLength.ToString()}, Kąt: {RadiansToDegrees(arcAngle).ToString()}");
        }
        
        
    }
    
    private static List<Vector3> LoadPointsFromFile(string fileName)
    {
        var points = new List<Vector3>();
        var lines = File.ReadLines(fileName);
        foreach (var line in lines)
        {
            var parts = line.Split(' ');
            points.Add(new Vector3(float.Parse(parts[0]), float.Parse(parts[1]), float.Parse(parts[2])));
        }
        return points;
    }

    private static double[] FitCircle(List<Vector3> points)
    {
        var center = new Vector3(points.Average(p => p.x), points.Average(p => p.y), points.Average(p => p.z));
        var radius = points.Average(p => Vector3.Distance(p, center));
        double[] initialGuess = new[] { (double)center.x, center.y, center.z, radius };

        var state = new alglib.minlmstate();
        var rep = new alglib.minlmreport();
        double epsx = 0.000001;
        int maxits = 0;
        alglib.minlmcreatev(points.Count, initialGuess, 0.0001, out state);
        alglib.minlmsetcond(state, epsx, maxits);
        alglib.minlmoptimize(state, CalculateResiduals, null, points);
        alglib.minlmresults(state, out initialGuess, out rep);
        return initialGuess;
    }

    private static void CalculateResiduals(double[] x, double[] fi, object obj)
    {
        var points = obj as List<Vector3>;
        for (int i = 0; i < points.Count; i++)
        {
            var point = points[i];
            var center = new Vector3((float)x[0], (float)x[1], (float)x[2]);
            var radius = (float)x[3];
            fi[i] = Vector3.Distance(point, center) - radius;
        }
    }

    private static float CalculateArcLength(List<Vector3> points)
    {
        float length = 0;
        for (int i = 0; i < points.Count - 1; i++)
        {
            length += Vector3.Distance(points[i], points[i + 1]);
        }
        return length;
    }

    private static float CalculateArcAngle(float arcLength, float radius)
    {
        return arcLength / radius;
    }

    private static float RadiansToDegrees(float radians)
    {
        return radians * (180.0f / (float)Math.PI);
    }
}
