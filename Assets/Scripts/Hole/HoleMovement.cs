using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Hole
{
    public class HoleMovement : MonoBehaviour
    {
        [Header("Hole Mesh")] 
   
        [SerializeField] private MeshFilter meshFilter;
        [SerializeField] private MeshCollider meshCollider;

        [Header("Hole Vertices Radius")] 
        
        [SerializeField] private Vector2 moveLimits;
        [SerializeField] private float radius;
        [SerializeField] private Transform holeCenter;
        [SerializeField] private Transform rotatingCircle;
        
        [Space]
        [SerializeField] private float moveSpeed;
   
        private Mesh mesh;
        private List<int> holeVertices;
        private List<Vector3> offsets;
        private int holeVerticesCount;
        private float x, y;
        private Vector3 touch, targetPosition;
        private Vector3 firstPos;
        private bool firstTouch;
        private Vector3 secondPos;

        private void Start()
        {
            CircleRotating();
            GameController.IsGameMoving = false;
            GameController.IsGameOver = false;
            
            holeVertices = new List<int>();

            offsets = new List<Vector3>();

            mesh = meshFilter.mesh;
            
            //Finding the hole vertices on the mesh
            FindHoleVertices();
        }

        private void Update()
        {
            if (GameController.IsGameOver) return;
           
            Movement();
        }

        private void CircleRotating()
        {
            rotatingCircle.DORotate(new Vector3(90f, 0f, -90f), .2f).SetEase(Ease.Linear).From(new Vector3(90f, 0f, 0f))
                .SetLoops(-1, LoopType.Incremental);
        }
        
            
        private void UpdateHoleVerticesPosition()
        {
            Vector3[] vertices = mesh.vertices;

            for (int i = 0; i < holeVerticesCount; i++)
            {
                vertices[holeVertices[i]] = holeCenter.position + offsets[i];
            }
            
            //Update mesh
            mesh.vertices = vertices;

            meshFilter.mesh = mesh;

            meshCollider.sharedMesh = mesh;
        }
        
        private void FindHoleVertices ()
        {
            for (int i = 0; i < mesh.vertices.Length; i++)
            {
                //Calculate distance between holeCenter & each Vertex
                float distance = Vector3.Distance (holeCenter.position, mesh.vertices [i]);

                if (distance < radius) {
                    //this vertex belongs to the Hole
                    holeVertices.Add (i);
                    //offset: how far the Vertex from the HoleCenter
                    offsets.Add (mesh.vertices [i] - holeCenter.position);
                }
            }
            //save hole vertices count
            holeVerticesCount = holeVertices.Count;
        }

        public void Movement()
        {
            if (Input.GetMouseButton(0))
            {
                if (!firstTouch)
                {
                    firstPos = Input.mousePosition;
                    secondPos = firstPos;
                    firstTouch = true;
                }
                else if (firstTouch)
                {
                    secondPos = Input.mousePosition;
                    Vector3 deltaPos = secondPos - firstPos;
                    firstPos = secondPos;
                        
                    Vector3 pos = transform.position;

                    float sW = Screen.width;
                    float sH = Screen.height;
                    pos.z += (deltaPos.y / sH) * 500 * Time.deltaTime * moveSpeed;
                    pos.x += (deltaPos.x / sW) * 500 * Time.deltaTime * moveSpeed;


                    pos = new Vector3(Mathf.Clamp(pos.x, -moveLimits.x, moveLimits.x), transform.position.y, 
                        Mathf.Clamp(pos.z, -moveLimits.y, moveLimits.y));

                    transform.position = pos;
                    
                    UpdateHoleVerticesPosition();

                }
            }
            else
            {
                firstTouch = false;
                firstPos = Vector3.zero;
                secondPos = Vector3.zero;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(holeCenter.position, radius);
        }
    }
}

