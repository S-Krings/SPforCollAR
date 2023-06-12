using Microsoft.MixedReality.Toolkit.UI.BoundsControl;
using Microsoft.MixedReality.Toolkit.UI.BoundsControlTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrivacyShieldValueSetter : MonoBehaviour
{
    [SerializeField] private BoundsControl boundsControl;

    [SerializeField] private Material boundingBoxMaterial;
    [SerializeField] private Material boundingBoxGrabbedMaterial;
    [SerializeField] private int flattenAxixDisplay = 0;

    [SerializeField] private Material scaleHandleMaterial;
    [SerializeField] private Material scaleHandleGrabbedMaterial;
    [SerializeField] private GameObject scaleHandlePrefab;
    [SerializeField] private float scaleHandleSize = 0.04f;
    [SerializeField] private Vector3 scaleColliderPadding = new Vector3(0.04f, 0.04f, 0.04f);
    [SerializeField] private bool scaleDrawTether = true;
    [SerializeField] private Collider scaleHandlesIgnoreCollider;
    [SerializeField] private GameObject scaleHandleSlatePrefab;
    [SerializeField] private HandleScaleMode scaleBehaviour = HandleScaleMode.NonUniform;

    [SerializeField] private Material rotationHandleMaterial;
    [SerializeField] private Material rotationHandleGrabbedMaterial;
    [SerializeField] private GameObject rotationHandlePrefab;
    [SerializeField] private float rotationHandleSize = 0.04f;
    [SerializeField] private Vector3 rotationColliderPadding = new Vector3(0.04f, 0.04f, 0.04f);
    [SerializeField] private bool rotationDrawTether = true;
    [SerializeField] private Collider rotationHandlesIgnoreCollider;
    [SerializeField] private HandlePrefabCollider rotationHandlePrefabCollider = HandlePrefabCollider.Box;
    [SerializeField] private bool rotationShowHandleX = true;
    [SerializeField] private bool rotationShowHandleY = true;
    [SerializeField] private bool rotationShowHandleZ = true;

    [SerializeField] private Material translationHandleMaterial;
    [SerializeField] private Material translationHandleGrabbedMaterial;
    [SerializeField] private GameObject translationHandlePrefab;
    [SerializeField] private float translationHandleSize = 0.04f;
    [SerializeField] private Vector3 translationColliderPadding = new Vector3(0.04f, 0.04f, 0.04f);
    [SerializeField] private bool translationDrawTether = true;
    [SerializeField] private Collider translationHandlesIgnoreCollider;
    [SerializeField] private HandlePrefabCollider translationHandlePrefabCollider = HandlePrefabCollider.Box;
    [SerializeField] private bool translationShowHandleX = true;
    [SerializeField] private bool translationShowHandleY = true;
    [SerializeField] private bool translationShowHandleZ = true;



    private void Awake()
    {
        BoxDisplayConfiguration boxConf = boundsControl.BoxDisplayConfig;
        boxConf.BoxMaterial = boundingBoxMaterial;
        boxConf.BoxGrabbedMaterial = boundingBoxGrabbedMaterial;
        boxConf.FlattenAxisDisplayScale = flattenAxixDisplay;

        ScaleHandlesConfiguration scaleConfig = boundsControl.ScaleHandlesConfig;
        scaleConfig.HandleMaterial = scaleHandleMaterial;
        scaleConfig.HandleGrabbedMaterial = scaleHandleGrabbedMaterial;
        scaleConfig.HandlePrefab = scaleHandlePrefab;
        scaleConfig.HandleSize = scaleHandleSize;
        scaleConfig.ColliderPadding = scaleColliderPadding;
        scaleConfig.DrawTetherWhenManipulating = scaleDrawTether;
        scaleConfig.HandlesIgnoreCollider = scaleHandlesIgnoreCollider;
        scaleConfig.HandleSlatePrefab = scaleHandleSlatePrefab;
        //scaleConfig.ShowScaleHandles 
        scaleConfig.ScaleBehavior = scaleBehaviour;

        RotationHandlesConfiguration rotationConfig = boundsControl.RotationHandlesConfig;
        rotationConfig.HandleMaterial = rotationHandleMaterial;
        rotationConfig.HandleGrabbedMaterial = rotationHandleGrabbedMaterial;
        rotationConfig.HandlePrefab = rotationHandlePrefab;
        rotationConfig.HandleSize = rotationHandleSize;
        rotationConfig.ColliderPadding = rotationColliderPadding;
        rotationConfig.DrawTetherWhenManipulating = rotationDrawTether;
        rotationConfig.HandlesIgnoreCollider = rotationHandlesIgnoreCollider;
        rotationConfig.HandlePrefabColliderType = rotationHandlePrefabCollider;
        rotationConfig.ShowHandleForX = rotationShowHandleX;
        rotationConfig.ShowHandleForY = rotationShowHandleY;
        rotationConfig.ShowHandleForZ = rotationShowHandleZ;

        TranslationHandlesConfiguration translationConfig = boundsControl.TranslationHandlesConfig;
        translationConfig.HandleMaterial = translationHandleMaterial;
        translationConfig.HandleGrabbedMaterial = translationHandleGrabbedMaterial;
        translationConfig.HandlePrefab = translationHandlePrefab;
        translationConfig.HandleSize = translationHandleSize;
        translationConfig.ColliderPadding = translationColliderPadding;
        translationConfig.DrawTetherWhenManipulating = translationDrawTether;
        translationConfig.HandlesIgnoreCollider = translationHandlesIgnoreCollider;
        translationConfig.HandlePrefabColliderType = translationHandlePrefabCollider;
        translationConfig.ShowHandleForX = translationShowHandleX;
        translationConfig.ShowHandleForY = translationShowHandleY;
        translationConfig.ShowHandleForZ = translationShowHandleZ;
    }


}
