using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Create
{


    // 기본
    public static T AddT<T> (this GameObject go) where T : Component
    {
        return go.AddComponent<T> ();
    }
    public static T AddT<T> (this Component component) where T : Component
    {
        return component.gameObject.AddComponent<T> ();
    }

    // 확장
    public static GameObject AddG<T> (this GameObject go) where T : Component
    {
        return go.AddComponent<T> ().gameObject;
    }
    public static GameObject AddG<T> (this Component component) where T : Component
    {
        return component.gameObject.AddComponent<T>().gameObject;
    }


}
