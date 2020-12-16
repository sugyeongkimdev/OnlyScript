using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 제작 래스스
// 게임에서 사용하는 게임 오브젝트 관련 생성
public static class Create
{
    //=========================================================//
    // 컴포넌트

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

    //=========================================================//

    public static Camera Camera ()
    {
        var cam = new GameObject ("MainCamera").AddT<Camera> ();
        cam.transform.position = new Vector3 (0, 0, -10f);
        return cam;
    }
    public static Canvas Canvas ()
    {
        var canvas = new GameObject ("Canvas").AddT<Canvas> ();
        canvas.transform.position = new Vector3 (0, 0, -10f);
        return canvas;
    }
}
