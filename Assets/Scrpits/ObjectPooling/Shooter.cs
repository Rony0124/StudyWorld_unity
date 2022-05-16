using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {
    private Camera camera;
    // Start is called before the first frame update
    void Start() {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButton(0)) {
            RaycastHit hitResult;
            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hitResult)) {
                var bullets = ObjectPool.GetPoolingObject();
                var direction = new Vector3(hitResult.point.x, transform.position.y, hitResult.point.z) - transform.position;
                bullets.transform.position = direction.normalized;
                bullets.Shoot(direction.normalized);
            }
        }
    }
}
