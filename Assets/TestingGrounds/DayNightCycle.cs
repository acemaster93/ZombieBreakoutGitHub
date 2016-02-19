﻿ using UnityEngine;
using System.Collections;

public class DayNightCycle : MonoBehaviour {

	[ReadOnlyAttribute]
	public string CurrentTime;

	[HideInInspector]
	public float TimeFloat;

	public GameObject Star;
	public int StarMaxParticles;
	public float StarAppearanceTime;

	[Tooltip("Must not be 0 !")]
	public int StarDisappearingRate = 1;
	public float StarDisappearingSpeed;

	private float Counter;
	private bool CanDelete = false;

	public Light moonlight;
	public float targetIntensity;
	public float fadeSpeed;

	public DayNightTrigger[] DayLightZombieTrigger;

	void Start () 
	{
		moonlight.intensity = 0.1f;
	}
	
	void Awake()
	{
//		TimeFloat = 24f;
	}

	void Update () 
	{
//		transform.Rotate(new Vector3(0,CycleSpeed*Time.deltaTime));
		//no need this because there's AutoIntensity to rotate the sun
		TimeFloat = ((transform.localRotation.eulerAngles.y)/90)*6;
		CurrentTime = TimeFloat.ToString("0.00");

		for(int i = 0; i < DayLightZombieTrigger.Length; i++)
		{
			if(TimeFloat >= DayLightZombieTrigger[i].StartEffectAt && DayLightZombieTrigger[i].StartEffectAt + DayLightZombieTrigger[i].EffectDuration >= TimeFloat)
			{
//				print(DayLightZombieTrigger[i].SayWhat);

			}
		}

		if(TimeFloat >= StarAppearanceTime)
		{
			Star.GetComponent<ParticleSystem>().maxParticles = StarMaxParticles;
			CanDelete = true;
		}
		else
		{
			if(CanDelete)
			DeleteStars();
		}

		if(TimeFloat >= 13)
		{
			moonlight.intensity = Mathf.Lerp(moonlight.intensity, targetIntensity, fadeSpeed * Time.deltaTime);
		}
	}

	[System.Serializable]//this is to make this array show up in the inspector
	public class DayNightTrigger
	{
		public float StartEffectAt;
		public float EffectDuration;
		public string SayWhat;

	}

	void DeleteStars()
	{
		if(Counter >= StarDisappearingRate)
		{
			if(Star.GetComponent<ParticleSystem>().maxParticles > 0)
			{
				Star.GetComponent<ParticleSystem>().maxParticles = Star.GetComponent<ParticleSystem>().maxParticles - StarDisappearingRate;
				Counter = 0;
			}
			else
			{
				CanDelete = false;
			}
		}
		else
		{
			Counter += StarDisappearingSpeed*Time.fixedDeltaTime;
		}
	}

}
