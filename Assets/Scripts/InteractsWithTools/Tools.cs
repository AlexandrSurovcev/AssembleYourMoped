
using UnityEngine;

public class Tools : Sounds
{
    private const string Tag = "Tools";
    private const string TagSpray = "SprayCans";
    private const string TagFuelGun = "FuelGun";
    [SerializeField] private KeyCode pickKey = KeyCode.Mouse1;
    [SerializeField] private int MaxRayDistance = 3;
    [SerializeField] private Transform _playerCamera;
    [SerializeField] private Transform _pickUpSocket;
    [SerializeField] private Transform _fuelGunSocket;
    private const int DefaultLayerValue = 0;
    private const int ToolsLayerValue = 10;
    public GameObject currentTool;
    public AudioClip takeSprayCanAudio;
    private bool canPick;
    private bool canPickSpray;
    private bool canPickFuelGun;
    void Update()
    {
        if(Input.GetKeyDown(pickKey))
        {
            if(!canPick&&!canPickSpray&&!canPickFuelGun){
                pickUpTool();
            }
            else if(canPickFuelGun){
                setFuelPlace();
            } 
            else dropTool();
        }
        GetComponent<workTools>().enabled = canPick;
        GetComponent<WorkSprayCans>().enabled = canPickSpray;
        GetComponent<WorkFuelGun>().enabled = canPickFuelGun;
        GetComponent<InteractionWithObjects>().enabled = !canPickSpray;
    }
    void pickUpTool(){
        RaycastHit hit;
        if(Physics.Raycast(_playerCamera.position, _playerCamera.forward, out hit, MaxRayDistance))
        {
            if (hit.transform.CompareTag(Tag))
            {
                if(canPick) dropTool();
                currentTool = hit.transform.gameObject;
                currentTool.GetComponent<Rigidbody>().isKinematic = true;
                currentTool.transform.parent = _pickUpSocket;
                currentTool.transform.localPosition = Vector3.zero;
                currentTool.transform.localEulerAngles = new Vector3(-90f,0f,0f);
                currentTool.layer = ToolsLayerValue;
                canPick = true;
            }
            else if(hit.transform.CompareTag(TagSpray)){
                if(canPickSpray) dropTool();
                currentTool = hit.transform.gameObject;
                PlaySound(transform.GetComponent<AudioSource>(), takeSprayCanAudio);
                currentTool.GetComponent<Rigidbody>().isKinematic = true;
                currentTool.transform.parent = _pickUpSocket;
                currentTool.transform.localPosition = Vector3.zero;
                currentTool.transform.localEulerAngles = new Vector3(-90f,0f,90f);
                currentTool.layer = ToolsLayerValue;
                canPickSpray = true;
            }
            else if(hit.transform.CompareTag(TagFuelGun)){
                if(canPickFuelGun) dropTool();
                currentTool = hit.transform.gameObject;
                PlaySound(transform.GetComponent<AudioSource>(), currentTool.GetComponent<UsableTools>().takeToolAudio);
                currentTool.GetComponent<Rigidbody>().isKinematic = true;
                currentTool.transform.parent = _pickUpSocket;
                currentTool.transform.localPosition = Vector3.zero;
                currentTool.transform.localEulerAngles = new Vector3(-90f,-50f,0f);
                currentTool.layer = ToolsLayerValue;
                canPickFuelGun = true;
            }
        }
    }
    void dropTool(){
        currentTool.transform.parent = null;
        currentTool.transform.GetComponent<Rigidbody>().isKinematic = false;
        
        currentTool.layer = DefaultLayerValue;
        canPick = false;
        canPickSpray = false;
        currentTool = null;
    }
    public void setFuelPlace(){
        currentTool.transform.parent = null;
        currentTool.transform.GetComponent<Rigidbody>().isKinematic = true;
        currentTool.layer = DefaultLayerValue;
        PlaySound(transform.GetComponent<AudioSource>(), currentTool.GetComponent<UsableTools>().putToolAudio);
        GetComponent<WorkFuelGun>().StopFilligFuelAudio();
        Quaternion rot = _fuelGunSocket.rotation;
        currentTool.transform.rotation = rot;
        Vector3 localPosition = _fuelGunSocket.position;
        currentTool.transform.localPosition = localPosition;
        canPickFuelGun = false;
        currentTool = null;
    }
    
}
