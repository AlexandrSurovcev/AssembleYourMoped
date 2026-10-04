using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TrainingInstruction : MonoBehaviour
{
    [SerializeField] GameObject fieldInstruction;
    [SerializeField] TextMeshProUGUI textInstruction;
    [SerializeField] TextMeshProUGUI textInputInstruction;
    [SerializeField] TextMeshProUGUI textOfScrew;
    [SerializeField] TextMeshProUGUI textOfTraining;
    [SerializeField] GameObject[] imageButtons;
    [SerializeField] GameObject[] textOfObjects;
    [SerializeField] Transform[] objects;
    [SerializeField] Transform Rcap;
    [SerializeField] Transform Fuel;
    [SerializeField] GameObject Player;
    [SerializeField] private Transform playerCamera;
    [SerializeField] private int maxRayDistance = 3;
    bool firstAssembled = false;
    bool secondAssembled = false;
    bool isSecondAssembled = false;
    void Update()
    {
        checkSequence();
        CheckBicycle();
        fieldInstruction.SetActive(false);
        if (Player.GetComponent<InteractionWithObjects>().draggableObject == null)
        {
            if (Physics.Raycast(playerCamera.position, playerCamera.forward, out RaycastHit hit, maxRayDistance))
            {
                if(hit.transform == objects[0] || hit.transform == objects[1]){
                    if(hit.transform.CompareTag("Disassembl") && !hit.transform.GetComponent<Assembled>().isAssembled){
                        ShowInstruct(1, "Нажмите     , чтобы  отсоединить");
                    }
                    else if(hit.transform.CompareTag("Draggable"))ShowInstruct(0, "Нажмите      , чтобы поднять");
                }
                if (hit.transform.CompareTag("Tools") || hit.transform.CompareTag("FuelGun") || hit.transform.CompareTag("SprayCans"))
                {
                    ShowInstruct(1, "Нажмите      , чтобы использовать");
                }
                if(hit.transform.CompareTag("Caps")){
                    ShowInstruct(2, "Используйте      , чтобы крутить");
                }
                if(hit.transform.CompareTag("Bolts")){
                    if(Player.GetComponent<Tools>().currentTool == hit.transform.GetComponent<Bolts>().tool){
                        ShowInstruct(2, "Используйте      , чтобы крутить");
                    }
                }
                if(Player.GetComponent<Tools>().currentTool != null && Player.GetComponent<Tools>().currentTool.CompareTag("SprayCans")){
                    if(hit.transform.GetComponent<Painted>()){
                        ShowInstruct(0,"Зажмите      , чтобы красить");
                    }
                }

                
                
                else {
                    textOfScrew.text = "Место крепления";
                    textOfObjects[3].SetActive(false);
                }
            }
            
            textOfObjects[2].SetActive(false);
            //textOfObjects[3].SetActive(false);
            if(Rcap.GetComponent<Rigidbody>().isKinematic == true){
                textOfScrew.text = "Прикрутите";
                if(!isSecondAssembled){
                    textOfTraining.text = "<color=#F1F607>• Соберите мопед и отправляйтесь домой\n<color=#04FD00>        • Поставьте цепь\n        • Поставьте крышку сцепления</color=#04FD00>\n<color=#FF0404>                • Прикрутите крышку сцепления";
                    isSecondAssembled = true;
                }
                textOfObjects[3].SetActive(true);
                if(Rcap.GetComponent<Assembled>().isAssembled){
                    textOfObjects[3].SetActive(false);
                }
            } 
        }
        else{
            for(int i =0;i<objects.Length;i++){
                if(Player.GetComponent<InteractionWithObjects>().draggableObject.transform == objects[i]){
                    Destroy(textOfObjects[i]);
                    textOfObjects[i+2].SetActive(true);
                    ShowInstruct(2, "Используйте      , чтобы вращать");
                    if(Physics.Raycast(playerCamera.position, playerCamera.forward, out RaycastHit hit, maxRayDistance)){
                        if(hit.transform==Player.GetComponent<InteractionWithObjects>().draggableObject.transform.GetComponent<Assembled>().assemblingObject.transform){
                            ShowInstruct(0, "      Нажмите      , чтобы прикрепить");
                        }
                    }
                }
                
            }
        }  
    }
    void CheckBicycle(){
        if(Player.GetComponent<Rigidbody>().isKinematic == true){
            textInputInstruction.text = "W - вперед\nS - назад\nA - влево\nD - вправо\nV - приблизить\nShift - сцепление\nL - свет\nSpace - тормоз";
        }
        else textInputInstruction.text = "W - вперед\nS - назад\nA - влево\nD - вправо\nV - приблизить\nShift - бег\nSpace - прыжок";
    }
    void checkSequence(){
        if(objects[0].GetComponent<Assembled>().isAssembled && !firstAssembled){
            textOfTraining.text = "<color=#F1F607>• Соберите мопед и отправляйтесь домой\n<color=#04FD00>        • Поставьте цепь</color=#04FD00>\n<color=#FF0404>        • Поставьте крышку сцепления\n                • Прикрутите крышку сцепления";
            textOfObjects[1].SetActive(true);
            objects[1].GetComponent<Draggable>().enabled = true;
            firstAssembled = true;
        }
        if(objects[1].GetComponent<Assembled>().isAssembled && !secondAssembled){
            textOfObjects[4].SetActive(true);
            textOfTraining.text = "<color=#04FD00>• Соберите мопед и отправляйтесь домой";
            secondAssembled = true;
        }
        if(objects[2].GetComponent<HandleSwitch>().isEnabled){
            textOfObjects[4].SetActive(false);
            textOfTraining.text = "<color=#04FD00>• Соберите мопед и отправляйтесь домой\n<color=#F1F607>• Осталось только заправить и можно ехать";
        }
        if(Fuel.GetComponent<HandleCaps>().fuelLevelValue>1){
            textOfTraining.text = "<color=#04FD00>• Соберите мопед и отправляйтесь домой\n• Осталось только заправить и можно ехать\n<color=#F1F607>• Отправляйтесь домой";
        }
    }
    void ShowInstruct(int image, string text){
        fieldInstruction.SetActive(true);
        textInstruction.text = text;
        showImage(image);
    }
    void showImage(int image){
        for(int i = 0; i < imageButtons.Length;i++){
            if(i == image){
                imageButtons[i].SetActive(true);
            }
            else imageButtons[i].SetActive(false);
        }
    }
}
