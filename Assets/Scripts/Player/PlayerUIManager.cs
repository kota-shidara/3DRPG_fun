using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerUIManager : MonoBehaviour
{

    public Slider hpSlider;
    public Slider staminaSlider;

    public void Init(PlayerManager playerManager)
    {
        hpSlider.maxValue = playerManager.maxHp;
        hpSlider.value = playerManager.maxHp;
        staminaSlider.maxValue = playerManager.maxStamina;
        staminaSlider.value = playerManager.maxStamina;
    }

    public void UpdateHP(int hp)
    {
        hpSlider.DOValue(hp, 0.3f);
    }

    public void UpdateStamina(int stamina)
    {
        staminaSlider.DOValue(stamina, 0.3f);
    }
}
