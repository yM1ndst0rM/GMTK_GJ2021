using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateMask : MonoBehaviour
{

	private SpriteMask playerMask;
	private SpriteRenderer playerBodyRenderer;

    // Start is called before the first frame update
    void Start()
    {
		playerMask = GetComponent<SpriteMask>();
		playerBodyRenderer = gameObject.GetComponentInParent(typeof(SpriteRenderer)) as SpriteRenderer;
    }

    // Update is called once per frame
    void Update()
    {
		playerMask.sprite = playerBodyRenderer.sprite;
    }
}
