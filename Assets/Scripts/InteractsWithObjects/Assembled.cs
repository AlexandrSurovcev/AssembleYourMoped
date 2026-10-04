using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assembled : MonoBehaviour
{
    private const string Tag = "Draggable";
    [SerializeField] public Vector3 _position;
    [SerializeField] public GameObject assemblingObject;
    public GameObject[] bolts;
    public GameObject[] wire;
    public GameObject Trigger;
    public GameObject haveAssembled;
    public GameObject[] additionalObjects;

    public bool isAssembled;
    public bool needSpinBolts = true;
    public bool needCollider = false;
    public Transform colliderObject;

    void Update()
    {

        if (needSpinBolts)
        {
            CheckSpinedBolts();
        }

        if (wire != null && wire.Length > 0)
        {
            foreach (var wirePiece in wire)
            {
                if (wirePiece != null && wirePiece.TryGetComponent(out MeshRenderer meshRenderer))
                {
                    meshRenderer.enabled = isAssembled;
                }
            }
        }

        if (Trigger != null && Trigger.TryGetComponent(out BoxCollider boxCollider))
        {
            boxCollider.enabled = isAssembled;
        }
    }

    private void CheckSpinedBolts()
    {
        foreach (var bolt in bolts)
        {
            if (bolt != null && bolt.TryGetComponent(out Bolts boltComponent))
            {
                if (boltComponent.isAssembled)
                {
                    isAssembled = true;
                    return;
                }
            }
        }
        isAssembled = false;
    }
}
