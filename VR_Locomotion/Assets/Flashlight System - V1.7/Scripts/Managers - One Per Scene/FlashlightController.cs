using UnityEngine;

namespace FlashlightSystem
{
    public class FlashlightController : MonoBehaviour
    {
        [Header("Flashlight On Start")]
        [SerializeField] private bool hasFlashlight = false;

        [Header("Inventory Toggle")]
        [Tooltip("If this is true, allows the user to toggle inventory on / off")]
        [SerializeField] private bool showFlashlightInventory = false;

        [Header("Infinite Flashlight")]
        [SerializeField] private bool infiniteFlashlight = false;

        [Header("Battery Parameters")]
        [SerializeField] private float batteryDrainAmount = 0.01f;
        [SerializeField] private int batteryCount = 1;

        [Header("Battery Reload Timers")]
        [SerializeField] private float replaceBatteryTimer = 1.0f;
        [SerializeField] private float maxReplaceBatteryTimer = 1.0f;

        [Header("Flashlight Parameters")]
        [Range(0, 10)] [SerializeField] private float maxFlashlightIntensity = 1.0f;
        [Range(1, 10)] [SerializeField] private int flashlightRotationSpeed = 2;

        [Header("Main Flashlight References")]
        [SerializeField] private Light flashlightSpot = null;
        [SerializeField] private FlashlightMovement flashlightMovement = null;

        [Header("Flashlight Sound Names")]
        [SerializeField] private Sound flashlightPickup = null;
        [SerializeField] private Sound flashlightClick = null;
        [SerializeField] private Sound flashlightReload = null;

        private bool shouldUpdate = false;
        private bool isFlashlightOn;

        public static FlashlightController instance;

        private void Awake()
        {
            if (instance != null) { Destroy(gameObject); }
            else { instance = this; DontDestroyOnLoad(gameObject); }
        }

        void Start()
        {
            flashlightSpot.intensity = maxFlashlightIntensity;
            flashlightMovement.speed = flashlightRotationSpeed;
            maxReplaceBatteryTimer = replaceBatteryTimer;
            FLUIManager.instance.UpdateBatteryUI(batteryCount);

            if (!showFlashlightInventory && !infiniteFlashlight)
            {
                print("You may want to make the flashlight infinite if you're not showing the flashlight UI");
            }
        }

        void ToggleInventory()
        {
            if (showFlashlightInventory)
            {
                FLUIManager.instance.ToggleFlashlightInventory();
            }
        }

        public void CollectFlashlight()
        {
            hasFlashlight = true;
            FlashlightPickupSound();
        }

        public void CollectBattery(int batteries)
        {
            batteryCount = batteryCount + batteries;
            FLUIManager.instance.UpdateBatteryUI(batteryCount);
            FlashlightPickupSound();
        }

        void Update()
        {
            if (hasFlashlight)
            {
                PlayerInput();
                DrainBattery();
            }
        }

        void PlayerInput()
        {
            if (Input.GetKeyDown(FLInputManager.instance.flashlightSwitch)) //TURNING FLASHLIGHT ON/OFF
            {
                FlashlightSwitch();
            }

            if (!infiniteFlashlight)
            {
                if (Input.GetKey(FLInputManager.instance.reloadBattery) && batteryCount >= 1)
                {
                    ReplaceBattery();
                }
                else
                {
                    CoolDownTimer();
                }

                if (Input.GetKeyUp(FLInputManager.instance.reloadBattery))
                {
                    shouldUpdate = true;
                }
            }

            if (Input.GetKeyDown(FLInputManager.instance.toggleFlashlightInv))
            {
                ToggleInventory();
            }
        }

        void FlashlightSwitch()
        {
            isFlashlightOn = !isFlashlightOn;

            flashlightSpot.enabled = isFlashlightOn;
            FLUIManager.instance.FlashlightIndicatorColor(isFlashlightOn);
            FlashlightClickSound();
        }

        void ReplaceBattery()
        {
            shouldUpdate = false;
            replaceBatteryTimer -= Time.deltaTime;

            if (showFlashlightInventory)
            {
                ToggleRadialIndicator(true);
                UpdateRadialIndicator(replaceBatteryTimer);
            }

            if (replaceBatteryTimer <= 0)
            {
                batteryCount--;
                flashlightSpot.intensity += maxFlashlightIntensity;

                if (showFlashlightInventory)
                {
                    FLUIManager.instance.UpdateBatteryUI(batteryCount);
                    FLUIManager.instance.MaximumBatteryLevel(maxFlashlightIntensity);
                }
                FlashlightReloadSound();
                replaceBatteryTimer = maxReplaceBatteryTimer;

                if (showFlashlightInventory)
                {
                    UpdateRadialIndicator(maxReplaceBatteryTimer);
                    ToggleRadialIndicator(false);
                }
            }
        }

        void CoolDownTimer()
        {
            if (shouldUpdate)
            {
                replaceBatteryTimer += Time.deltaTime;
                if (showFlashlightInventory)
                {
                    UpdateRadialIndicator(replaceBatteryTimer);
                }

                if (replaceBatteryTimer >= maxReplaceBatteryTimer)
                {
                    replaceBatteryTimer = maxReplaceBatteryTimer;
                    if (showFlashlightInventory)
                    {
                        UpdateRadialIndicator(maxReplaceBatteryTimer);
                        ToggleRadialIndicator(false);
                    }
                    shouldUpdate = false;
                }
            }
        }

        void DrainBattery()
        {
            if (!infiniteFlashlight && isFlashlightOn)
            {
                flashlightSpot.intensity = Mathf.Clamp(flashlightSpot.intensity - batteryDrainAmount * Time.deltaTime * maxFlashlightIntensity, 0, maxFlashlightIntensity);
                if (showFlashlightInventory)
                {
                    FLUIManager.instance.UpdateBatteryLevelUI(batteryDrainAmount * Time.deltaTime);
                }
            }
        }

        void ToggleRadialIndicator(bool on)
        {
            FLUIManager.instance.ToggleRadialIndicator(on);
        }

        void UpdateRadialIndicator(float amount)
        {
            FLUIManager.instance.UpdateRadialIndicatorUI(amount);
        }

        void FlashlightPickupSound()
        {
            FLAudioManager.instance.Play(flashlightPickup);
        }

        void FlashlightClickSound()
        {
            FLAudioManager.instance.Play(flashlightClick);
        }

        void FlashlightReloadSound()
        {
            FLAudioManager.instance.Play(flashlightReload);
        }
    }
}