using System;
using System.Collections;
using System.Collections.Generic;
using DigitalRuby.Tween;
using UnityEngine;

public class SquareController : MonoBehaviour
{
    public Vector3 StartSize;
    public Vector3 MaxSize;
    public float RotationEnd;
    public float IncreaseSizeTime;
    public float RotateSpeed;
    
    public GameObject Particle;
    public GameObject SquareMesh;
    public GameObject Square;
    public AudioSource Audio;
    public SpriteRenderer SquareRenderer;

    private string guid;
    
    private void Start()
    {
        guid = System.Guid.NewGuid().ToString();
        
        TweenScale();
        TweenRotate();
        TweenColor();
    }

    private void OnMouseDown()
    {
        if(!SquareMesh.activeSelf)
            return;
        
        Instantiate(Particle, transform.position, Quaternion.identity);
        Audio.Play();
        SquareMesh.SetActive(false);
        CameraShake.Instance.Shake(0.5f);

        Destroy(gameObject, 1f);
    }
    
    private void TweenScale()
    {
        if(SquareMesh == null)
            return;
        
        System.Action<ITween<Vector3>> updateSqureSize = (t) =>
        {
            if(SquareMesh == null)
                return;
            
            Square.gameObject.transform.localScale = t.CurrentValue;
        };

        System.Action<ITween<Vector3>> squareCompleted = (t) =>
        {
            Debug.Log("Completed");
        };

        Vector3 startPos = Vector3.one;

        // completion defaults to null if not passed in
        Square.gameObject.Tween("ScaleSquare" + guid, StartSize, MaxSize, IncreaseSizeTime, TweenScaleFunctions.CubicEaseIn, updateSqureSize);
    }
    
    
    private void TweenRotate()
    {
        if(SquareMesh == null)
            return;
        
        System.Action<ITween<float>> updateSqureSize = (t) =>
        {
            if(SquareMesh == null)
                return;
            
            SquareMesh.gameObject.transform.rotation = Quaternion.Euler(0, 0, t.CurrentValue);
        };

        System.Action<ITween<Vector3>> squareCompleted = (t) =>
        {
            Debug.Log("Completed");
        };

        float startPos = 0f;

        // completion defaults to null if not passed in
        SquareMesh.gameObject.Tween("RotateSquareMesh" + guid, startPos, RotationEnd, RotateSpeed, TweenScaleFunctions.CubicEaseIn, updateSqureSize);
    }
    
    private void TweenColor()
    {
        if(SquareMesh == null)
            return;

        System.Action<ITween<Color>> updateColor = (t) =>
        {
            if(SquareMesh == null)
                return;

            SquareRenderer.color = t.CurrentValue;
        };

        Color startColor = UnityEngine.Random.ColorHSV(0.0f, 1.0f, 0.0f, 1.0f, 0.5f, 1.0f, 1.0f, 1.0f);
        Color endColor = UnityEngine.Random.ColorHSV(0.0f, 1.0f, 0.0f, 1.0f, 0.5f, 1.0f, 1.0f, 1.0f);

        // completion defaults to null if not passed in
        SquareRenderer.gameObject.Tween("ColorSquare" + guid, endColor, endColor, 1.0f, TweenScaleFunctions.QuadraticEaseOut, updateColor);
    }
}
