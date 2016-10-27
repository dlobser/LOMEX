using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class testLine : MonoBehaviour {

	public Transform parent;
	public GameObject[] pathTracer;
	float[] rands;
	public int[] pathTracerRandomAmount;
	public int amount = 10;
	private GameObject[] tracers;
	public int detail = 100;
	public float speed = 1;
	private float count = 0;
	private float rCount = 0;

	private bool closed = true;
	public bool linear = false;
    public float micro_changethis = 0f;

    //debug


	private List<Transform> controlPoints = new List<Transform> ();
	private Spline spline;
	private GameObject target;
	private GameObject rTarget;
	

    void Awake()
    {

        spline = ScriptableObject.CreateInstance<Spline>();
        for (int i = 0; i < parent.childCount; i++)
        {
            //			print (i);
            controlPoints.Add(parent.GetChild(i));
        }
        //		print (controlPoints.Count);
        spline.init(controlPoints, detail, micro_changethis);

    }
	void Start(){

//		speed = PlayerPrefs.GetFloat ("speedMin")+(PlayerPrefs.GetFloat ("speedMax")-PlayerPrefs.GetFloat ("speedMin"));
//        if (speed == 0)
//            speed = 0.2f;
//		speed *= .07f;
		tracers = new GameObject[amount];
		rands = new float[amount];


		List<int> odds = new List<int>();

		for (int i = 0; i < pathTracerRandomAmount.Length; i++) {
			for (int j = 0; j < pathTracerRandomAmount[i]; j++) {
				odds.Add(i);
			}
		}
		for(int i = 0 ; i < amount ; i++){
			int which = (int)Mathf.Floor (Random.Range (0, odds.Count));
//			Debug.Log (which);
			tracers[i] = Instantiate(pathTracer[odds[which]]) as GameObject;
			tracers [i].transform.SetParent (this.transform);
			rands [i] = Mathf.Pow(((float)i/(float)amount+1f),.1f)+Random.value*(1f/(float)amount);
		}
		target = new GameObject ();
		rTarget = new GameObject();

        
	}
	
	void Update () {

		count += speed * Time.deltaTime;
		rCount -= speed * Time.deltaTime;
//		if(count>=1)
//			count=0;

		if (!linear) {
			for (int i =0; i < tracers.Length; i++) {

				float offset = rands [i];//Mathf.PerlinNoise((float)i*.13f,(float)i*.33f);
//				if (i % 2 == 0) {
					tracers [i].transform.localPosition = 
					spline.getPointClosed (offset*((float)i / ((float)amount - 1)) + count);
					target.transform.localPosition = spline.getPointClosed (offset*((float)i / ((float)amount - 1)) + count + .0001f);
					tracers [i].transform.LookAt (target.transform.localPosition);
//				Debug.Log (target.transform.localPosition);


//				} else {
//					tracers [i].transform.localPosition = spline.getPointClosed (offset*((float)i / ((float)amount - 1)) + rCount);
//					rTarget.transform.localPosition = spline.getPointClosed (offset*((float)i / ((float)amount - 1)) + rCount - .000001f);
//					tracers [i].transform.LookAt (rTarget.transform.localPosition);
//				}

			}
		} else {

			for (int i =0; i < tracers.Length; i++) {
				float offset = rands [i];// Mathf.PerlinNoise((float)i*.13f,(float)i*.33f);
				print (offset);

//				if (i % 2 == 0) {
					tracers [i].transform.localPosition = spline.getLinearPoint (offset*((float)i / ((float)amount - 1)) + count);
					target.transform.localPosition = spline.getLinearPoint (offset*((float)i / ((float)amount - 1)) + count + .000001f);
					tracers [i].transform.LookAt (target.transform.localPosition);
					
					
//				} else {
//					tracers [i].transform.localPosition = spline.getLinearPoint (offset*((float)i / ((float)amount - 1)) + rCount);
//					rTarget.transform.localPosition = spline.getLinearPoint (offset*((float)i / ((float)amount - 1)) + rCount - .000001f);
//					tracers [i].transform.LookAt (rTarget.transform.localPosition);
//				}
				
			}
		}
        
    }
    void OnDrawGizmos()
    {
//        for (int i = 0; i < spline.controlPointsList.Count; i++)
//        {
//            Gizmos.DrawWireSphere(spline.controlPointsList[i], 0.3f);
//        }
    }
}
