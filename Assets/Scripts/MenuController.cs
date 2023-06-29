using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene("MainScene");
    }
    
    public void JanekButton()
    {

        SceneManager.LoadScene("JanekScene");
    }
    
    public void BartekButton()
    {

        SceneManager.LoadScene("BartekScene");
    }
 
    public void MatiButton()
    {

        SceneManager.LoadScene("MatiScene");
    }
    
    public void CalibrationButton()
    {

        SceneManager.LoadScene("CalibrationScene");
    }
    
    
}
