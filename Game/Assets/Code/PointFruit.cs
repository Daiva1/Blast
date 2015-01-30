using System;
using UnityEngine;

		public class PointFruit: MonoBehaviour, IPlayerRespawnListener
		{
	public GameObject Effect;
	public int PointsToAdd = 10;
	public AudioClip HitFruitSound;

	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.GetComponent<Player> () == null)
						return;
		if (HitFruitSound != null)
						AudioSource.PlayClipAtPoint (HitFruitSound, transform.position);

		GameManager.Instance.AddPoints (PointsToAdd);
		Instantiate (Effect, transform.position, transform.rotation);

		gameObject.SetActive (false);

		FloatingText.Show (string.Format ("+{0}", PointsToAdd), "PointFruitText", new WorldPointTextPosit (Camera.main, transform.position, 1.5f, 50));
		}
	public void OnPlayerRespawnInThisCheckpoint(Checkpoint checkpoint, Player player)
	{
		gameObject.SetActive (true);
		}
		}


