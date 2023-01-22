using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance = null;
    public static T Instance => _instance;

    public bool isDontDestroyOnLoad = false;

    public virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            if (isDontDestroyOnLoad)
                DontDestroyOnLoad(this);
        }
    }
}