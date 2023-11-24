using UnityEngine;

public abstract class SingletonMb<T> : MonoBehaviour where T : SingletonMb<T>
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        Instance = (T)this;
    }
}