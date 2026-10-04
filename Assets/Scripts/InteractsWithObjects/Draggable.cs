using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Draggable : MonoBehaviour
{
    
    private const string TagOfDraggableItem = "Draggable";
    private Rigidbody _rigidBody;
    private const int DefaultLayerValue = 0;
    private const int DraggableLayerValue = 6;
    public string textOfDescription;

    void Start()
    {
        if(GetComponent<Assembled>()){
            if(!GetComponent<Assembled>().isAssembled){
                transform.SetParent(null);
                this.gameObject.tag = TagOfDraggableItem;
            }
        }
        else{
            transform.SetParent(null);
            this.gameObject.tag = TagOfDraggableItem;
        }
        _rigidBody = GetComponent<Rigidbody>();
    }

    public void PrepareForDrag()
    {
        this.gameObject.layer = DraggableLayerValue;
        _rigidBody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
    }
    public void PrepareForDrop()
    {
        this.gameObject.layer = DefaultLayerValue;
        _rigidBody.collisionDetectionMode = CollisionDetectionMode.Discrete;
    }
    public void DropWithForce(Vector3 dropDirection, float forceDrop) 
    {
        this.gameObject.layer = DefaultLayerValue;
        _rigidBody.collisionDetectionMode = CollisionDetectionMode.Discrete;
        _rigidBody.AddForce(dropDirection * forceDrop, ForceMode.Impulse);
    }
}
