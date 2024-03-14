using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public static SkinManager instance;
    [SerializeField] Material defaultMaterial;

    private void Awake() 
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(this);
    }

    public static SkinManager GetInstance()
    {
        return instance;
    }

    public Material GetSkin()
    {
        return defaultMaterial;
    }

    public void SetSkin(Material skin)
    {
        defaultMaterial = skin;
    }
}
