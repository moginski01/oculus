using System;
using UnityEngine;
using UnityEngine.XR;
using Oculus.Interaction.Samples;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class MenuList
{
    private List<ModelScale.BodyPart> bodyParts = new List<ModelScale.BodyPart>();
    private int currentIndex = 0;
    public bool startMeasure = false;
    public int measureStep = 0;
    public List<Vector3> vectorList = new List<Vector3>();

    public MenuList()
    {
        bodyParts.Add(ModelScale.BodyPart.None);
        bodyParts.Add(ModelScale.BodyPart.RightArm);
        bodyParts.Add(ModelScale.BodyPart.LeftArm);
        bodyParts.Add(ModelScale.BodyPart.RightLeg);
        bodyParts.Add(ModelScale.BodyPart.LeftLeg);
        bodyParts.Add(ModelScale.BodyPart.Body);
    }

    public void nextElement()
    {
        if (currentIndex < bodyParts.Count - 1)
        {
            currentIndex++;
        } else
        {
            currentIndex = 0;
        }
    }

    public void previousElement()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
        }
        else
        {
            currentIndex = bodyParts.Count - 1;
        }
    }

    public void ShowBodyPartMeasurement()
    {
        switch (this.bodyParts[this.currentIndex])
        {
            case ModelScale.BodyPart.RightArm:
                Debug.Log("Wybrałeś pomiar prawej ręki. Wciśnij A, aby zaakceptować wybór.");
                break;
            case ModelScale.BodyPart.LeftArm:
                Debug.Log("Wybrałeś pomiar lewej ręki. Wciśnij A, aby zaakceptować wybór.");
                break;
            case ModelScale.BodyPart.RightLeg:
                Debug.Log("Wybrałeś pomiar prawej nogi. Wciśnij A, aby zaakceptować wybór.");
                break;
            case ModelScale.BodyPart.LeftLeg:
                Debug.Log("Wybrałeś pomiar lewej nogi. Wciśnij A, aby zaakceptować wybór.");
                break;
            case ModelScale.BodyPart.Body:
                Debug.Log("Wybrałeś pomiar ciała. Dotknij kostki kontrolerem i naciśnij A, aby rozpocząć pomiar.");
                break;
            case ModelScale.BodyPart.None:
                Debug.Log("Nie wybrałeś żadnej opcji. Wciśnij A, aby zaakceptować wybór.");
                break;
            default:
                Debug.Log("Nieznana opcja. Wciśnij A, aby zaakceptować wybór.");
                break;
        }
    }

    public ModelScale.BodyPart GetBodyPart()
    {
        return bodyParts[currentIndex];
    }

}

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
    private ModelScale modelScale = null;
    private MenuList menuList = null;
    private Boolean wasTriggerMoved = false;


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
            this.menuList.ShowBodyPartMeasurement();
            this.wasTriggerMoved = true;


        }

        if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x < -0.5f && wasTriggerMoved.Equals(false))
        {
            
            this.menuList.previousElement();
            this.menuList.ShowBodyPartMeasurement();
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
                //GameObject man = GameObject.Find("HumanMale_Character_FREE");
                //Transform hip = man.transform.Find("Armature/Root_M/Hip_R");
                //Vector3 worldPosition = hip.TransformPoint(Vector3.zero);
                //worldPosition += new Vector3(0, 0, 0.01f);
                //hip.position = worldPosition;


                if (this.menuList.startMeasure == false)
                {
                    switch (this.menuList.GetBodyPart())
                    {
                        case ModelScale.BodyPart.RightArm:
                            Debug.Log("Wybrałeś pomiar prawej ręki. Wciśnij A, aby zaakceptować wybór.");
                            break;
                        case ModelScale.BodyPart.LeftArm:
                            Debug.Log("Wybrałeś pomiar lewej ręki. Wciśnij A, aby zaakceptować wybór.");
                            break;
                        case ModelScale.BodyPart.RightLeg:
                            Debug.Log("Wybrałeś pomiar prawej nogi. Wciśnij A, aby zaakceptować wybór.");
                            break;
                        case ModelScale.BodyPart.LeftLeg:
                            Debug.Log("Wybrałeś pomiar lewej nogi. Wciśnij A, aby zaakceptować wybór.");
                            break;
                        case ModelScale.BodyPart.Body:
                            switch (this.menuList.measureStep)
                            {
                                case 0:
                                    this.menuList.vectorList.Add(new Vector3(rightData.x, rightData.y, rightData.z));
                                    this.menuList.measureStep++;
                                    Debug.Log("Dotnkij kolana i powtórz procedurę");
                                    break;
                                case 1:
                                    this.menuList.vectorList.Add(new Vector3(rightData.x, rightData.y, rightData.z));
                                    this.menuList.measureStep++;
                                    Debug.Log("Dotnkij biodra i powtórz procedurę");
                                    break;
                                case 2:
                                    this.menuList.vectorList.Add(new Vector3(rightData.x, rightData.y, rightData.z));
                                    this.menuList.measureStep++;
                                    Debug.Log("Dotnkij klatki piersiowej i powtórz procedurę");
                                    break;
                                case 3:
                                    this.menuList.vectorList.Add(new Vector3(rightData.x, rightData.y, rightData.z));
                                    this.menuList.measureStep++;
                                    Debug.Log("Wciśnij A w celu zakończenia pomiaru.");
                                    break;
                                default:
                                    this.modelScale.ScaleModel(this.menuList.vectorList, ModelScale.BodyPart.Body);
                                    this.menuList.measureStep = 0;
                                    this.menuList.vectorList.Clear();
                                    break;
                            }
                            break;
                        case ModelScale.BodyPart.None:
                            Debug.Log("Nie wybrałeś żadnej opcji. Wciśnij A, aby zaakceptować wybór.");
                            break;
                        default:
                            Debug.Log("Nieznana opcja. Wciśnij A, aby zaakceptować wybór.");
                            break;
                    }
                }


                ////test banana
                //GameObject bananaMan = GameObject.Find("Banana Man");

                //// Znajdź obiekt Left Forearm wewnątrz Banana Man
                //Transform leftForearm = bananaMan.transform.Find("Armature/Hips/Spine 1/Spine 2/Spine 3/Left Shoulder/Left Arm/Left Forearm");

                //// Znajdź komponent Constant Rotation na obiekcie Left Forearm
                //ConstantRotation rotation = leftForearm.GetComponent<ConstantRotation>();

                //// Zmniejsz prędkość obrotu
                //rotation.RotationSpeed += 1000.0f;
                ////--------------


                //if (isGrowth.Equals(false))
                //{
                //    Debug.Log("Wykonywnie pomiaru w osi Y");
                //    isGrowth = true;
                //    growth1.x = rightData.x;
                //    growth1.y = rightData.y;
                //    growth1.z = rightData.z;
                //    Debug.Log("Pierwsza wartość " + growth1.y);
                //}
                //else
                //{
                //    isGrowth = false;
                //    growth2.x = rightData.x;
                //    growth2.y = rightData.y;
                //    growth2.z = rightData.z;
                //    Debug.Log("Druga wartość " + growth2.y);
                //    float distance = Math.Abs(growth1.y - growth2.y);
                //    Debug.Log("Wynik pomiaru: " + distance);
                //}


            }
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
    }
    
    
   
}
