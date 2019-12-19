using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{
    private Camera mainCamera;
    // Use this for initialization
    void Awake()
    {
        mainCamera = Camera.main;
    }
    /// <summary>
    /// 相机拉近
    /// </summary>
    public void ZoomIn()
    {
        mainCamera.DOOrthoSize(12.88f, 0.5f);
    }
    /// <summary>
    /// 相机拉远
    /// </summary>
    public void ZoomOut()
    {
        mainCamera.DOOrthoSize(17.26f,0.5f);
    }
}