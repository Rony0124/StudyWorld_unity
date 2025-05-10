using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Wire
{
    public class WireHandler : MonoBehaviour
    {
        [SerializeField]
        private LineRenderer line;
        [SerializeField]
        private List<GameObject> points = new ();
        [SerializeField]
        private int numPoints = 10;
        [SerializeField]
        private float spacing = 0.5f;

        [SerializeField] 
        private GameObject targetObj;

        private const int Division = 100;
        
        
        void Start()
        {
           Shoot();
        }
        
        public void Shoot()
        {
            targetObj.transform.DOMove(Vector3.forward * 3f, 0.3f).SetEase(Ease.OutExpo);
            targetObj.transform.DOShakePosition(0.1f, 5f, 10, 0,
                true, randomnessMode: ShakeRandomnessMode.Full);
        } 
        
        void LateUpdate()
        {
            List<Vector3> controlPoints = new List<Vector3>();
            
            controlPoints.Add(points[0].transform.position 
                              + (points[0].transform.position - points[1].transform.position));
            
            for (int i = 0; i < points.Count; i++)
            {
                controlPoints.Add(points[i].transform.position);
            }
            
            controlPoints.Add(points[points.Count - 1].transform.position 
                              + (points[points.Count - 1].transform.position 
                                 - points[points.Count - 2].transform.position));
            
            List<Vector3> linePoints = new List<Vector3>();

            for (int i = 0; i < controlPoints.Count - 3; i++)
            {
                for (float j = 1; j <= Division; j++)
                {
                    float t = j / Division;
                    Vector3 pos = MathUtil.CatmullRomSplineInterp(
                        controlPoints[i],
                        controlPoints[i + 1],
                        controlPoints[i + 2],
                        controlPoints[i + 3],
                        t
                    );

                    linePoints.Add(pos);
                }
            }
            
            line.positionCount = linePoints.Count;
            line.SetPositions(linePoints.ToArray());
        }
    }
}
