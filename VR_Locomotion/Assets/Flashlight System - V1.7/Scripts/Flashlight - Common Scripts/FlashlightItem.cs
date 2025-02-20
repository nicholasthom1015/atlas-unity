using UnityEngine;

namespace FlashlightSystem
{
    public class FlashlightItem : MonoBehaviour
    {
        [Space(10)] [SerializeField] private ObjectType _objectType = ObjectType.None;
        private enum ObjectType { None, Flashlight, Battery }

        [SerializeField] private int batteryAmount = 1;

        public void ObjectInteract()
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
