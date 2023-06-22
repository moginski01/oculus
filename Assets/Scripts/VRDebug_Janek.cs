using System;
using UnityEngine;
using UnityEngine.XR;
using Oculus.Interaction.Samples;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;
using Timer = System.Threading.Timer;

enum Menu
{
    LIST,
    LOAD,
    CLEAR,
    REMOVE,
    REMOVE_ALL
}

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
    private Boolean wasTriggerMoved = false;
    
    private List<Vector3> arc = new List<Vector3>();
    public Button loadButton;
    public Button clearButton;
    public Button removeButton;
    public Button removeAllButton;
    public TMP_Dropdown dropdown;


    private List<String> fileNameList = new List<string>();
    
    private List<Menu> menuOptions;
    private int currentMenuOptionIndex;
    

    private int currentOption = 0;
    private float thumbstickInput_x = 0f;
    private float thumbstickInput_y = 0f;
    public float thumbstickThreshold = 0.5f;

    private float timer = 0f;

    private Color[] defaultColors;

    public Image arrowUp;
    public Image arrowDown;
    public List<List<GameObject>> history = new List<List<GameObject>>();

    void ClearHistory()
    {
        foreach (var gameObjectList in history)
        {
            foreach (var gameObject in gameObjectList)
            {
                Destroy(gameObject);
            }
        }

        foreach (GameObject o in path)
        {
            Destroy(o);
        }
        history.Clear();
    }

    private void LoadMeasurements()
    {
        string fileName = "hand_positions_measurements.txt";
        fileName = Path.Combine(Application.persistentDataPath, fileName);
        
        List<string> measurements = new List<string>();
        if (File.Exists(fileName))
        {
            try
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        measurements.Add(line);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log("The file could not be read:");
            }
        }

        dropdown.ClearOptions();
        dropdown.AddOptions(measurements);
        fileNameList = measurements;
        UpdateArrowVisibility();
        
    }
    
    void RemoveLineFromFile(string lineToRemove)
    {
        string fileName = "hand_positions_measurements.txt";
        fileName = Path.Combine(Application.persistentDataPath, fileName);
        if(File.Exists(fileName))
        {
            string tempFile = Path.GetTempFileName();

            using (var sr = new StreamReader(fileName))
            using (var sw = new StreamWriter(tempFile))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line != lineToRemove)
                        sw.WriteLine(line);
                    else
                    {
                        string fileToRemove = Path.Combine(Application.persistentDataPath, lineToRemove);
                        if (File.Exists(fileToRemove))
                        {
                            try
                            {
                                File.Delete(fileToRemove);
                            }
                            catch (IOException ex)
                            {
                                Debug.LogError("Nie udało się usunąć pliku: " + fileToRemove + ". Błąd: " + ex.Message);
                            }
                        }
                    }
                }
            }

            File.Delete(fileName);
            File.Move(tempFile, fileName);
        }
        else
        {
            Debug.LogError("Plik nie istnieje: " + fileName);
        }
        UpdateArrowVisibility();
        LoadMeasurements();
    }
    
    void DeleteFile()
    {
        string fileName = "hand_positions_measurements.txt";
        fileName = Path.Combine(Application.persistentDataPath, fileName);
        if (File.Exists(fileName))
        {
            using (var sr = new StreamReader(fileName))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    line = Path.Combine(Application.persistentDataPath, line);
                    if (File.Exists(line))
                    {
                        File.Delete(line);
                    }
                }
            }
            File.Delete(fileName);
        }

        LoadMeasurements();
        UpdateArrowVisibility();
    }
    
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
        // this.menuList = new MenuList();
        // this.modelScale = new ModelScale();
        Button btn = loadButton.GetComponent<Button>();
        dropdown = dropdown.GetComponent<TMP_Dropdown>();
        btn.onClick.AddListener(() =>
        {
            LoadPointsFromFile(dropdown.options[dropdown.value].text);
        });

        LoadMeasurements();
        
        menuOptions = new List<Menu>()
        {
            Menu.LIST,
            Menu.LOAD,
            Menu.CLEAR,
            Menu.REMOVE,
            Menu.REMOVE_ALL
        };

        currentMenuOptionIndex = 0;
        
    }

    void TaskOnClick()
    {
        Debug.Log(dropdown.options[dropdown.value].text);
        
    }
    
    public void NextOption()
    {
        currentMenuOptionIndex = (currentMenuOptionIndex + 1) % menuOptions.Count;
        Debug.Log(currentMenuOptionIndex.ToString());
    }

    public void PreviousOption()
    {
        currentMenuOptionIndex = (currentMenuOptionIndex - 1 + menuOptions.Count) % menuOptions.Count;
        Debug.Log(currentMenuOptionIndex.ToString());
    }

    public void HandleCurrentOption()
    {
        Menu currentOption = menuOptions[currentMenuOptionIndex];
    
        switch (currentOption)
        {
            case Menu.LIST:
                break;
            case Menu.LOAD:
                if (dropdown.options.Count() > 0)
                {
                    LoadPointsFromFile(dropdown.options[dropdown.value].text);
                    LoadMeasurements();
                }
                Debug.Log("Wybrano LOAD");
                break;
            case Menu.CLEAR:
                ClearHistory();
                break;
            case Menu.REMOVE:
                if (dropdown.options.Count() > 0)
                {
                    RemoveLineFromFile(dropdown.options[dropdown.value].text);
                    LoadMeasurements();
                }
                break;
            case Menu.REMOVE_ALL:
                DeleteFile();
                break;
        }
    }
    
    public void ChangeState()
    {
        Menu currentOption = menuOptions[currentMenuOptionIndex];
        dropdown.image.color = Color.white;
        loadButton.image.color = Color.white;
        clearButton.image.color = Color.white;
        removeButton.image.color = Color.white;
        removeAllButton.image.color = Color.white;
        switch (currentOption)
        {
            case Menu.LIST:
                dropdown.image.color = Color.yellow;
                break;
            case Menu.LOAD:
                loadButton.image.color = Color.yellow;
                break;
            case Menu.CLEAR:
                clearButton.image.color = Color.yellow;
                break;
            case Menu.REMOVE:
                removeButton.image.color = Color.yellow;
                break;
            case Menu.REMOVE_ALL:
                removeAllButton.image.color = Color.yellow;
                break;
        }
    }

    void ListNextOptions()
    {
        var currentOption = dropdown.value;
        if (currentOption < dropdown.options.Count - 1)
        {
            dropdown.value = currentOption + 1;
        }

        UpdateArrowVisibility();
        dropdown.RefreshShownValue();
    }

    void ListPreviousOptions()
    {
        var currentOption = dropdown.value;
        if (currentOption > 0)
        {
            dropdown.value = currentOption - 1;
        }

        UpdateArrowVisibility();
        dropdown.RefreshShownValue();
    }

    void UpdateArrowVisibility()
    {
        if (dropdown.options.Count == 0)
        {
            arrowDown.enabled = false;
            arrowUp.enabled = false;
        }
        else
        {
            arrowDown.enabled = dropdown.value > 0;
            arrowUp.enabled = dropdown.value < dropdown.options.Count - 1;
        }
    }

    void Update()
    {

        timer += Time.deltaTime;
        thumbstickInput_x = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x;
        thumbstickInput_y = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y;
        
        var selectedOption = menuOptions[currentMenuOptionIndex];
        if (thumbstickInput_x > thumbstickThreshold && timer > 0.3f)
        {
            NextOption();
            timer = 0.0f;
        }
        else if (thumbstickInput_x < -thumbstickThreshold && timer > 0.3f)
        {
            PreviousOption();
            timer = 0.0f;
        }
        
        if (thumbstickInput_y > thumbstickThreshold && timer > 0.3f)
        {
            if (selectedOption == Menu.LIST)
            {
                ListNextOptions();
            }
            timer = 0.0f;
        }
        else if (thumbstickInput_y < -thumbstickThreshold && timer > 0.3f)
        {
            if (selectedOption == Menu.LIST)
            {
                ListPreviousOptions();
            }
            timer = 0.0f;
        }

        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            HandleCurrentOption();
        }

        ChangeState();



        async void StartRecordingPath()
        {
            GameObject controller = GameObject.Find("OVRPlayerController");
            Transform hand = controller.transform.Find("OVRCameraRig/TrackingSpace/RightHandAnchor");

            Debug.Log("Here");
            foreach(GameObject pathElement in this.path)
            {
                Destroy(pathElement);
            }
            this.path.Clear();
            cancellationTokenSource = new CancellationTokenSource();

            string dateTimeString = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
            string fileName = "hand_positions_" + dateTimeString + ".txt";
            CallAppendText(fileName);
            fileName = Path.Combine(Application.persistentDataPath, fileName);
            

            
            using (var writer = new StreamWriter(fileName, true))
            {
                do
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
                } while (!cancellationTokenSource.IsCancellationRequested);
            }
            
            
        }
        
        void AppendTextToFileAsync(string filePath, string text)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLineAsync(text);
                }
            }
            catch (Exception ex)
            {
                Debug.Log($"An error occurred: {ex.Message}");
            }
        }
    
        void CallAppendText(string filePath)
        {
            string fileName = "hand_positions_measurements.txt";
            fileName = Path.Combine(Application.persistentDataPath, fileName);
            AppendTextToFileAsync(fileName, filePath);
        }
        
        

        void StopRecordingPath()
        {
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
            }
            LoadMeasurements();
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

    private void LoadPointsFromFile(string fileName)
    {
        var points = new List<Vector3>();
        fileName = Path.Combine(Application.persistentDataPath, fileName); 
        var lines = File.ReadLines(fileName);
        foreach (var line in lines)
        {
            var parts = line.Split(' ');
            if (parts.Length != 3)
            {
                continue;
            }
        
            if (!float.TryParse(parts[0], out float x) || 
                !float.TryParse(parts[1], out float y) || 
                !float.TryParse(parts[2], out float z))
            {
                continue;
            }

            points.Add(new Vector3(x, y, z));
        }

        Color randomColor = UnityEngine.Random.ColorHSV();
        List<GameObject> tempList = new List<GameObject>();
        foreach (var point in points)
        {
            GameObject pathElement = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            pathElement.GetComponent<Collider>().enabled = false;
            pathElement.transform.position = point;
            pathElement.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            pathElement.GetComponent<Renderer>().material.color = randomColor;
            tempList.Add(pathElement);
        }
        
        history.Add(tempList);
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
