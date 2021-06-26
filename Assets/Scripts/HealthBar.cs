using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public void SetHealthBarValue(float ratio) {
        gameObject.GetComponent<Slider>().value = ratio;
    }
}
