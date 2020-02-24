using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PancakeScript : MonoBehaviour
{
    bool isSticky = false;
    float stickVelocity = 10f;

    public Image topSide;
    public Image bottomSide;

    float topCooked = 0;
    float bottomCooked = 0;

    float valTop =-1f;
    float valBottom=-1f;

    bool upRight;
    float threshold = 0.6f;

    public Color uncooked;
    public Color cooked;
    public Color burnt;

    Rigidbody rb;
    GameObject hob;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        hob = GameObject.FindGameObjectWithTag("Hob");
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.magnitude > stickVelocity) { isSticky = true; }
        if (IsUpright()) { upRight = true; } else { upRight = false; }

        if (topCooked != valTop || bottomCooked != valBottom)
        {
            if (topCooked < 20) { topSide.color = uncooked; }
            else if (topCooked > 20 && topCooked < 60) { topSide.color = cooked; }
            else if (topCooked > 60) { topSide.color = burnt; }
            valTop = topCooked;

            if (bottomCooked < 20) { bottomSide.color = uncooked; }
            else if (bottomCooked > 20 && bottomCooked < 60) { bottomSide.color = cooked; }
            else if (bottomCooked > 60) { bottomSide.color = burnt; }
            valBottom = bottomCooked;
        }

        if (Vector3.Distance(transform.position, hob.transform.position) < 0.5f)
        {
            CookSide();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isSticky)
        {
            transform.SetParent(collision.transform);
        }
    }
    public bool IsUpright()
    {
        return transform.up.y > threshold;
    }

    public void CookSide()
    {
        if (upRight)
        {
            topCooked += Time.deltaTime;
        }
        else
        {
            bottomCooked += Time.deltaTime;
        }
    }
}
