using System;
using UnityEngine;
using UnityEngine.XR;
using Oculus.Interaction.Samples;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
// using namespace Oculus.Interaction.Samples

public class VRDebug : MonoBehaviour
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

    

    // Start is called before the first frame update
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

        GameObject bananaMan = GameObject.Find("Banana Man");

        Transform leftForearm = bananaMan.transform.Find("Armature/Hips/Spine 1/Spine 2/Spine 3/Left Shoulder/Left Arm/Left Forearm");
        this.head = bananaMan.transform.Find("Armature/Hips/Spine 1/Spine 2/Spine 3/Neck/Head");
    }

    // Update is called once per frame

    void Update()
    {



        // // if(OVRInput.GetDown(OVRInput.Button.Four))
        // // {
        // //     UIActive = !UIActive;
        // //     UI.SetActive(UIActive);
        // // }
        // if(UIActive)
        // {
        //     // UI.transform.position = UIAnchor.transform.position;
        //     // UI.transform.eulerAngles = new Vector3(UIAnchor.transform.eulerAngles.x,UIAnchor.transform.eulerAngles.y,0);
        //
        // }






        if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x != 0f || OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y != 0f)
        {
    
            Vector3 temp = this.head.localScale;
            temp.x += OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x;
            temp.y += OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y;
            this.head.localScale = temp;
        }




        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            if (_inputData._rightController.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 rightData))
            {
                
                //test banana
                GameObject bananaMan = GameObject.Find("Banana Man");

                // Znajdź obiekt Left Forearm wewnątrz Banana Man
                Transform leftForearm = bananaMan.transform.Find("Armature/Hips/Spine 1/Spine 2/Spine 3/Left Shoulder/Left Arm/Left Forearm");

                // Znajdź komponent Constant Rotation na obiekcie Left Forearm
                ConstantRotation rotation = leftForearm.GetComponent<ConstantRotation>();

                // Zmniejsz prędkość obrotu
                rotation.RotationSpeed += 1000.0f;
                //--------------
                
                
                if (isGrowth.Equals(false))
                {
                    Debug.Log("Wykonywnie pomiaru w osi Y");
                    isGrowth = true;
                    growth1.x = rightData.x;
                    growth1.y = rightData.y;
                    growth1.z = rightData.z;
                    Debug.Log("Pierwsza wartość " + growth1.y);
                }
                else
                {
                    isGrowth = false;
                    growth2.x = rightData.x;
                    growth2.y = rightData.y;
                    growth2.z = rightData.z;
                    Debug.Log("Druga wartość " + growth2.y);
                    float distance = Math.Abs(growth1.y - growth2.y);
                    Debug.Log("Wynik pomiaru: " + distance);
                }
                
                //od tąd jest test banana
                // Rigidbody rb = GameObject.Find("MyObject").GetComponent<Rigidbody>();
                
                
                // float x = rightData.x;
                // float y = rightData.y;
                // float z = rightData.z;
                
                // Debug.Log("right hand x = " + x2 + ",y = " + y2 + ",z = " + z2);
            }
            // Debug.Log("Right trigger pressed.");
        }

        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            if (_inputData._rightController.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 rightData))
            {
                if (firstSet.Equals(false))
                {
                    firstSet = true;
                    x1 = rightData.x;
                    y1 = rightData.y;
                    z1 = rightData.z;
                    Debug.Log("Punkt pierwszy x = " + x1 + ",y = " + y1 + ",z = " + z1);
                }
                else
                {
                    firstSet = false;
                    x2 = rightData.x;
                    y2 = rightData.y;
                    z2 = rightData.z;
                    Debug.Log("Punkt drugi x = " + x2 + ",y = " + y2 + ",z = " + z2);
                    float distance = Mathf.Sqrt(Mathf.Pow((x2 - x1), 2) + Mathf.Pow((y2 - y1), 2) + Mathf.Pow((z2 - z1), 2));
                    Debug.Log("Odległość między punktami: " + distance);
                }
                // float x = rightData.x;
                // float y = rightData.y;
                // float z = rightData.z;
                
                // Debug.Log("right hand x = " + x2 + ",y = " + y2 + ",z = " + z2);
            }
        }



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
                    await Task.Delay(10);
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

        // Przykładowe wywołanie metod
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))
        {
            Debug.Log("Command_Clear");
            StartRecordingPath();
        }

        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger))
        {
            StopRecordingPath();
        }



        //if(OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger)) 
        //{
        //    Debug.Log("Command_Clear");

        //    GameObject controller = GameObject.Find("OVRPlayerController");
        //    Transform hand = controller.transform.Find("OVRCameraRig/TrackingSpace/RightHandAnchor");
        //    Transform eye = controller.transform.Find("OVRCameraRig/TrackingSpace/CenterEyeAnchor");

        //    if (this.pathBool.Equals(false))
        //    {
        //        this.pathBool = true;
        //        this.path.Clear();
        //        this.pathThread = new Thread(() =>
        //        {
        //            while (this.pathBool.Equals(true))
        //            {
        //                Vector3 vector3 = new Vector3(hand.position.x, hand.position.y, hand.position.z);
        //                if (_inputData._rightController.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 rightData))
        //                {
        //                    GameObject pathElement = GameObject.CreatePrimitive(PrimitiveType.Sphere); // utwórz obiekt kuli
        //                    pathElement.GetComponent<Collider>().enabled = false;
        //                    pathElement.transform.position = vector3; // ustaw pozycję kuli na centrum
        //                    pathElement.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f); // ustaw promień kuli
        //                    this.path.Add(pathElement);
        //                    Thread.Sleep(333);
        //                }
        //            }
        //            this.pathBool = false;
        //            return;
        //        });
        //        this.pathThread.Start();
        //    }

        //}

        //if(OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger))
        //{
        //    this.pathBool = false;
        //    this.pathThread.Abort();
        //}
    }
    
    
   
}
