using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;


// Simple draggable interaction, while the mouse button is pressed, updates the root position of this object with the one obtained by raycasting against the floor
public class MovableElement : InteractableElement
{
    [SerializeField] Collider m_collider;
    [SerializeField] private List<Collider> m_ignoreColliders;
    Vector3 m_oldInteractablePosition;
    Quaternion m_oldInteractableRotation;

    private void Start()
    {
        m_collider = GetComponent<Collider>();
    }

    public override void OnInteractableMouseDown(PlayerController controller)
    {
        m_oldInteractablePosition = m_root.transform.position;
        m_oldInteractableRotation = m_root.transform.rotation;
    }

    public override void OnInteractableMouse(PlayerController controller)
    {
        RaycastHit hitFloor;

        if (Physics.Raycast(controller.mouseRay, out hitFloor, float.MaxValue, GlobalOptions.main.FloorLayer))
        {
            if (CheckForOverlap(hitFloor.point))
                return;
            m_root.transform.position = hitFloor.point;
            m_root.GetComponent<PostController>()?.UpdateStockUI();
        }
    }

    public override void OnInteractableMouseUp(PlayerController controller)
    {
        // do nothing
    }
    
    public override void OnInteractableMouseScroll(PlayerController controller, float scrollDelta)
    {
        m_root.transform.Rotate(Vector3.up * scrollDelta);
    }

    public bool CheckForOverlap(Vector3 newPosition)
    {
        Vector3 newPositionOffset = newPosition - m_root.transform.position;
        Collider[] colliders = Physics.OverlapBox(
            m_collider.bounds.center + newPositionOffset,
            m_collider.bounds.extents,
            m_root.transform.rotation,
            GlobalOptions.main.ObstacleLayer
        );
        foreach (var collider in colliders)
        {
            if (m_ignoreColliders.Contains(collider))
                continue;
            return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (m_collider != null)
            Gizmos.DrawWireCube(m_collider.bounds.center, m_collider.bounds.extents * 2);
    }
}
