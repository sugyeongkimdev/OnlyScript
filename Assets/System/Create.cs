using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Create
{



    public static GameObject Add<T> (this GameObject go) where T : Component
    {
        return go.AddComponent<T> ().gameObject;
    }
    public static GameObject Add<T> (this T component) where T : Component
    {
        return component.gameObject.Add<T> ();
    }


}
