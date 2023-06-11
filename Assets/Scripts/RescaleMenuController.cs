using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RescaleMenuController : MonoBehaviour
{
    public GameObject[] options;
    private int currentOption = 0;
    private float thumbstickInput = 0f;
    public float thumbstickThreshold = 0.5f;
    public float switchDelay = 0.1f;
    private float switchTimer = 0f;

    public Color selectedColor; // Kolor dla wybranego elementu
    private Color[] defaultColors; // Tablica kolorów dla wszystkich elementów

    void Start()
    {
        defaultColors = new Color[options.Length]; // Inicjalizacja tablicy kolorów

        // Przechodzimy przez wszystkie elementy i zapisujemy ich domyślne kolory
        for (int i = 0; i < options.Length; i++)
        {
            Image image = options[i].GetComponent<Image>();
            defaultColors[i] = image.color;
        }

        // Ustawienie koloru dla początkowo wybranego elementu
        SetSelectedColor(currentOption);
    }

    void Update()
    {
        thumbstickInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y;

        if (thumbstickInput > thumbstickThreshold && switchTimer <= 0f)
        {
            SelectOption(currentOption - 1);
            switchTimer = switchDelay;
        }
        else if (thumbstickInput < -thumbstickThreshold && switchTimer <= 0f)
        {
            SelectOption(currentOption + 1);
            switchTimer = switchDelay;
        }

        if (switchTimer > 0f)
        {
            switchTimer -= Time.deltaTime;
        }
    }

    void SelectOption(int index)
    {
        if (index < 0)
        {
            index = options.Length - 1;
        }
        else if (index >= options.Length)
        {
            index = 0;
        }

        // Wyłącz podświetlenie aktualnej opcji
        SetDefaultColor(currentOption);

        // Wybierz nową opcję i włącz jej podświetlenie
        currentOption = index;
        SetSelectedColor(currentOption);
    }

    void SetSelectedColor(int index)
    {
        Image image = options[index].GetComponent<Image>();
        image.color = Color.yellow;
    }

    void SetDefaultColor(int index)
    {
        Image image = options[index].GetComponent<Image>();
        image.color = defaultColors[index];
    }
    
    public int GetCurrentOption()
    {
        return currentOption;
    }
}