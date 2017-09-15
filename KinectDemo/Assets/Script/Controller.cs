using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

    public float speed = 5;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0)) {
            transform.position += Vector3.right * Time.deltaTime * Input.GetAxis("Mouse X") * speed;
            transform.position += Vector3.up * Time.deltaTime * Input.GetAxis("Mouse Y") * speed;
        }
    }

    private void OnMouseDrag()
    {
        
    }
}
