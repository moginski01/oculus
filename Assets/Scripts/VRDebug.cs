using UnityEngine;
using UnityEngine.XR;

public class VRDebug : MonoBehaviour
{

    public GameObject UI;
    public GameObject UIAnchor;
    private bool UIActive;
    private InputData _inputData;
    private bool firstSet = false;
    private float x1, y1, z1;
    private float x2, y2, z2;
    
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

        if(OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            if (_inputData._rightController.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 rightData))
            {
                if (firstSet.Equals(false))
                {
                    firstSet = true;
                    x1 = rightData.x;
                    y1 = rightData.y;
                    z1 = rightData.z;
                    Debug.Log("Punkt pierwszy = " + x1 + ",y = " + y1 + ",z = " + z1);
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
            // Debug.Log("Right trigger pressed.");
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
        
        if(OVRInput.GetDown(OVRInput.Button.Two))
        {
            Debug.Log("Command_Clear");
        }
    }
}
