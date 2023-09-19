using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shapes : MonoBehaviour
{
    private SpriteRenderer _sprite;
    public float opacity { get; set; }
    private Color _col;

	// Use this for initialization
	void Awake ()
	{
	    _sprite = GetComponent<SpriteRenderer>();
	    _col = _sprite.color;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    _col.a = opacity;
		_sprite.color = _col;
	}

    public void Show()
    {
        Go.to(this, 20f, new GoTweenConfig()
            .floatProp("opacity", 0.15f)
            );
    }

    public void Hide()
    {
        Go.to(this, 3f, new GoTweenConfig()
            .floatProp("opacity", 0f)
            .onComplete(HideDone)
            );
    }

    public void HideDone(AbstractGoTween t)
    {
        t.destroy();
        Destroy(transform.parent.gameObject);
    }
}
