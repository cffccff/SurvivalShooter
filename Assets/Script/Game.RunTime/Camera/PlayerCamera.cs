using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform target;
    [SerializeField]
    public float x, y=13f, z=-10.54f;
    private void Start()
    {
        transform.rotation = Quaternion.Euler(new Vector3(45f,0f,0f));
        y = 13f;
        z = -10.54f;
    }
    private void Update()
    {
        transform.position =new Vector3( target.transform.position.x+x, target.transform.position.y + y, target.transform.position.z+z);
    }
}
