using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 direction;
    public void Shoot(Vector3 direction) {
        this.direction = direction;
        //5초뒤에 풀링에 반환하도록 해준다
        Invoke("DestroyBullet", 5f);
    }
    public void DestroyBullet() {
        ObjectPool.ReturnObject(this);
    }
    void Update() {
        transform.Translate(direction);
    }

}
