using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidAgent : MonoBehaviour
{
    [SerializeField] protected FluidPlane fluidPlane;
    protected Texture DisplacementTexture;
    protected Vector2 lastPosition;
    private void Update()
    {
        Vector2 newPosition = new Vector2(transform.position.x, transform.position.z);
    }
}
