using System;
using System.Collections.Generic;

// 유니티 편의성 확장
public static class UnityExtension
{


    // 열거 무명 실행자
    public static void ForEach<T> (this IEnumerable<T> enumor, Action<T> action)
    {
        foreach(var item in enumor)
        {
            action?.Invoke(item);
        }
    }

}
