using UnityEngine;

namespace FlashlightSystem
{
    [RequireComponent(typeof(Camera))]
    public class FlashlightInteractor : MonoBehaviour
    {
        [Header("Raycast Features")]
        [SerializeField] private float rayDistance = 5;

        private FlashlightItem interactiveItem;
        private Camera _camera;

        void Start()
        {
            _camera = GetComponent<Camera>();
        }

        void Update()
        {
            if (Physics.Raycast(_camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f)), transform.forward, out RaycastHit hit, rayDistance))
            {
                var flashlightItem = hit.collider.GetComponent<FlashlightItem>();
                if (flashlightItem != null)
                {
                    interactiveItem = flashlightItem;
                    HighlightCrosshair(true);
                }
                else
                {
                    ClearExaminable();
                }
            }
            else
            {
                ClearExaminable();
            }

            if (interactiveItem != null)
            {
                if (Input.GetKeyDown(FLInputManager.instance.pickupKey))
                {
                    interactiveItem.ObjectInteract();
                }
            }
        }

        private void ClearExaminable()
        {
            if (interactiveItem != null)
            {
                HighlightCrosshair(false);
                interactiveItem = null;
            }
        }

        void HighlightCrosshair(bool on)
        {
            FLUIManager.instance.HighlightCrosshair(on);
        }
    }
}
