using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    [SerializeField] private GameObject gameObjectPrefab;

    private Queue<Bullet> prefabQueue = new Queue<Bullet>();

    private void Awake() {
        Instance = this;
        Initialize(10);
    }

    private void Initialize(int initCount) {
        for(int i = 0; i < initCount; i++) {
            //큐안에 미리 생성해두는 
            prefabQueue.Enqueue(CreatePoolingObject());
        }
    }

    private Bullet CreatePoolingObject() {
        Bullet newObj = Instantiate(gameObjectPrefab).GetComponent<Bullet>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }
    public static Bullet GetPoolingObject() {
        if(Instance.prefabQueue.Count > 0) {
            Bullet obj = Instance.prefabQueue.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else {
            Bullet newObj = Instance.CreatePoolingObject();
            newObj.transform.SetParent(null);
            newObj.gameObject.SetActive(true);
            return newObj;
        }
    }
    public static Bullet ReturnObject(Bullet obj) {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        return obj;
    }

}
