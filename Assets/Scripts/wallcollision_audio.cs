﻿using UnityEngine;
using System.Collections;

public class wallcollision_audio : MonoBehaviour {

    public AudioClip clip;
    private AudioSource source;

    private void Start()
    {
		source = GetComponent<AudioSource> ();
        source.clip = clip;
        source.loop = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject go = collision.collider.gameObject;
        if (go.CompareTag("Player"))
        {
            Rigidbody body = go.GetComponent<Rigidbody>();
            float mag = Mathf.Min(Mathf.Abs(body.velocity.x) + Mathf.Abs(body.velocity.y), 3);
            float vol = mag / 3;
            source.volume = Mathf.Min(vol, 1);
            source.PlayOneShot(clip, (float)1.0);
        }
    }
}
