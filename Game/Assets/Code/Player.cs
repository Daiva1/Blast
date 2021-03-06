﻿using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour {
	private bool _isFacingRight;
	private PlayerControl _controller;
	private float _normalizedHorizontalSpeed;

	public float MaxSpeed = 8;
	public float SpeedAccelerationOnGround = 10f;
	public float SpeedAccelerationInAir = 5f;
	public float speed = 0.2F;

	public int MaxHealth = 100;
	public GameObject OuchEffect;
	public AudioClip PlayerHitSound;

	public int Health { get; private set; }
	public bool IsDead{ get; private set; }


	public void Awake()
	{
		_controller = GetComponent<PlayerControl> ();
		_isFacingRight = transform.localScale.x > 0;
		Health = MaxHealth;
		}
	public void Update()
	{
				if (!IsDead) 
			//HandleInput ();
			TouchInput (); 
			var movementFactor = _controller.State.IsGrounded ? SpeedAccelerationOnGround : SpeedAccelerationInAir;
				
				if (IsDead)
						_controller.SetHorizontalForce (0);
				else
						_controller.SetHorizontalForce (Mathf.Lerp (_controller.Velocity.x, _normalizedHorizontalSpeed * MaxSpeed, Time.deltaTime * movementFactor));
		}
public void FinishLevel()
{
	enabled = false;
	_controller.enabled = false;
		collider2D.enabled = false;
		}

	public void Kill()
	{
		_controller.HandleCollisions = false;
		collider2D.enabled = false;
		IsDead = true;
		Health = 0;
		_controller.SetForce (new Vector2 (0, 20));
		}

	public void RespawnAt(Transform spawnPoint)
	{
		if (!_isFacingRight)
						Flip ();
		IsDead = false;
		collider2D.enabled = true;
		_controller.HandleCollisions = true;
		Health = MaxHealth;

		transform.position = spawnPoint.position;
		}

	public void TakeDamage(int damage)
	{
		AudioSource.PlayClipAtPoint (PlayerHitSound, transform.position);
		FloatingText.Show (string.Format ("-{0}", damage), "PlayerTakeDamageText", new WorldPointTextPosit (Camera.main, transform.position, 2f, 60f));

		Instantiate (OuchEffect, transform.position, transform.rotation);
		Health -= damage;
		if (Health <= 0)
						LevelManager.Instance.KillPlayer ();
		}

	private void HandleInput ()
	{
		if (Input.GetKey (KeyCode.D)) {
			_normalizedHorizontalSpeed = 1;
			if (!_isFacingRight)
				Flip ();
		} else if (Input.GetKey (KeyCode.A)) {
			_normalizedHorizontalSpeed = -1;
			if (_isFacingRight)
				Flip ();
		} else {
			_normalizedHorizontalSpeed = 0;
		}
		if (_controller.CanJump && Input.GetKeyDown (KeyCode.Space)) {
			_controller.Jump ();
		}
	}

	private void TouchInput()
	{
				if (Input.touchCount > 0) {
						int i = 0;
						for (i = 0; i < Input.touchCount; i++) {
						Touch touch = Input.GetTouch (i);
						if (touch.position.x < Screen.width / 2) {
						if (touch.phase == TouchPhase.Stationary)            
							_normalizedHorizontalSpeed = -1;
							if (_isFacingRight)
								Flip ();
								}
						if (touch.position.x > Screen.width / 2) {
						if (touch.phase == TouchPhase.Stationary)
							_normalizedHorizontalSpeed = 1;
						if (!_isFacingRight)
								Flip ();
								}
				if (_controller.CanJump && touch.phase == TouchPhase.Moved) {
					_controller.Jump ();
				}
						}
				} else {
						_normalizedHorizontalSpeed = 0;
				}
		}
	private void Flip() 
	{
				transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
				_isFacingRight = transform.localScale.x > 0;
		}
}
