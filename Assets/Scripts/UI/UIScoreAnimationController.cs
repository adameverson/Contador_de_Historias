﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIScoreAnimationController : MonoBehaviour {
	public Image ScoreBg;
	public Text text;
	public ParticleSystem Particles;
	public AudioClip SoundSwish;


	bool isSegmentAnimation = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//ScoreBg.color -= new Color (0, 0, 0, Time.deltaTime);
	}

	public void ShowBgEffect()
	{
		//ScoreBg.color = Color.white;
		Particles.Play ();

		if (GameplayController.Instance != null) {
			GameplayController.Instance.FlushLevelScore ();
		}

		if (AudioController.Instance != null) {
			AudioController.Instance.PlaySound (SoundSwish, .5f);
		}
	}

	public void EndAnimation()
	{
		if (isSegmentAnimation) {
//			GameplayController.Instance.EndSegmentWinAnimation ();
		} else {
			Destroy (gameObject);
		}
	}

	public void Play()
	{
		isSegmentAnimation = true;
		GetComponent<Animation>().Play("AddScoreAnimation");
		text.text = GameplayController.Instance.AddLevelScoreToFlush.ToString ();
	}

	public void Play(int sum)
	{
		isSegmentAnimation = false;
		GetComponent<Animation>().Play("AddScoreAnimation");
		text.text = sum.ToString ();
	}


}
