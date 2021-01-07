//Attach this script to a GameObject in your Scene.
using UnityEngine;
using UnityEngine.UI;

public class InputGyro : MonoBehaviour
{
    Gyroscope m_Gyro;

    void Start()
    {
        
    }

    //This is a legacy function, check out the UI section for other ways to create your UI
    void OnGUI()
    {
        //Output the rotation rate, attitude and the enabled state of the gyroscope as a Label
        GUI.Label(new Rect(500, 300, 200, 40), "Gyro rotation rate " + m_Gyro.rotationRate);
        GUI.Label(new Rect(500, 350, 200, 40), "Gyro attitude" + m_Gyro.attitude);
        GUI.Label(new Rect(500, 400, 200, 40), "Gyro enabled : " + m_Gyro.enabled);
    }

    private void Update()
    {
        gameObject.transform.rotation = m_Gyro.attitude;
        //print(m_Gyro.attitude);
        //print(m_Gyro.rotationRate);
    }
}