using UnityEngine;
using System.Collections;
using System;

public class StartScreen:MonoBehaviour
	{

	private bool _isFirstMenu = true;
	private bool _isOptionsMenu = false;

	private bool _isAudioOptions = false;

	private float _gameVolume = 0.6f;

	public GUISkin GameSkin;
	//public string FirstLevel;

	public void Start ()
	{
		//PlayerPrefs.DeleteAll ();
				_gameVolume = PlayerPrefs.GetFloat ("Game Volume", _gameVolume);

				if (PlayerPrefs.HasKey ("Game Volume")) {
			AudioListener.volume = PlayerPrefs.GetFloat ("Game Volume", _gameVolume);
				} else {
						PlayerPrefs.SetFloat ("Game Volume", _gameVolume);
				}
		}

	public void Update()
	{
		//if (!Input.GetMouseButton (0))
						//return;
		//GameManager.Instance.Reset ();
		//Application.LoadLevel (FirstLevel);
	}
	public void OnGUI()
	{
		GUI.skin = GameSkin;

		FirstMenu ();
		OptionsMenu ();
	    AudioOptionsDisplay();

		if (_isOptionsMenu == true) {
			if(GUI.Button(new Rect(10, Screen.height - 80, 250, 60), "Back"))
			{
				_isOptionsMenu = false;
			    _isAudioOptions = false;

			    _isFirstMenu = true;
			}
				}
		}

	public void FirstMenu()
	{
		if(_isFirstMenu)
		{
			if(GUI.Button(new Rect(320, Screen.height /2-100, 250, 60), "Play Game"))
			{
				Application.LoadLevel ("Level1");
			}
			if(GUI.Button(new Rect(320, Screen.height /2-30, 250, 60), "Options"))
			{
				_isFirstMenu = false;
				_isOptionsMenu = true;
			}	
			if(GUI.Button(new Rect(320, Screen.height /2+40, 250, 60), "Exit"))
			{
				Application.Quit();
			}
		}
		}

	public void OptionsMenu()
	{
		if (_isOptionsMenu) 
		{
			//GUI.Label(new Rect(10, Screen.height / 2 - 120, 250, 100), "Options", "Sub Menu Title");
			
		if(_isAudioOptions == true){
				GUI.Box(new Rect(10, Screen.height /2-60, 350, 200), "");
		}
		    if(GUI.Button(new Rect(10, Screen.height /2-120, 250, 50), "Audio Options"))
			{
			_isAudioOptions = true;
			}
		}
	}

public void AudioOptionsDisplay()
{
	if(_isAudioOptions)
	{
			GUI.Label(new Rect(15, 180, 200, 40), "Game Volume:");
		_gameVolume = GUI.HorizontalSlider(new Rect(15, 230, Screen.width /2-250, 40), _gameVolume, 0.0f, 1.0f);
		AudioListener.volume = _gameVolume;
		if(GUI.Button(new Rect(50, Screen.height - 160, 240, 50), "Apply"))
		{
			PlayerPrefs.SetFloat("Game Volume", _gameVolume);
		}
	}
}
}


