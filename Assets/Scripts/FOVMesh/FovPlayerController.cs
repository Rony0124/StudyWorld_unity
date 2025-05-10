using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FovPlayerController : MonoBehaviour
{
    [SerializeField]
    float speed = 8.0f;

    Rigidbody rigidbody;
    Camera viewCamera;
    Vector3 velocity;
    float rotDegree;

    void Start()
    {
        rigidbody = transform.GetComponent<Rigidbody>();
        viewCamera = Camera.main;
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = viewCamera.ScreenToWorldPoint(mousePos);

        float dz = mousePos.z - rigidbody.position.z;
        float dx = mousePos.x - rigidbody.position.x;
        rotDegree = -(Mathf.Rad2Deg * Mathf.Atan2(dz, dx) - 90);
        velocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized * speed;
    }

    void FixedUpdate()
    {
        rigidbody.MoveRotation(Quaternion.Euler(0, rotDegree, 0));
        rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime);
    }
}
