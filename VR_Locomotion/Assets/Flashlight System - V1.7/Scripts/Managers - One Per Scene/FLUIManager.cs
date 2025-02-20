using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FlashlightSystem
{
    public class FLUIManager : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private Image batteryLevelUI = null;
        [SerializeField] private Text batteryCountUI = null;
        [SerializeField] private Image flashlightIndicatorUI = null;
        [SerializeField] private CanvasGroup flashlightCanvas = null;

        [Header("Radial Indicator")]
        [SerializeField] private Image radialIndicatorUI = null;

        [Header("Crosshair")]
        [SerializeField] private Image CrosshairUI = null;

        private bool showUI;

        public static FLUIManager instance;

        private void Awake()
        {
            if (instance != null) { Destroy(gameObject); }
            else { instance = this; DontDestroyOnLoad(gameObject); }

            flashlightCanvas.alpha = 0;
        }

        public void UpdateBatteryUI(int batteryCount)
        {
            batteryCountUI.text = batteryCount.ToString("0");
        }

        public void UpdateBatteryLevelUI(float drainAmount)
        {
            batteryLevelUI.fillAmount -= drainAmount;
        }

        public void MaximumBatteryLevel(float maxIntensity)
        {
            batteryLevelUI.fillAmount = maxIntensity;
        }

        public void ToggleRadialIndicator(bool on)
        {
            radialIndicatorUI.enabled = on;
        }

        public void UpdateRadialIndicatorUI(float indicatorAmount)
        {
            radialIndicatorUI.fillAmount = indicatorAmount;
        }

        public void ToggleFlashlightInventory()
        {
            showUI = !showUI;
            flashlightCanvas.alpha = showUI ? 1 : 0;
        }

        public void FlashlightIndicatorColor(bool on)
        {
            if (on)
            {
                flashlightIndicatorUI.color = Color.white;
            }
            else
            {
                flashlightIndicatorUI.color = Color.black;
            }
        }

        public void HighlightCrosshair(bool on)
        {
            if (on)
            {
                CrosshairUI.color = Color.red;
            }
            else
            {
                CrosshairUI.color = Color.white;
            }
        }
    }
}
