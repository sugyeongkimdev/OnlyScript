using System;
using UnityEngine;

public class Main : MonoBehaviour
{

    //=========================================================//

    // 유니티 고유 속성을 이용한 코드 실행
    [RuntimeInitializeOnLoadMethod (RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Run ()
    {
        var mainGo = new GameObject ("Main", typeof (Main));
        mainGo.GetComponent<Main> ().Init ();
    }

    //=========================================================//

    [Manager] public SoundManager sound;
    [Single] public Camera cam;


    private void Init ()
    {
        this.Inject ();

        Create.Camera ().Bind ();

    }


    //=========================================================//

}
