using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class WhiteboardPen : VRTK_InteractableObject
{
    public Whiteboard whiteboard;
    private RaycastHit touch;
    private bool lastTouch;
    private Quaternion lastAngle;
    public Color color;

    void Start()
    {
        //this.whiteboard = whiteboard.GetComponent<Whiteboard>();
    }

    void Update()
    {
        float height = transform.localScale.y;
        Vector3 tip = transform.position;

        if(lastTouch)
        {
            height = 1.1f;
        }

        if(Physics.Raycast(tip, transform.up, out touch, height))
        {
            if (!(touch.collider.tag == "Whiteboard"))
                return;
            this.whiteboard = touch.collider.GetComponent<Whiteboard>();

            this.whiteboard.SetColor(color);
            this.whiteboard.SetTouchPosition(touch.textureCoord.x, touch.textureCoord.y);
            this.whiteboard.ToggleTouch(true);

            if(!lastTouch)
            {
                lastTouch = true;
                lastAngle = transform.rotation;
            }
        } else
        {
            this.whiteboard.ToggleTouch(false);
            lastTouch = false;
        }

        if(lastTouch)
        {
            transform.rotation = lastAngle;
        }
    }
}
