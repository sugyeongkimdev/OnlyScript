using System.Linq;
using UnityEngine;

// info
// https://stackoverrun.com/ko/q/10703839

// 사운드 매니저...
// audio asset은 사용하지 못하므로 코드로 소리를 만들어내야 함
// PCM

public class SoundManager : BaseManager
{

    //=========================================================//

    [Manager] public AssetManager asset;


    //=========================================================//

    public GameObject managerGo;
    public AudioSource audioSource;
    public LineRenderer audioLine;

    //=========================================================//

    public int position = 0;
    public const int samplerate = 44100;    // 보통 오디오 음원에서 사용하는 주파수(CD 표준, 샘플 속도)
    public const int chanel = 1;            // 채널. 1 모노, 2 스테레오?
    public float frequency = 440;           // 주파수


    public float[] data =new float[samplerate];

    //=========================================================//

    public override BaseManager Init ()
    {
        this.Inject ();

        managerGo = new GameObject ("SoundManager_Audio");
        audioSource = managerGo.AddT<AudioListener> ().AddT<AudioSource> ();
        audioLine = managerGo.AddT<LineRenderer> ();

        // TODO : 사운드 작성중
        AudioClip clip1 = AudioClip.Create ("MySinusoid1", samplerate * 2, chanel, samplerate, true, OnAudioRead, OnAudioSetPosition);
        AudioClip clip2 = AudioClip.Create ("MySinusoid2", samplerate, chanel, samplerate, false, OnAudioRead, OnAudioSetPosition);

        clip2.GetData (data, 0);

        // TODO : 사운드 뷰어 작성중
        // 이걸 만들어야 나중에 사운드 작업을 할 수 있을듯 함
        audioLine.positionCount = samplerate;
        audioLine.startWidth = 0.01f;
        audioLine.endWidth = 0.01f;

        audioLine.material = asset.GetDefaultAsset<Material>(AssetManager.DEFAULT_MAT_LINE);
        var pos = data.Select ((d, i) => new Vector3 (i/ (samplerate/100f), d)).ToArray();
        audioLine.SetPositions (pos);

        
        //Debug.Log (data);

        //Play (clip1);
        Play (clip2);

        return this;
    }

    //=========================================================//

    public void Play (AudioClip clip)
    {
        audioSource.PlayOneShot (clip);
    }

    public void DrawSound ()
    {
        //Vector2 oldPos = Vector2.zero;
        //Vector2 newPos = Vector2.zero;
        //int lneg = 0;
        //foreach(var d in sound.data)
        //{
        //    newPos = new Vector2 (lneg / (float)sound.data.Length * 100f, d);
        //    Gizmos.DrawLine (oldPos, newPos);
        //    oldPos = newPos;
        //    lneg++;
        //}
        //Debug.Log (newPos);
    }

    //=========================================================//


    void OnAudioRead (float[] data)
    {
        int count = 0;

        while(count < data.Length)
        {
            data[count] = Mathf.Sin (2 * Mathf.PI * frequency * position / samplerate);


            position++;
            count++;
        }
    }

    void OnAudioSetPosition (int newPosition)
    {
        position = newPosition;
    }
}
