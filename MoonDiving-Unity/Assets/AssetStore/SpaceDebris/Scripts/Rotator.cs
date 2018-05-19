using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Rotator : MonoBehaviour {

    public float x_rate = 0f;
    public float y_rate = 0f;
    public float z_rate = 0f;
    
    

	
	
	void Update () {

        transform.Rotate((Time.deltaTime * x_rate), (Time.deltaTime * y_rate), (Time.deltaTime * z_rate));
    }
}
