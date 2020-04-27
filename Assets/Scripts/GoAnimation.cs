
using UnityEngine;

public class GoAnimation : MonoBehaviour {

    public static GoAnimation Instance;
    private Animator anim;
	void Start () {
        Instance = this;
        anim = GetComponent<Animator>();
	}
	
    public void StartAnimation()
    {
        anim.SetTrigger("start");
    }
    public void onAnimationComplete()
    {
        gameObject.SetActive(false);
    }
}
