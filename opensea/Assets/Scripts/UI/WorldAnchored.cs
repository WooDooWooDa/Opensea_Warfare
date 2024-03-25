using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(RotationConstraint))]
public sealed class WorldAnchored : MonoBehaviour
{
    private RotationConstraint m_constraint;

    private void Awake()
    {
        m_constraint = GetComponent<RotationConstraint>();
        m_constraint.AddSource(new ConstraintSource() { 
            sourceTransform = GameObject.FindWithTag("WorldAnchor").transform, 
            weight = 1 
        });
    }
}
