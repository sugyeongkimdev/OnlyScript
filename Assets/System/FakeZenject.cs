using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

//=========================================================//
// idea https://github.com/modesttree/Zenject

// 성능 포기함 + 자주 쓰는 젠젝트 기능만 그럴싸하게 만듬
// single에 자동생성기능까지 넣을까 했지만 복잡해질까봐 그만 둠

//=========================================================//
// 매니저 어트리뷰트, 자동 생성 및 싱글톤으로 관리됨
// 제한 조건 : public으로만 주입
[AttributeUsage (AttributeTargets.Field)] public class ManagerAttribute : Attribute { }

// 싱글 어트리뷰트, 매니저와 같이 싱글톤으로 관리됨
// 제한조건 : 아마도 public으로만 주입, 사용할 때 테스트 해야함
[AttributeUsage (AttributeTargets.Field)] public class SingleAttribute : Attribute { }

//=========================================================//
public static class FakeZenject
{
    private static Dictionary<Type, object> instnaceDic = new Dictionary<Type, object> ();

    // 연결
    public static T Bind<T> (this T bindTarget) where T : class, new()
    {
        instnaceDic.Add (bindTarget.GetType (), bindTarget);
        return bindTarget;
    }
    // 해제
    public static void Release (this Type releaseType)
    {
        instnaceDic.Remove (releaseType);
    }
    public static void Release<T> (this T releaseTarget) where T : class, new()
    {
        instnaceDic.Remove (releaseTarget.GetType ());
    }
    //=========================================================//

    // null 청소
    public static void CleanUp ()
    {
        instnaceDic = instnaceDic
            .Where (dic => dic.Value != null)
            .ToDictionary (dic => dic.Key, dic => dic.Value);
    }
    //=========================================================//

    // 해당 객체에 주입
    public static T Inject<T> (this T injectTarget) where T : class, new()
    {
        // 해당하는 필드를 찾고
        // 해당하는 어트리뷰트 타입을 찾은 뒤 분류에 따라 실행
        typeof (T).GetFields ()
            .ForEach (field => field.GetCustomAttributes (true)
                .ForEach (att =>
                {
                switch(att)
                    {
                        //=========================================================//

                        // 매니저 주입
                        case ManagerAttribute manager:
                            var baseMangerType = typeof (BaseManager<>).MakeGenericType (field.FieldType);

                            // 이름을 정확히 가져오기 위해서 변경할 수 없는 제네릭을 아무거나 넣음
                            var instanceName = nameof (BaseManager<MonoBehaviour>.instasnce);   // 싱글톤 인스턴스 필드
                            var initName = nameof (BaseManager<MonoBehaviour>.Init);            // 초기화 메소드

                            // 인스턴스
                            var instanceField  = baseMangerType.GetField (instanceName, BindingFlags.Static | BindingFlags.Public);
                            var instance = instanceField.GetValue (null);

                            if(instance == null)
                            {
                                // 싱글톤 인스턴스가 비어있으면 생성을 하고 초기화를 함
                                instance = Activator.CreateInstance (field.FieldType);
                                baseMangerType.GetMethod (initName).Invoke (instance, null);
                                instanceField.SetValue (null, instance);
                            }

                            // 주입
                            field.SetValue (injectTarget, instance);
                            break;

                        //=========================================================//

                        // 싱글 주입
                        case SingleAttribute single:
                            instnaceDic.TryGetValue (field.FieldType, out var get);
                            field.SetValue (injectTarget, get);
                            break;
                    }
                })
           );

        return injectTarget;
    }
}
