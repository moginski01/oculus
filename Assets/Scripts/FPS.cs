using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FPS : MonoBehaviour
{

    TextMeshProUGUI fpsText;
    public int refreshRate = 10;
    int frameCounter;
    float totalTime;
    // Start is called before the first frame update
    void Start()
    {
        fpsText = GetComponent<TextMeshProUGUI>();
        frameCounter = 0;
        totalTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(frameCounter == refreshRate)
        {
            float averageFps = (1.0f / (totalTime/refreshRate));
            fpsText.text = averageFps.ToString("F1");
            
            frameCounter = 0;
            totalTime = 0;
        }else
        {
            totalTime += Time.deltaTime;
            frameCounter++;
        }
    }
}
