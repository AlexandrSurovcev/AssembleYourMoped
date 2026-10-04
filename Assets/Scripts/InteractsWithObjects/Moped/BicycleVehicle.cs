using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class BicycleVehicle : Sounds
{
	public ParticleSystem exhaustSystem;
	float horizontalInput;
	public float verticalInput;
	public bool MotorStarted = false;
	public bool canStart = false;
	public AudioClip launchedMotorSound;
	public AudioClip launchMotorSound;
	public AudioSource pedalsAudioSource;

	public Transform handle;
	bool braking;
	bool clutch;
	Rigidbody rb;
	public float TurnoverPlusOnGaz = 45;
	public float TurnoverMinusOnHolostie = 0.001f;
	public float minTurnover = 0.6f;

	public static BicycleVehicle bv;
	public float CurrentTurnover = 0.7f;

	bool isLaunchedSound = false;
	bool isLaunchSound = false;
	bool isPedalsSound = false;

	public Vector3 COG;
	[SerializeField] float currentForce;
	public float motorForce;
	[SerializeField] float pedalForce;
	[SerializeField] float brakeForce;

	public AudioSource audioSource;

	float currentBrakeForce;

	float steeringAngle;
	[SerializeField] float currentSteeringAngle;
	[Range(0f, 0.1f)] [SerializeField] float speedSteerControlTime;
	[SerializeField] float maxSteeringAngle;
	[Range(0.000001f, 1)] [SerializeField] float turnSmoothing;

	[SerializeField]float maxLayingAngle = 45f;
	public float targetLayingAngle;
	[Range(-40, 40)]public float layingAmount;
	[Range(0.000001f, 1 )] [SerializeField] float leanSmoothing;

	[SerializeField] WheelCollider frontWheel;
	[SerializeField] WheelCollider backWheel;

	[SerializeField] Transform frontWheelTransform;
	[SerializeField] Transform backWheelTransform;
	[SerializeField] Transform pedalsTransform;
	[SerializeField] Transform pedalR;
	[SerializeField] Transform pedalL;

	[SerializeField] TrailRenderer frontTrail;
	[SerializeField] TrailRenderer rearTrail;
	public float localNum = 100;

	// Start is called before the first frame update
	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		bv = this;
		StopEmitTrail();
		rb = GetComponent<Rigidbody>();		
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if(GetComponent<CheckEngineAssembly>().IsPedalMotor){
			HandleEngine();
		}
		GetInput();
		HandleSteering();
		UpdateWheels();
		UpdateHandles();
		LayOnTurn();
		DownPressureOnSpeed();
		EmitTrail();
	}
	

	void Update(){
		TurnoversOnStart();
		Pedals();
		CheckEngine();
		Clutch();
	}
	//Звуки педалей и кручение
	void Pedals(){
		UpdatePedals();
		if(verticalInput>0 && !MotorStarted){
			if(!isPedalsSound){
				pedalsAudioSource.Play();
				isPedalsSound = true;
				pedalsAudioSource.pitch = rb.velocity.magnitude/10;
				if(pedalsAudioSource.pitch<0.45f){
					pedalsAudioSource.pitch = 0.45f;
				}
			}
			if(pedalsAudioSource.pitch<1.5f){
				pedalsAudioSource.pitch += rb.velocity.magnitude/10000;
			}
		}
		else if(verticalInput>0 && MotorStarted && clutch){
			if(!isPedalsSound){
				pedalsAudioSource.Play();
				isPedalsSound = true;
				pedalsAudioSource.pitch = rb.velocity.magnitude/10;
				if(pedalsAudioSource.pitch<0.45f){
					pedalsAudioSource.pitch = 0.45f;
				}
			}
			if(pedalsAudioSource.pitch<1.5f){
				pedalsAudioSource.pitch += rb.velocity.magnitude/10000;
			}
		}
		else {
			pedalsAudioSource.Stop();
			isPedalsSound = false;
			if(pedalsAudioSource.pitch>0.45f){
				pedalsAudioSource.pitch-=Time.deltaTime/5;
			}
		}
	}
	//заглушить мотор
	public void EngineShutDown(){
		MotorStarted=false;
		isLaunchedSound = false;
		audioSource.Stop();
		exhaustSystem.Stop();
	}
	IEnumerator ShutDownEngine(){
        yield return new WaitForSeconds(12);
		EngineShutDown();
		canStart = false;
		CurrentTurnover = 0;
    }
	public void CheckEngine(){
		//условия для заглушения мотора
		if(MotorStarted&&CurrentTurnover<0){
			EngineShutDown();
		}
		if(!GetComponent<CheckEngineAssembly>().IsFuel){
			if(CurrentTurnover>0){
				CurrentTurnover-=Time.deltaTime/20;
			}
			StartCoroutine(ShutDownEngine());
		}
		//сила мотора без сцепления
		if(MotorStarted && !clutch){
			currentForce = motorForce;
		}
		else if(verticalInput > 0){
			currentForce = pedalForce;
			//UpdatePedalSteering();
		}
		else if(verticalInput < 0){
			currentForce = pedalForce - 5;
			//UpdatePedalSteering();
		}

		//начало работы мотора
		if(MotorStarted && !isLaunchedSound){
			audioSource.clip = launchedMotorSound;
			audioSource.Play();
			exhaustSystem.Play();
			isLaunchedSound = true;
			isLaunchSound = false;
		}
		//процесс запуска мотора
		if(clutch){
			if(!MotorStarted && !isLaunchSound && verticalInput > 0 && canStart){
				audioSource.clip = launchMotorSound;
				audioSource.Play();
				isLaunchSound = true;
			}
		}
		//прекращение запуска при отпускании сцепления если мотор не запустился
		if(!MotorStarted && !clutch){
			audioSource.Stop();
			isLaunchSound = false;
			CurrentTurnover = 0;
		}
	}
	void TurnoversOnStart(){
		if(MotorStarted){
			if(verticalInput>0){
				if(CurrentTurnover<minTurnover){
					CurrentTurnover = minTurnover;
				}
			}

		}
		
	}

	public void GetInput()
	{
		horizontalInput = Input.GetAxis("Horizontal");
		verticalInput = Input.GetAxis("Vertical");
		braking = Input.GetKey(KeyCode.Space);
		clutch = Input.GetKey(KeyCode.LeftShift);
	}
	public void TurnoversUpdate(){
		//передний ход
		if(verticalInput >0){
			currentForce = motorForce;
			float plus = 0;
			if(CurrentTurnover<0.7){
		 		plus =0.1f;
			}
			CurrentTurnover = (rb.velocity.magnitude * 3.6f)/TurnoverPlusOnGaz + plus;
			
		}
		//тормоз, задний ход
		else if(verticalInput <0){
			currentForce = pedalForce - 10;
			CurrentTurnover = (rb.velocity.magnitude * 3.6f)/TurnoverPlusOnGaz;
		}
		//условие падения оборотов
		else {
			if(!clutch)CurrentTurnover -= TurnoverMinusOnHolostie;
		}

	}
	void LaunchEngine(){
		//условие запуска мотора
		if(!MotorStarted && canStart && Input.GetKey(KeyCode.LeftShift)){
			CurrentTurnover = (rb.velocity.magnitude * 3.6f)/TurnoverPlusOnGaz;
			if(CurrentTurnover > 0.7f){
				MotorStarted = true;
			}
		}
	}

	public void HandleEngine()
	{
		//езда на силе двигателя без сцепления
		if(MotorStarted && !clutch){
			TurnoversUpdate();
		}
		else if(!MotorStarted && clutch){
			LaunchEngine();
		}
		backWheel.motorTorque = verticalInput * currentForce;
		
		currentBrakeForce = braking ? brakeForce : 0f;
		//If we are not braking, ApplyBreaking applies a brakeForce of 0, so no conditional is needed
		ApplyBraking();
	}

	void Clutch(){
		//сцепление
		if(GetComponent<CheckEngineAssembly>().IsClutch){
			if(clutch){
				if(MotorStarted){
					if(CurrentTurnover < minTurnover){
						CurrentTurnover = minTurnover;
					}
					else{
						CurrentTurnover -= TurnoverMinusOnHolostie;
					}
				}
				else if(verticalInput>0){
					currentForce+=10;
				}
			}
		}
	}

	//Applies downforce, to keep the bike from bouncing off the ground over small bumps.
	public void DownPressureOnSpeed()
	{
		Vector3 downforce = Vector3.down; 
		float downPressure;
		if (rb.velocity.magnitude > 5)
		{
			downPressure = rb.velocity.magnitude;
			rb.AddForce(downforce * downPressure, ForceMode.Force);
		}
	}

	public void ApplyBraking()
	{
		frontWheel.brakeTorque = currentBrakeForce;
		backWheel.brakeTorque = currentBrakeForce;
	}

	//This function caps the maximum angle you can turn the front wheel.
	//It used to step up and down with speed, now it uses an exponential decay function.
	public void SpeedSteeringReductor() 
	{
		maxSteeringAngle = Mathf.LerpAngle(
			maxSteeringAngle, 
			//These two magic numbers were derived by doing a linear regression (on constants from the 20 lines that this replaced).
			Mathf.Clamp(66.614f * Mathf.Pow(0.893982f, rb.velocity.magnitude), 5, 50), 
			speedSteerControlTime
		);			
	}

	public void HandleSteering()
	{
		SpeedSteeringReductor();

		currentSteeringAngle = Mathf.Lerp(currentSteeringAngle, maxSteeringAngle * horizontalInput, turnSmoothing);
		frontWheel.steerAngle = currentSteeringAngle;

		//We set the target laying angle to the + or - input value of our steering 
		//We invert our input for rotating in the ocrrect axis
		targetLayingAngle = maxLayingAngle * -horizontalInput;		
	}
	private void LayOnTurn()
	{
		Vector3 currentRot = transform.rotation.eulerAngles;

		//Case: not moving much
		if (rb.velocity.magnitude < 1)
		{
			layingAmount = Mathf.LerpAngle(layingAmount, 0f, 0.05f);		
			transform.rotation = Quaternion.Euler(currentRot.x, currentRot.y, layingAmount);
			return;
		}
		//Case: Not steering or steering a tiny amount
		if (currentSteeringAngle < 0.5f && currentSteeringAngle > -0.5  )
		{
			layingAmount =  Mathf.LerpAngle(layingAmount, 0f, leanSmoothing);			
		}
		//Case: Steering
		else
		{
			layingAmount = Mathf.LerpAngle(layingAmount, targetLayingAngle, leanSmoothing );		
			rb.centerOfMass = new Vector3(rb.centerOfMass.x, COG.y, rb.centerOfMass.z);
		}

		transform.rotation = Quaternion.Euler(currentRot.x, currentRot.y, layingAmount);
	}

	public void UpdateWheels()
	{
		UpdateSingleWheel(frontWheel, frontWheelTransform);
		UpdateSingleWheel(backWheel, backWheelTransform);
		
	}

	public void UpdateHandles()
	{		
		Quaternion sethandleRot;
		sethandleRot = frontWheelTransform.rotation;		
		handle.localEulerAngles = new Vector3(handle.localEulerAngles.x, currentSteeringAngle, handle.localEulerAngles.z);
	}

	private void EmitTrail() 
	{	
		frontTrail.emitting = frontWheel.GetGroundHit(out WheelHit Fhit);
		rearTrail.emitting = backWheel.GetGroundHit(out WheelHit Rhit);
	}

	private void StopEmitTrail() 
	{
		frontTrail.emitting = false;
		rearTrail.emitting = false;
	}

	private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
	{
		Vector3 position;
		Quaternion rotation;
		wheelCollider.GetWorldPose(out position, out rotation);
		wheelTransform.rotation = rotation;
		wheelTransform.position = position;
	}
	private void UpdatePedals(){
		if(isPedalsSound){
			pedalsTransform.Rotate(Vector3.right,pedalsAudioSource.pitch*Time.deltaTime*750);
			pedalL.Rotate(Vector3.right,pedalsAudioSource.pitch*Time.deltaTime*-750);
			pedalR.Rotate(Vector3.right,pedalsAudioSource.pitch*Time.deltaTime*750);
		}
	}
	
}
