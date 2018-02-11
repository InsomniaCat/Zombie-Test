using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour {

    public float speed;
    
	void Update ()
    {
        transform.position += transform.rotation * new Vector3(0, speed * Time.deltaTime, 0);
	}
}
