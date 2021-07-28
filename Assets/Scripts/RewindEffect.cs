using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
public class RewindEffect : MonoBehaviour
{
    public PostProcessVolume volume; // reference to the post-processing component

    LensDistortion lens; //
    ColorGrading color;  //
    Vignette vignette;   // references to the atributes to modify

    public float defaultVignette = .4f; //
    public float maxVignette = .5f;     //
    public float vignetteSpeed = 6;     //
    public float defaultLens = 10;      //
    public float maxLens = 40;          //
    public float lensSpeed = 6;         //
    public float defaultSaturation = 0; //
    public float maxSaturation = -45;   //
    public float saturationSpeed = 6;   // default, max and speed values for each attribute
    private void Start()
    {
        volume.profile.TryGetSettings(out lens);     //
        volume.profile.TryGetSettings(out color);    //
        volume.profile.TryGetSettings(out vignette); // get references from the post-processing component
        lens.intensity.value = defaultLens;         //
        color.saturation.value = defaultSaturation; //
        vignette.intensity.value = defaultVignette; // set the initial value of the attributes to the prefered one
    }

    public void DoRewindEffect()
    {
        // increase the attribute's values from the current one towards the max value with the speed specified times Time.deltaTime to keep the speed constant
        // Mathf.Lerp() is kinda like Vector3.MoveTowards() for math, to explain it simply, I don't know what "interpolate" means
        lens.intensity.value = Mathf.Lerp(lens.intensity.value, maxLens, lensSpeed * Time.deltaTime);
        color.saturation.value = Mathf.Lerp(color.saturation.value, maxSaturation, saturationSpeed * Time.deltaTime);
        vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, maxVignette, vignetteSpeed * Time.deltaTime);
    }

    public void StopRewindEffect()
    {
        // same logic as DoRewindEffect() but in reverse
        lens.intensity.value = Mathf.Lerp(lens.intensity.value, defaultLens, lensSpeed * Time.deltaTime);
        color.saturation.value = Mathf.Lerp(color.saturation.value, defaultSaturation, saturationSpeed * Time.deltaTime);
        vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, defaultVignette, vignetteSpeed * Time.deltaTime);
    }
}
