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
    //[Single] public Camera cam;


    private void Init ()
    {
        this.Inject ();

        Console.Beep (5000,1000);

        ManagerDataBind ();
    }

    // 자주 사용하는 데이터 바인드
    private void ManagerDataBind ()
    {
        new GameObject ("MainCamera").AddComponent<Camera> ().Bind (); // 메인 카메라 바인드
    }

    //=========================================================//

}
