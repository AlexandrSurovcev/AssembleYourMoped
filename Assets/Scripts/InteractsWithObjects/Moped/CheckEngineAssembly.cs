 using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckEngineAssembly : MonoBehaviour
{
    [SerializeField] Transform ChainPedals;
    [SerializeField] Transform[] FuelObjects;
    [SerializeField] Transform FuelCrane;
    [SerializeField] Transform FuelLevelValue;
    [SerializeField] Transform exhaustPipe;
    [SerializeField] ParticleSystem exhaustPipeSmoke;
    [SerializeField] ParticleSystem engineSmoke;
    [SerializeField] AudioClip motorAudioWithExhaust;
    [SerializeField] AudioClip motorAudioWithoutExhaust;
    [SerializeField] Transform[] ClutchObjects;
    [SerializeField] Transform[] EngineObjects;
    public bool IsPedalMotor = false;
    //public bool IsElectricity = false;
    public bool IsMotorAssembly = false;
    public bool IsClutch = false;
    public bool IsFuel = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //GetComponent<BicycleVehicle>().TurnoversUpdate();
        CheckChain();
        CheckEngineSystem();
        CheckExhaustSystem();
        CheckFuelSystem();
        CheckClutchSystem();
        CheckSystems();
    }
    //Проверка сборки мотора
    void CheckEngineSystem(){
        IsMotorAssembly = CheckSystem(EngineObjects);
        
    }
    //проверка всех систем
    void CheckSystems(){
        if(IsMotorAssembly){
            if(IsFuel){
                GetComponent<BicycleVehicle>().canStart = true;
            }
        }
        else GetComponent<BicycleVehicle>().EngineShutDown();
    }
    //Проверка цепи педалей
    void CheckChain(){
        IsPedalMotor = ChainPedals.GetComponent<Assembled>().isAssembled;
    }
    //Проверка сцепления
    void CheckClutchSystem(){
        IsClutch = CheckSystem(ClutchObjects);
    }
    //Проверка выхлопной системы
    void CheckExhaustSystem(){
        if(exhaustPipe.GetComponent<Assembled>().isAssembled && GetComponent<BicycleVehicle>().launchedMotorSound == motorAudioWithoutExhaust){
            ExhaustPipe(0.4f,exhaustPipeSmoke,motorAudioWithExhaust);
        }
        else if(!exhaustPipe.GetComponent<Assembled>().isAssembled && GetComponent<BicycleVehicle>().launchedMotorSound == motorAudioWithExhaust) {
            ExhaustPipe(0.6f,engineSmoke,motorAudioWithoutExhaust);
        }
    }
    //Настройка выхлопной системы
    void ExhaustPipe(float volume, ParticleSystem smoke, AudioClip motorAudio){
        GetComponent<BicycleVehicle>().exhaustSystem.Stop();
        GetComponent<AudioSource>().volume = volume;
        GetComponent<BicycleVehicle>().exhaustSystem = smoke;
        GetComponent<BicycleVehicle>().launchedMotorSound = motorAudio;
        if(GetComponent<BicycleVehicle>().MotorStarted){
            GetComponent<AudioSource>().clip = motorAudio;
            GetComponent<AudioSource>().Play();
            GetComponent<BicycleVehicle>().exhaustSystem.Play();
        }
    }
    //Проверка топливной системы
    void CheckFuelSystem(){
        IsFuel = CheckSystem(FuelObjects);
        if(IsFuel){
            if(FuelCrane.GetComponent<HandleSwitch>().isEnabled){
                if(FuelLevelValue.GetComponent<HandleCaps>().fuelLevelValue>0){
                    IsFuel = true;
                    StopCoroutine(EngineDown());
                }
                else {
                    IsFuel = false;
                    if(GetComponent<BicycleVehicle>().CurrentTurnover>0) GetComponent<BicycleVehicle>().CurrentTurnover -= Time.deltaTime/20;
                    StartCoroutine(EngineDown());
                }
            }
            else {
                IsFuel = false;
                if(GetComponent<BicycleVehicle>().CurrentTurnover>0) GetComponent<BicycleVehicle>().CurrentTurnover -= Time.deltaTime/20;
                StartCoroutine(EngineDown()); 
            }
        }

    }
    //Заглушить мотор через 12 сек
    IEnumerator EngineDown(){
        yield return new WaitForSeconds(12f);
        GetComponent<BicycleVehicle>().EngineShutDown();
        
    }
    //Проверка сборки системы
    bool CheckSystem(Transform[] transforms){
        bool flag = false;
        for (int i = 0;i < transforms.Length;i++){
            if (!transforms[i].GetComponent<Assembled>().isAssembled){
                flag = true;
            }
        }
        return !flag;
    }
}
