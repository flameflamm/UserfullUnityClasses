using UnityEngine;

public abstract class MonoPool<T> : MonoSingleton<MonoPool<T>> where T : MonoBehaviour {
    public T ObjPrefab;
    public int PoolSize = 1000;

    protected T[] m_pool;
    protected int m_currentPoolIndex;

    private void Awake() {
        m_pool = new T[PoolSize];
        for(int i = 0; i < PoolSize; i++) {
            m_pool[i] = Instantiate(ObjPrefab, Instance.transform) as T;
            m_pool[i].gameObject.SetActive(false);
        }
    }

    public static T Take(Vector3 position, Quaternion rotation) {
        if (++Instance.m_currentPoolIndex >= Instance.m_pool.Length)
            Instance.m_currentPoolIndex = 0;

        Instance.m_pool[Instance.m_currentPoolIndex].gameObject.SetActive(false);
        Instance.m_pool[Instance.m_currentPoolIndex].transform.position = position;
        Instance.m_pool[Instance.m_currentPoolIndex].transform.rotation = rotation;
        Instance.m_pool[Instance.m_currentPoolIndex].gameObject.SetActive(true);

        return Instance.m_pool[Instance.m_currentPoolIndex];
    }
}