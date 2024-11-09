using UnityEngine;
using UnityEngine.UI;

public class AbsorbTimer : MonoBehaviour
{
   public Slider timerSlider;

    private float lerpSpeed = 0.03f;
    private float timeLeft;

    private void Start()
    {
        timerSlider.value = 5;
    }

    private void Update()
    {
        if(timerSlider.value != DurachokAbsorption.instance.timeLeft)
        {
            timerSlider.value = DurachokAbsorption.instance.timeLeft;
        }

        /**
        if(armorSlider.value != SliderDelay.value)
        {
            SliderDelay.value = Mathf.Lerp(SliderDelay.value,  (PlayerHealth.instance.health / 100), lerpSpeed);
        }**/
    }
}
