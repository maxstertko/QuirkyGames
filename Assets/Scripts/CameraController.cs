﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CameraController{
    public float ySpeed;
    public float xSpeed;
    public float distance;
    public bool invertedX = true;
    public bool invertedY = false;
    public bool lockCursor = true;
    public float MinimumY;
    public float MaximumY;

    //private Vector3 mCharacterPosition;
    //private Vector3 mCameraPosition;
    //private Vector2 mUnitPosition;
    //private float yOffset;
    private float yPos;
    private Transform mCamera;
    private bool mCursorLocked = true;

    public void Init(Transform character, Transform camera)
    {
        Vector3 unit3D = camera.position.normalized;
        //mCameraPosition = camera.position;
        //mCharacterPosition = character.position;
        //mUnitPosition = new Vector2(unit3D.x, unit3D.z);
        //distance = 10f;//camera.position.magnitude;
        //yOffset = camera.GetChild(0).position.y;
        yPos = camera.GetChild(0).localPosition.y;
        mCamera = camera;
    }

    public void UpdatePosition()
    {
        float horizontal = Input.GetAxisRaw("Mouse X") * xSpeed;
        float vertical = Input.GetAxisRaw("Mouse Y") * ySpeed;
        Vector3 tilt = mCamera.GetChild(0).localPosition;

        horizontal *= invertedX ? -1f : 1f;
        vertical *= invertedY ? -1f : 1f;
        yPos += vertical * Time.deltaTime;
        yPos = Mathf.Clamp(yPos, MinimumY, MaximumY);

        tilt.y = yPos;

        mCamera.Rotate(Vector3.up, horizontal * Time.deltaTime);
        mCamera.GetChild(0).localPosition = tilt;
        /*
        Vector2 tangent;

        mUnitPosition = new Vector2(Mathf.Cos(horizontal), Mathf.Sin(horizontal));
        tangent = FindTangent(mUnitPosition);
        if(horizontal != 0f)
        {
            mCamera.position += new Vector3(tangent.x, 0f, tangent.y);
            //Debug.Log(tangent);
        }
        */
    }

    public void SetCursorLock(bool value)
    {
        lockCursor = value;
        if (!lockCursor)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void UpdateCursorLock()
    {
        if (lockCursor)
            InternalLockUpdate();
    }

    private void InternalLockUpdate()
    {
        if (Input.GetKeyUp(KeyCode.L))
            mCursorLocked = false;
        else if (Input.GetMouseButtonUp(0))
            mCursorLocked = true;

        if (mCursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
