using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    public Slider sliderMana;

    public void SetMaxMana(float mana)
    {
        sliderMana.maxValue = mana;
        sliderMana.value = mana;
    }

    public void SetMana(float mana)
    {
        sliderMana.value = mana;
    }
}
