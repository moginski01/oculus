using UnityEngine;
using UnityEngine.XR;

public class VRDebug : MonoBehaviour
{

    public GameObject UI;
    public GameObject UIAnchor;
    private bool UIActive;
    private InputData _inputData;
    
    // Start is called before the first frame update
    void Start()
    {
        UI.SetActive(false);
        UIActive = false;
        _inputData = GetComponent<InputData>();
    }

    // Update is called once per frame
    void Update()
    {
        if(OVRInput.GetDown(OVRInput.Button.Four))
        {
            UIActive = !UIActive;
            UI.SetActive(UIActive);
        }
        if(UIActive)
        {
            UI.transform.position = UIAnchor.transform.position;
            UI.transform.eulerAngles = new Vector3(UIAnchor.transform.eulerAngles.x,UIAnchor.transform.eulerAngles.y,0);

        }

        if(OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            if (_inputData._rightController.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 rightData))
            {
                float x = rightData.x;
                float y = rightData.y;
                float z = rightData.z;
                
                Debug.Log("right hand x = " + x.ToString("F3") + ",y = " + y.ToString("F3") + ",z = " + z.ToString("F3"));
            }
            // Debug.Log("Right trigger pressed.");
        }

        if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            if (_inputData._leftController.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 leftData))
            {
                float x = leftData.x;
                float y = leftData.y;
                float z = leftData.z;
                
                Debug.Log("left hand x = " + x.ToString("F3") + ",y = " + y.ToString("F3") + ",z = " + z.ToString("F3"));
            }
            // Debug.Log("Left trigger pressed.");
        }
    }
}
