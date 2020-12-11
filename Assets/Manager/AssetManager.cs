using UnityEditor;
using UnityEngine;

// 에셋 매니져
// 기본적인 메테리얼을 불러옴
// 나중에 게임에서 사용하는 에셋을 동적생성하는 기능으로 만들 생각
public class AssetManager : BaseManager
{
    // 자주 사용할듯한 기본 메테리얼 이름
    public const string DEFAULT_MAT = "Default-Material.mat";
    public const string DEFAULT_MAT_LINE = "Default-Line.mat";
    public const string DEFAULT_MAT_SPRITE = "Sprite_Default.mat";
    public const string DEFAULT_MAT_PARTICLE = "Default-Particle.mat";

    // 기본 에셋 불러오기
    public T GetDefaultAsset<T> (string defaultAseetName) where T : Object
    {
#if UNITY_EDITOR
        return AssetDatabase.GetBuiltinExtraResource<T> (defaultAseetName);
#else
        return Resources.GetBuiltinResource<T> (defaultAseetName);
#endif
    }

}
