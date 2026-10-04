using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    public GameObject mainCam;
    public GameObject BedCamera;
    public GameObject player;
    public GameObject DayCycle;
    public GameObject closed;
    [SerializeField] private KeyCode interactionKey = KeyCode.Mouse1;
    public float interactionDistance = 3f;
    // Start is called before the first frame update
    void Start()
    {
        mainCam.SetActive(false);
        player.SetActive(false);
        BedCamera.SetActive(true);
        BedCamera.GetComponent<Animator>().SetBool("rise", true);
        Cursor.visible = false;
        StartCoroutine(StopRise());
        StartCoroutine(StopClosed());
    }
    IEnumerator StopClosed(){
        yield return new WaitForSeconds(3f);
        closed.SetActive(false);
    }
    IEnumerator StopRise(){
        yield return new WaitForSeconds(6.3f);
        mainCam.SetActive(true);
        player.SetActive(true);
        BedCamera.GetComponent<Animator>().SetBool("rise", false);
        BedCamera.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = mainCam.GetComponent<Camera>().ViewportPointToRay(Vector3.one / 2f);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, interactionDistance))
        {
            if(hit.collider.CompareTag("Bed"))
            {
               if(Input.GetKeyUp(interactionKey)){
                    mainCam.SetActive(false);
                    player.SetActive(false);
                    BedCamera.SetActive(true);
                    DayCycle.GetComponent<DayCycleManager>().DayDuration = DayCycle.GetComponent<DayCycleManager>().DayDuration/45;
                    BedCamera.GetComponent<Animator>().SetBool("sleep", true);
                    StartCoroutine(StopSleep());
               }
            }
        }
    }
    IEnumerator StopSleep(){
        yield return new WaitForSeconds(5f);
        DayCycle.GetComponent<DayCycleManager>().DayDuration = DayCycle.GetComponent<DayCycleManager>().DayDuration * 45;
        Vector3 localPosition = new Vector3(715.284f, 0.417f, 357.029f);
        player.transform.localPosition = localPosition;
        mainCam.SetActive(true);
        player.SetActive(true);
        BedCamera.SetActive(false);
    }
}
