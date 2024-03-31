using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    public Transform target;
    public float lineWidth = 0.01f;

    public TextMeshProUGUI displayText;

    private Transform player;
    private Renderer playerRenderer;
    private LineRenderer line;


    private Material materialDefault;
    public Material materialNew;


    private void Start()
    {
        player = GetComponent<Transform>();
        playerRenderer = GetComponent<Renderer>();
        materialDefault = playerRenderer.material;

        line = gameObject.AddComponent(typeof(LineRenderer)) as LineRenderer;
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
        line.enabled = false;
    }

    private void SetDisplayText(float distance, bool visible = false)
    {
        if (displayText == null)
        {
            return;
        }

        string message = "Distance: " + distance.ToString("0.00") + " m";
        displayText.text = message;
        if (visible)
        {
            displayText.color = Color.green;
        }
        else
        {
            displayText.color = Color.white;
        }
    }

    private void SetBallDefault()
    {
        playerRenderer.material = materialDefault;
    }

    private void SetBallNew()
    {
        playerRenderer.material = materialNew;
    }

    private void Update()
    {

        float distance = Vector3.Distance(target.position, player.position);
        float maxDistance = distance + 10.0f;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, (target.position - transform.position).normalized, out hit, maxDistance))
        {
            if (hit.collider.gameObject == target.gameObject)
            {
                line.enabled = true;
                SetBallNew();
                SetDisplayText(distance, true);
            }
            else
            {
                line.enabled = false;
                SetBallDefault();
                SetDisplayText(distance, false);
            }
        }
        else
        {
            line.enabled = false;
            SetBallDefault();
            SetDisplayText(distance, false);
        }

        line.SetPosition(0, transform.position);
        if (hit.collider != null)
        {
            line.SetPosition(1, hit.point);
        }
        else
        {
            line.SetPosition(1, transform.position + (target.position - transform.position).normalized * maxDistance);
        }
    }
}

