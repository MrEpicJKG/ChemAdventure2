using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ModHealthBarColorByValue : MonoBehaviour
{
 //   [SerializeField] [Range(0f, 1f)] private float percentHealthForMaxColor = 0.5f; //All amounts of health above this percentage will simply be the max color.
 //   [SerializeField] private Color maxColor; //set by reading the starting color of the Fill
 //   [SerializeField] private Color minColor;
 //   [SerializeField] private Image fill;

 //   private float colorStartHealthAmt = 50;
 //   private Slider healthSlider;
 //   private float diffR;
 //   private float diffG;
 //   private float diffB;
 //   // Start is called before the first frame update
 //   void Start()
 //   {
 //       healthSlider = GetComponent<Slider>();
 //       colorStartHealthAmt = healthSlider.maxValue * percentHealthForMaxColor;
 //       //maxColor = fill.color;
 //       diffR = maxColor.r - minColor.r;
 //       diffG = maxColor.g - minColor.g;
 //       diffB = maxColor.b - minColor.b;
 //       healthSlider.value = healthSlider.maxValue;
 //   }

 //   public void OnSliderValueChanged(float value)
 //   {
 //       value /= 100; 
 //       if(value <= colorStartHealthAmt)
 //       {
 //           float partialVal = healthSlider.value * percentHealthForMaxColor;
 //           fill.color = new Color(diffR * partialVal, diffG * partialVal, diffB * partialVal);
	//	}
 //       else
 //       {
 //           fill.color = maxColor;
	//	}
	//}
}
