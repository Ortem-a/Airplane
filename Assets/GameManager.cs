using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject ScreenshotObj;
    public GameObject DataObj;
    public GameObject AirplaneObj;
    public GameObject CameraObj;


    private void Awake()
    {
        AirplaneObj.SetActive(false);
        CameraObj.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            AirplaneObj.SetActive(true);
            CameraObj.SetActive(false);
            DataObj.SetActive(true);
            ScreenshotObj.SetActive(true);
        }
    }
}
