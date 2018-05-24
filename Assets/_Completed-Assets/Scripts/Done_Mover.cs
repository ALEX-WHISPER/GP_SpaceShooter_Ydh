using UnityEngine;
using System.Collections;

public class Done_Mover : MonoBehaviour
{
	public float speed;
    public bool moveAlongUpDir;

	void Start ()
	{
        if (moveAlongUpDir) {
            GetComponent<Rigidbody>().velocity = transform.up * speed;
        } else {
            GetComponent<Rigidbody>().velocity = transform.forward * speed;
        }
    }
}
