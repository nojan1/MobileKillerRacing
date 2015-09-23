using UnityEngine;
using System.Collections;

public class RotatingObject : MonoBehaviour {

    public float rotationSpeed = 6f; 

	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0f, rotationSpeed * Time.deltaTime, 0f));
	}
}
