using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessManager : MonoBehaviour
{

    public static PostProcessManager Instance;
    private PostProcessVolume volume;
    private ChromaticAberration chromaticAberration;

    private void Awake()
    {
        Instance = this;
        volume = GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings<ChromaticAberration>(out chromaticAberration);
        //chromaticAberration.intensity;
    }

    //色差效果
    public void ChromaticAberrationEF()
    {
        //先停止可能在运行的协程
        StopChromaticAberrationEF();
        StartCoroutine("StartChromaticAberrationEF");
    }

    IEnumerator StartChromaticAberrationEF()
    {
        //递增到0.5
        while (chromaticAberration.intensity < 0.5f)
        {
            yield return new WaitForSeconds(0.01f);
            chromaticAberration.intensity.value += 0.04f;
        }
        yield return StopChromaticAberrationEF();
    } 

    IEnumerator StopChromaticAberrationEF()
    {
        //递增到0.5
        while (chromaticAberration.intensity > 0)
        {
            yield return new WaitForSeconds(0.01f);
            chromaticAberration.intensity.value -= 0.04f;
        }
    }


}
