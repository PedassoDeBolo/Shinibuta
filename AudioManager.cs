using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public Sound[] musiques;
	// Use this for initialization
	void Awake () {
		foreach (Sound s in musiques) // pour chaque son on repère la musique, le volume, et la vitesse.
                {
               s.source = gameObject.AddComponent<AudioSource>();
               s.source.clip = s.clip;
               s.source.volume = s.volume;
               s.source.pitch = s.pitch;
                }
	}
	
	// Update is called once per frame
	public void Jouer(string nom) // Fonction pour jouer une musique especifique ayant du parametre "nom" de la musique
    {
        Sound s = Array.Find(musiques, sound => sound.nom == nom);
        s.source.Play();
        
        
            
    }
    
    public void Arreter(string nom) // Arrete la musique
    {
        Sound s = Array.Find(musiques, musique => musique.nom == nom);
        s.source.Stop();
    }
}
