using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T: MonoSingleton<T>
{
    private static T _instance = null;
    public static bool IsAwake { get { return (_instance != null); } }

    public static T Instance {
        get {
            if (_instance == null) {
                _instance = (T)FindFirstObjectByType(typeof(T));
                if (_instance == null) {
                    string goName = typeof(T).ToString();

                    GameObject go = GameObject.Find(goName);
                    if (go == null) {
                        go = new GameObject();
                        go.name = goName;
                    }

                    _instance = go.AddComponent<T>();
                }
            }
            return _instance;
        }
    }

    public virtual void OnApplicationQuit()  {
        // release reference on exit
        _instance = null;
    }
}