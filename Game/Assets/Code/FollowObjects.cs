using UnityEngine;
using System;

public class FollowObjects:MonoBehaviour
	{
	public Vector2 Offset;
	public Transform Following;

	public void Update()
	{
				transform.position = Following.transform.position + (Vector3)Offset;
		}
	}


