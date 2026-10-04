
using UnityEngine;

[ExecuteInEditMode]
public class Zoom : MonoBehaviour
{
    [SerializeField] private KeyCode _zoomKey = KeyCode.Z;
    Camera camera;
    [SerializeField] Camera BoltsCamera;
    public float defaultFOV = 60;
    public float sensitivity;
    bool zoomed = false;


    void Awake()
    {
        // Get the camera on this gameObject and the defaultZoom.
        camera = GetComponent<Camera>();
        if (camera)
        {
            defaultFOV = camera.fieldOfView;
        }
    }
    void Start(){
        sensitivity = GetComponent<FirstPersonLook>().sensitivitylvl;

    }

    void Update()
    {
        if(Input.GetKey(_zoomKey)){
            zoomed = true;  
        }
        else zoomed = false;
        GetComponent<Animator>().SetBool("zoom", zoomed);
        GetComponent<FirstPersonLook>().sensitivitylvl = sensitivity/(defaultFOV/camera.fieldOfView);
        // BoltsCamera.fieldOfView = camera.fieldOfView;
    }
            
    
}
