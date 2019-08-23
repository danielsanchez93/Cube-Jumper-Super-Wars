using UnityEngine;

public abstract class PoolObject: MonoBehaviour
{
    public abstract void InitializePoolObject(PoolObjectParams parameters);
    public int amountToPool;
    public delegate void OnPoolObjectFinished();
    public event OnPoolObjectFinished onPoolObjectFinished;
    

    private void Update()
    {
        if(onPoolObjectFinished != null)
            onPoolObjectFinished.Invoke();
    }
}
