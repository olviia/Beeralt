using System;
using UnityEngine;

namespace Helpers
{
    public class ChangingLineRenderer:MonoBehaviour
    {
        //put it on the game object where is Line Rendered, 
        //it has to be one of two components between which the line is drawn

        public GameObject from;
        public GameObject to;
        private LineRenderer lineRenderer;

        private void Start()
        {
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.useWorldSpace = true;
        }

        private void Update()
        {
            lineRenderer.SetPosition(0, from.transform.position);
            lineRenderer.SetPosition(1, to.transform.position);
        }
    }
}