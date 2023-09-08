using UnityEngine;
using UnityEngine.InputSystem;

public class RaycastInteractor : MonoBehaviour
{
    [SerializeField] private InputAction fingerTap;

    private void OnEnable()
    {
        fingerTap.performed += OnFingerTapPerformed;
        fingerTap.Enable();
    }

    private void OnFingerTapPerformed(InputAction.CallbackContext ctx)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            // GameObject detected in front of the camera.
            if (hit.transform.gameObject)
            {
                hit.transform.GetComponent<Interactable>().Select();
            }
        }
    }

    
}