using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
   public Camera mainCam;
   public float interactionDistance = 7f;
   public TextMeshProUGUI textOfPaper;
   public TextMeshProUGUI description;
   public TextMeshProUGUI descriptionOfInteraction;
   public GameObject Crosshair;
   public GameObject interactionUI;
   public GameObject DisassemblUI;
   public GameObject BedUI;
    void Update()
    {
        ReadPaper();
        InteractionRay();
        Interaction();
        InteractionBed();
        InteractionDisassemblParts();
        InteractionDescriptionWithObjects();
    }

    void ReadPaper(){
        if(textOfPaper){
            Ray ray = mainCam.ViewportPointToRay(Vector3.one / 2f);
            RaycastHit hit;
            bool hitSomething = false;
            if(Physics.Raycast(ray, out hit, interactionDistance))
            {
                if(hit.collider.CompareTag("Paper"))
                {
                        StartCoroutine(ReadAllPaper());
                        hitSomething = true;
                }
            }
            else {
                    StopCoroutine(ReadAllPaper());
                    textOfPaper.text = "Не расслабляйся и почини мопед твоего отца, или мы отправим его на свалку, потому что он уже долгое время заполняет гараж. Ключи лежат на столе. Кстати, на заднем дворе лежит старый мопед, может какие-то запчасти пригодятся. Tвой папа говорит, что если ты отремонтируешь его и он поедет, ты можешь оставить его себе.";
                   
                
            }
            textOfPaper.enabled = hitSomething;

        }
        
    }
    
                    
    IEnumerator ReadAllPaper(){
        yield return new WaitForSeconds(20);
        textOfPaper.text = "В канистре должно быть осталось немного топлива, можешь заехать к дяде на заправку, проедь вдоль трассы и увидишь ее, он заправит тебя бесплатно. Не забудь убраться, если устроишь беспорядок! Мы вернемся домой через неделю! С наилучшими пожеланиями, мама и папа";
    }

    public void InteractionRay()
    {
        Ray ray = mainCam.ViewportPointToRay(Vector3.one / 2f);
        RaycastHit hit;
        bool hitSomething = false;
        if(Physics.Raycast(ray, out hit, interactionDistance))
        {
            if(hit.collider.GetComponent<Draggable>())
            {
                description.text = hit.collider.GetComponent<Draggable>().textOfDescription;
                hitSomething = true;
            }
            else if(hit.collider.GetComponent<UsableTools>())
            {
                description.text = hit.collider.GetComponent<UsableTools>().description;
                hitSomething = true;
            }
        }
        description.enabled = hitSomething;
    }
    public void InteractionDescriptionWithObjects()
    {
        Ray ray = mainCam.ViewportPointToRay(Vector3.one / 2f);
        RaycastHit hit;
        bool hitSomething = false;
        if(Physics.Raycast(ray, out hit, interactionDistance))
        {
            if(hit.collider.GetComponent<HandleSwitch>() && hit.collider.CompareTag("FuelCrane"))
            {
                hitSomething = true;
                if(hit.collider.GetComponent<HandleSwitch>().isEnabled){
                    descriptionOfInteraction.text = "Закрыть";
                }
                else descriptionOfInteraction.text = "Открыть";
                hitSomething = true;
            }
            if(hit.collider.GetComponent<HandleSwitch>() && hit.collider.CompareTag("Switch"))
            {
                hitSomething = true;
                if(hit.collider.GetComponent<HandleSwitch>().isEnabled){
                    descriptionOfInteraction.text = "Выкл";
                }
                else descriptionOfInteraction.text = "Вкл";
                hitSomething = true;
            }
            if(hit.collider.CompareTag("Ignition"))
            {
                descriptionOfInteraction.text = "Заглушить";
                hitSomething = true;
            }
        }
        descriptionOfInteraction.enabled = hitSomething;
    }

    public void Interaction(){
        Ray ray = mainCam.ViewportPointToRay(Vector3.one / 2f);
        RaycastHit hit;
        bool hitSomething = false;
        if(Physics.Raycast(ray, out hit, interactionDistance))
        {
            if(hit.collider.CompareTag("Tools") 
            || hit.collider.CompareTag("Doors") 
            || hit.collider.CompareTag("Caps") 
            || hit.collider.CompareTag("Switch") 
            || hit.collider.CompareTag("Gates") 
            || hit.collider.CompareTag("Drawers")
            || hit.collider.CompareTag("Keys")
            || hit.collider.CompareTag("FuelCrane")
            || hit.collider.CompareTag("Padlock")
            || hit.collider.CompareTag("SprayCans")
            || hit.collider.CompareTag("Footrest")
            || hit.collider.CompareTag("CasolineCap")
            || hit.collider.CompareTag("FuelGun")
            || hit.collider.CompareTag("Ignition"))
            {
                hitSomething = true;
            }
        }
        interactionUI.SetActive(hitSomething);
    }
    public void InteractionBed(){
        Ray ray = mainCam.ViewportPointToRay(Vector3.one / 2f);
        RaycastHit hit;
        bool hitSomething = false;
        if(Physics.Raycast(ray, out hit, interactionDistance))
        {
            if(hit.collider.CompareTag("Bed"))
            {
                hitSomething = true;
            }
        }
        if(BedUI){
            BedUI.SetActive(hitSomething);
        }
    }
    
    public void InteractionDisassemblParts(){
        Ray ray = mainCam.ViewportPointToRay(Vector3.one / 2f);
        RaycastHit hit;
        bool hitSomething = false;
        if(Physics.Raycast(ray, out hit, interactionDistance))
        {
            if(hit.collider.CompareTag("Disassembl") && !hit.transform.GetComponent<Assembled>().isAssembled)
            {
                hitSomething = true;
            }
        }
        DisassemblUI.SetActive(hitSomething);
    }
}
