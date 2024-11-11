using UnityEngine;
using UnityEngine.UI;

public class AbsorbTimer : MonoBehaviour
{
    public Slider timerSlider;

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
    }
}
