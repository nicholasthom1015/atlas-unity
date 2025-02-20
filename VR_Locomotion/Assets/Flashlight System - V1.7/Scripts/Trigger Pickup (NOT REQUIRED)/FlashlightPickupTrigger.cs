using UnityEngine;

namespace FlashlightSystem
{
    public class FlashlightPickupTrigger : MonoBehaviour
    {
        [Space(10)] [SerializeField] private ObjectType _objectType = ObjectType.None;

        private enum ObjectType { None, Flashlight, Battery }

        [Header("Battery Number")]
        [SerializeField] private int batteryAmount = 1;

        private const string playerTag = "Player";

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(playerTag))
            {
                PickupFlashlightItem();
            }
        }

        private void PickupFlashlightItem()
        {
            switch (_objectType)
            {
                case ObjectType.Flashlight:
                    FlashlightController.instance.CollectFlashlight();
                    break;
                case ObjectType.Battery:
                    FlashlightController.instance.CollectBattery(batteryAmount);
                    break;
            }
            gameObject.SetActive(false);
        }
    }
}
