using System;
using UnityEngine;
using UnityEngine.XR;
using Oculus.Interaction.Samples;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class VRDebugRescale : MonoBehaviour
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

    float multiplier = 1.0f;
    float multiplierHand = 1.0f;
    float startScale = 1.5f;
    private Vector3 originalScale = new Vector3(18.10255f,18.10255f,18.10255f);//default values for banana man
    private RescaleMenuController menuController;
    private int lastMenuOption = 0;

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

    private void Awake()
    {
        menuController = FindObjectOfType<RescaleMenuController>();
    }
    void Update()
    {
        //if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x != 0f || OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y != 0f)
        //{
    
        //    Vector3 temp = this.head.localScale;
        //    temp.x += OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x;
        //    temp.y += OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y;
        //    this.head.localScale = temp;
        //}

        if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x > 0.5f && wasTriggerMoved.Equals(false))
        {
           
            this.menuList.nextElement();
            // this.menuList.ShowBodyPartMeasurement();
            this.wasTriggerMoved = true;
        }

        if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x < -0.5f && wasTriggerMoved.Equals(false))
        {
            
            this.menuList.previousElement();
            // this.menuList.ShowBodyPartMeasurement();
            this.wasTriggerMoved = true;

        }

        if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x < 0.5f && OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x > -0.5f && wasTriggerMoved.Equals(true))
        {
            wasTriggerMoved = false;
        }




        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            if (_inputData._rightController.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 rightData))
            {
                int currentOption = menuController.GetCurrentOption();
                Debug.Log("Obecnie wybrana opcja: " + currentOption);

                if (currentOption != lastMenuOption)
                {
                    firstSet = false;
                }
                
                switch (currentOption)
                {
                    case 0:
                    {//pomiar wzrostu - skalujemy wszystko
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

                            float distance = Math.Abs(y1 - y2);
                            // Debug.Log("Odległość między punktami: " + distance);
                            if (this.menuList.startMeasure == false)
                            {
                                GameObject model = GameObject.Find("UserModel");
                                Transform armature = model.transform.Find("Armature");
                                Vector3 currentScaleArmature = armature.localScale;
                        
                                multiplier = 1.0f *distance/1.5f;//1.5f dlatego że zakładamy że rozmiar początkowy modelu wynosi 1.5m wizualnie ponieważ umożliwia to komfortowe korzystanie z aplikacji dla modelów większych do ok 3m.
                                // Debug.Log("Multiplier: " + multiplier);
                                // Debug.Log("Armature: " + currentScaleArmature);
                                currentScaleArmature = originalScale*multiplier;
                                armature.transform.localScale = currentScaleArmature;
          
                            }
                        }
                        break;
                    }
                    case 1:
                    {//pomiar ręki lewej
                        if (firstSet.Equals(false))
                        {
                            firstSet = true;
                            x1 = rightData.x;
                            y1 = rightData.y;
                            z1 = rightData.z;
                            // Debug.Log("Punkt pierwszy x = " + x1 + ",y = " + y1 + ",z = " + z1);
                            Debug.Log("Teraz przyłóż kontroler na koniec lewej ręki");
                        }
                        else
                        {
                            firstSet = false;
                            x2 = rightData.x;
                            y2 = rightData.y;
                            z2 = rightData.z;

                            float distance = Mathf.Sqrt(Mathf.Pow((x2 - x1), 2) + Mathf.Pow((y2 - y1), 2) + Mathf.Pow((z2 - z1), 2));
                            // Debug.Log("Odległość między punktami: " + distance);
                            
                            // float distance2 = Mathf.Sqrt(Mathf.Pow((7.45058e-11f - 0.0001550801f), 2) + Mathf.Pow((0.01007542f - 0.005770001f), 2) + Mathf.Pow((2.114102e-09f - 0.001264061f), 2));
                            // Debug.Log("Odległość między punktami2: " + distance2);//
                            if (this.menuList.startMeasure == false)
                            {
                                GameObject model = GameObject.Find("UserModel");
                                Transform leftHand = model.transform.Find("Armature/Hips/Spine 1/Spine 2/Spine 3/Left Shoulder");
                                Vector3 currentScaleLeftHand = leftHand.localScale;
                                // Debug.Log("Local scale:"+currentScaleLeftHand);
                                // Debug.Log("Local scalev2:"+leftHand.lossyScale);
                                multiplierHand =distance/0.5843f;//0.584f to rozmiar ręki dla człowieka 1.5m
                                Vector3 originalScaleHand = new Vector3(1.0f, 1.0f, 1.0f)/multiplier;
                                // Debug.Log("Multiplier: " + multiplier);
                                // Debug.Log("Armature: " + currentScaleLeftHand);
                                currentScaleLeftHand = originalScaleHand*multiplierHand;
                                leftHand.transform.localScale = currentScaleLeftHand;
                                Debug.Log("Długość lewej ręki wynosi: " + distance);

                            }
                        }
                        break;
                        
                    }
                    case 2:
                    {//pomiar prawej ręki
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

                            float distance = Mathf.Sqrt(Mathf.Pow((x2 - x1), 2) + Mathf.Pow((y2 - y1), 2) + Mathf.Pow((z2 - z1), 2));
                            
                            // float distance2 = Mathf.Sqrt(Mathf.Pow((7.45058e-11f - 0.0001550801f), 2) + Mathf.Pow((0.01007542f - 0.005770001f), 2) + Mathf.Pow((2.114102e-09f - 0.001264061f), 2));
                            // Debug.Log("Odległość między punktami2: " + distance2);//
                            if (this.menuList.startMeasure == false)
                            {
                                GameObject model = GameObject.Find("UserModel");
                                Transform rightHand = model.transform.Find("Armature/Hips/Spine 1/Spine 2/Spine 3/Right Shoulder");
                                Vector3 currentScaleLeftHand = rightHand.localScale;
                                // Debug.Log("Local scale:"+currentScaleLeftHand);
                                // Debug.Log("Local scalev2:"+rightHand.lossyScale);
                                multiplierHand =distance/0.5843f;//0.584f to rozmiar ręki dla człowieka 1.5m
                                Vector3 originalScaleHand = new Vector3(1.0f, 1.0f, 1.0f)/multiplier;
                                // Debug.Log("Multiplier: " + multiplier);
                                // Debug.Log("Armature: " + currentScaleLeftHand);
                                currentScaleLeftHand = originalScaleHand*multiplierHand;
                                rightHand.transform.localScale = currentScaleLeftHand;
                                Debug.Log("Długość prawej ręki wynosi: " + distance);

                            }
                        }
                        break;
                        
                    }
                    
                }

                lastMenuOption = currentOption;

            }
        }

        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            if (_inputData._rightController.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 rightData))
            {

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
                // Vector3 vector3 = new Vector3(hand.position.x, hand.position.y, hand.position.z);
                Vector3 vector3 = new Vector3(hand.position.x, hand.position.y, hand.position.z);//multiplier skala rozmiarowa na podstawie kalibracji
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
    }
}
