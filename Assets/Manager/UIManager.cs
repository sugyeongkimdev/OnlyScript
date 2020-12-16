using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임에 관한 UI관리 매니저
// 작성중
public class UIManager : BaseManager<UIManager>
{
    public Canvas mainCanvas;

    public override UIManager Init ()
    {
        this.Inject ();
        return this;
    }

    

}
