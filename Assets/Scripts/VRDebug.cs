using System;
using UnityEngine;
using UnityEngine.XR;
using Oculus.Interaction.Samples;
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
        
        //test do banana
        // Znajdź obiekt Banana Man w hierarchii sceny
        GameObject bananaMan = GameObject.Find("Banana Man");

        // Znajdź obiekt Left Forearm wewnątrz Banana Man
        Transform leftForearm = bananaMan.transform.Find("Armature/Hips/Spine 1/Spine 2/Spine 3/Left Shoulder/Left Arm/Left Forearm");

        // Dodaj komponent Constant Rotation do obiektu Left Forearm
        // ConstantRotation rotation = leftForearm.gameObject.AddComponent<ConstantRotation>();
        //----------------------------
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
        


        if(OVRInput.GetDown(OVRInput.Button.One))
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
                rotation.RotationSpeed += 10.0f;
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

        // if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        // {
        //     if (_inputData._leftController.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 leftData))
        //     {
        //         float x = leftData.x;
        //         float y = leftData.y;
        //         float z = leftData.z;
        //         Debug.Log("left hand x = " + x.ToString("F3") + ",y = " + y.ToString("F3") + ",z = " + z.ToString("F3"));
        //     }
        //     // Debug.Log("Left trigger pressed.");
        // }
        
        if(OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))
        {
            Debug.Log("Command_Clear");
        }
    }
    
    
   
}
