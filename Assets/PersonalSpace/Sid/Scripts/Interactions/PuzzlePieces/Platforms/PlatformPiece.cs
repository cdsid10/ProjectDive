using System.Collections.Generic;
using PersonalSpace.Sid.Scripts.Managers;
using UnityEngine;
using UnityEngine.Serialization;

namespace PersonalSpace.Sid.Scripts.Interactions.PuzzlePieces.Platforms
{
    public class PlatformPiece : BasePuzzlePiece
    {
        [field: SerializeField] public override bool IsPuzzlePieceEnabled { get; set; }
        [SerializeField] private bool shouldGetComponentInChildren;
        [SerializeField] private bool hasMultipleMeshRenderers;

        [SerializeField] private List<Light> lightList = new List<Light>();
        [SerializeField] private Color32 red = new Color32();
        [SerializeField] private Color32 green = new Color32();

        [SerializeField] private AudioClip appear;
        [SerializeField] private AudioClip disappear;
        

        private Collider platformCollider;
        private MeshRenderer meshRenderer;
        private MeshRenderer[] meshRendererArray;

        private void Awake()
        {
            if (shouldGetComponentInChildren)
            {
                platformCollider = gameObject.GetComponentInChildren<Collider>();
                
                if (hasMultipleMeshRenderers)
                {
                    meshRendererArray = gameObject.GetComponentsInChildren<MeshRenderer>();
                }
                else
                {
                    meshRenderer = gameObject.GetComponentInChildren<MeshRenderer>();
                }
            }
            else
            {
                platformCollider = gameObject.GetComponent<Collider>();
                
                if (hasMultipleMeshRenderers)
                {
                    meshRendererArray = gameObject.GetComponentsInChildren<MeshRenderer>();
                }
                else
                {
                    meshRenderer = gameObject.GetComponentInChildren<MeshRenderer>();
                }
            }
        }

        private void Start()
        {
            if (IsPuzzlePieceEnabled)
            {
                platformCollider.enabled = true;
                
                if (hasMultipleMeshRenderers)
                {
                    foreach (var renderer1 in meshRendererArray)
                    {
                        renderer1.enabled = true;
                    }
                }
                else
                {
                    meshRenderer.enabled = true;

                }
                
                foreach (var platformLight in lightList)
                {
                    platformLight.color = green;
                }
            }
            else
            {
                platformCollider.enabled = false;
                if (hasMultipleMeshRenderers)
                {
                    foreach (var renderer1 in meshRendererArray)
                    {
                        renderer1.enabled = false;
                    }
                }
                else
                {
                    meshRenderer.enabled = false;

                }
                
                foreach (var platformLight in lightList)
                {
                    platformLight.color = red;
                }
            }
        }

        public override void PerformAction()
        {
            if (IsPuzzlePieceEnabled)
            {
                SoundManager.Instance.PlaySound(disappear);
                platformCollider.enabled = false;
                
                if (hasMultipleMeshRenderers)
                {
                    foreach (var renderer1 in meshRendererArray)
                    {
                        renderer1.enabled = false;
                    }
                }
                else
                {
                    meshRenderer.enabled = false;

                }

                foreach (var platformLight in lightList)
                {
                    platformLight.color = red;
                }
            }
            else
            {
                SoundManager.Instance.PlaySound(appear);
                platformCollider.enabled = true;
                
                if (hasMultipleMeshRenderers)
                {
                    foreach (var renderer1 in meshRendererArray)
                    {
                        renderer1.enabled = true;
                    }
                }
                else
                {
                    meshRenderer.enabled = true;

                }
                
                foreach (var platformLight in lightList)
                {
                    platformLight.color = green;
                }
            }
            
            IsPuzzlePieceEnabled = !IsPuzzlePieceEnabled;

        }
    }
}