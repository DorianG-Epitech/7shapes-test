using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Mouse control
// Passes the mouse activity when interacting with InteractableElements
public class PlayerController : MonoBehaviour
{
    [Header("Links")]
    [SerializeField] Camera m_camera;

    [Header("Selections")]
    [SerializeField] WorkerController m_activeWorker;
    [Header("Status")]
    [SerializeField] InteractableElement m_currentInteractable = null;
    [SerializeField] Vector3 m_oldInteractablePosition;
    [SerializeField] Quaternion m_oldInteractableRotation;
    [SerializeField] private Ray m_mouseRay;

    public Ray mouseRay { get => m_mouseRay; private set => m_mouseRay = value; }
    public WorkerController activeWorker { get => m_activeWorker; private set => m_activeWorker = value; }

    void OnValidate()
    {
        if (!m_camera) m_camera = Camera.main;
    }

    void Update()
    {

        mouseRay = m_camera.ScreenPointToRay(Input.mousePosition);
        // pick, drag, and drop movable object
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInteractable;
            if (Physics.Raycast(mouseRay, out hitInteractable, float.MaxValue, GlobalOptions.main.InteractableLayer))
            {
                if (hitInteractable.collider.TryGetComponent<InteractableElement>(out m_currentInteractable))
                {
                    m_oldInteractablePosition = m_currentInteractable.root.transform.position;
                    m_oldInteractableRotation = m_currentInteractable.root.transform.rotation;
                    m_currentInteractable.OnInteractableMouseDown(this);

                    // this is not very elegant, but the alternative implies a larger codebase
                    WorkerController newWorker;
                    if(m_currentInteractable.root.TryGetComponent<WorkerController>(out newWorker))
                    {
                        activeWorker = newWorker;
                    }
                }
                else
                {
                    Debug.LogWarning("The object " + hitInteractable.collider.name + " does not have an Interactable script.");
                }
            }
        }
        else if (Input.GetMouseButton(0) && m_currentInteractable)
        {
            m_currentInteractable.OnInteractableMouse(this);
            
            if (Input.mouseScrollDelta.y != 0f)
                m_currentInteractable.OnInteractableMouseScroll(this, Input.mouseScrollDelta.y);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (m_currentInteractable)
            {
                m_currentInteractable.OnInteractableMouseUp(this);
                EventHistory.instance.AddRelocationEvent(m_currentInteractable.root, m_oldInteractablePosition, m_oldInteractableRotation);
                m_currentInteractable = null;

                if (m_activeWorker.Started)
                {
                    m_activeWorker.RecomputePath();
                    m_activeWorker.GoToTarget();
                }
            }
            
        }
    }
}
