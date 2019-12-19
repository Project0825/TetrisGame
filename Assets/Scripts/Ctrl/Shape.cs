using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour {

    private bool isPause = false;
    private Transform pivot;
    private float timer = 0;

    private float stepTime = 0.8f;
    private int multiple = 18;
    private bool IsSpeedUp = false;
    private Ctrl ctrl;
    private GameManager gameManager;
    private void Awake()
    {
        pivot = transform.Find("Pivot");
    }
    private void Update()
    {
        if (isPause) { return; }
        timer += Time.deltaTime;
        if (timer > stepTime)
        {
            timer = 0;
            Fall();
        }
        inputControl();

    }
    public void Init(Color color,Ctrl ctrl,GameManager gameManager)
    {
        this.ctrl = ctrl;
        this.gameManager = gameManager;
        foreach (Transform t in transform)
        {
            if(t.tag == "Block")
            {
                t.GetComponent<SpriteRenderer>().color = color;
            }
        }
    }

    private void Fall()
    {
        Vector3 pos = transform.position;
        pos.y -= 1;
        transform.position = pos; 
        if(ctrl.model.IsVaildMapPosition(this.transform) == false)
        {
            pos.y += 1;
            transform.position = pos;
            isPause = true;
            bool isClear = ctrl.model.PlaceShape(this.transform);
            if (isClear) ctrl.audioManager.PlayAudioClear();
            gameManager.FallDown();
            return;
        }
        ctrl.audioManager.PlayDrop();
    }
    private void inputControl()
    {
        float h = 0;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            h = -1;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            h = 1;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.RotateAround(pivot.position, Vector3.forward, -90);
            if (ctrl.model.IsVaildMapPosition(this.transform) == false)
            {
                transform.RotateAround(pivot.position, Vector3.forward, 90);
            }
            else
            {
                ctrl.audioManager.PlayMove();
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            stepTime /= multiple;
        }
        if (h != 0)
        {
            Vector3 pos = transform.position;
            pos.x += h;
            transform.position = pos;
            if (ctrl.model.IsVaildMapPosition(this.transform) == false)
            {
                pos.x -= h;
                transform.position = pos;return;
            }
            ctrl.audioManager.PlayMove();
        }
        
    }
    public void Pause()
    {
        isPause = true;
    }
    public void Resume()
    {
        isPause = false;
    }
}
