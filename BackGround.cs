using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour {
    private Transform cible;
    void Awake()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        float hauteurCamera = Camera.main.orthographicSize * 2; // hauteur de la camera * 2 car la fonction ne prends que la moitié
        Vector2 cameraSize = new Vector2(Camera.main.aspect * hauteurCamera, hauteurCamera);
        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;

        Vector2 scale = transform.localScale; // augmente la taille de l'arrière plan par rapport si la hauteur de la caméra est supérieure à sa largeur
        if (cameraSize.x >= cameraSize.y)
        { // Landscape (or equal)
            scale *= cameraSize.x / spriteSize.x;
        }
        else
        { // Portrait
            scale *= cameraSize.y / spriteSize.y;
        }

        
    }
}
