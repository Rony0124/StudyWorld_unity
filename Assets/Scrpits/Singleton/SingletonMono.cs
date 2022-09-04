using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMono <T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance = null;
    private static bool _ShuttingDown = false;
    private static object _Lock = new object();

    public static T Instance {
        get {
            if (_ShuttingDown)
            {
                return null;
            }

            if (instance == null) {
                instance = (T) FindObjectOfType(typeof(T));
            }
            lock(_Lock){
                if (instance == null) {
                GameObject obj = new GameObject();
                obj.name = typeof(T).ToString();
                instance = obj.AddComponent<T>();

                GameObject manager = GameObject.Find("SingletonManager");
                if (manager == null) {
                    manager = new GameObject("SingletonManager");
                    manager.transform.SetAsFirstSibling();
                }

                DontDestroyOnLoad(manager);
                instance.transform.SetParent(manager.transform);
            }

            return instance;
            }
            
        }
    }
    private void OnApplicationQuit()
    {
        _ShuttingDown = true;
    }
 
    private void OnDestroy()
    {
        _ShuttingDown = true;
    }
}
