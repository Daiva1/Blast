using UnityEngine;
using System;

public class FinishLevel1:MonoBehaviour
	{
	public string LevelName;
	public AudioClip PlayerWinSound;

		public void OnTriggerEnter2D(Collider2D other)
	{
		AudioSource.PlayClipAtPoint (PlayerWinSound, transform.position);
				if (other.GetComponent<Player> () == null)
						return;
				LevelManager.Instance.GotoNextLevel (LevelName);

		}
	}

