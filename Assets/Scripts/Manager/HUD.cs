using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Combatant player;
    [SerializeField] Image fillImage;
    [SerializeField] TextMeshProUGUI totalCoinsText;

    private void Start()
    {
        slider.maxValue = player.getMaxHealth();
    }

    // Update is called once per frame
    void Update()
    {
        //this code removes the leftover red color in the health bar that is left by the fillImage in the slider
        if (slider.value <= slider.minValue)
        {
            fillImage.enabled = false;
        }
        else if (slider.value > slider.minValue && !fillImage.enabled)
        {
            fillImage.enabled = true;
        }

        //updates health bar
        float fillValue = player.getCurrentHealth();
        slider.value = fillValue;

        //updates coins text
        totalCoinsText.text = GameManager.gameManager.totalCoins + GameManager.gameManager.coins + "";
    }
}
