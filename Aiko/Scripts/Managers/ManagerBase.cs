using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public abstract class ManagerBase<TSelf> : MonoBehaviour where TSelf : ManagerBase<TSelf>
{
    public static TSelf Instance { get; protected set; }

    protected virtual void Awake()
    {
        SetInstance();
        DontDestroyOnLoad(this);
    }

    protected virtual void OnEnable()
    {
        AssemblyReloadEvents.afterAssemblyReload += SetInstance;
    }

    protected virtual void OnDisable()
    {
        AssemblyReloadEvents.afterAssemblyReload -= SetInstance;
    }

    protected virtual void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void SetInstance()
    {
        if (!Instance)
        {
            print("Setting instance of " + typeof(TSelf).FullName);
            Instance = (TSelf)this;
        }
    }
}
