using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColorController : MonoBehaviour
{
    [Header("--- Colors ---")]
    public Color hairColor;
    public Color skinColor;
    public Color shirtColor;
    public Color pantsColor;
    public Color shoeColor;

    [Header("--- Renderers ---")]
    [SerializeField] private SpriteRenderer playerHairRend;
    [SerializeField] private List<SpriteRenderer> playerSkinRends = new List<SpriteRenderer>();
    [SerializeField] private List<SpriteRenderer> playerShirtRends = new List<SpriteRenderer>();
    [SerializeField] private List<SpriteRenderer> playerPantsRends = new List<SpriteRenderer>();
    [SerializeField] private List<SpriteRenderer> playerShoeRends = new List<SpriteRenderer>();

    private Color lastHairColor = Color.clear;
    private Color lastSkinColor = Color.clear;
    private Color lastShirtColor = Color.clear;
    private Color lastPantsColor = Color.clear;
    private Color lastShoeColor = Color.clear;

	[ExecuteAlways]
	[HideInInspector]
	public void OnGUI()
	{
        Update();
	}


	//// Start is called before the first frame update
	//void Start()
 //   {
 //       lastHairColor = hairColor;
 //       lastSkinColor = skinColor;
 //       lastShirtColor = shirtColor;
 //       lastPantsColor = pantsColor;
 //       lastShoeColor = shoeColor;
 //   }

    // Update is called once per frame
    void Update()
    {
        if(hairColor != lastHairColor)
        {
            playerHairRend.color = hairColor;
            lastHairColor = hairColor;
        }

        if(skinColor != lastSkinColor)
        {
            for(int i = 0; i < playerSkinRends.Count; i++)
            {
                playerSkinRends[i].color = skinColor;
			}
            lastSkinColor = skinColor;
		}

        if(shirtColor != lastShirtColor)
        {
            for (int i = 0; i < playerShirtRends.Count; i++)
            {
                playerShirtRends[i].color = shirtColor;
            }
            lastShirtColor = shirtColor;
        }

        if (pantsColor != lastPantsColor)
        {
            for (int i = 0; i < playerPantsRends.Count; i++)
            {
                playerPantsRends[i].color = pantsColor;
            }
            lastPantsColor = pantsColor;
        }

        if (shoeColor != lastShoeColor)
        {
            for (int i = 0; i < playerShoeRends.Count; i++)
            {
                playerShoeRends[i].color = shoeColor;
            }
            lastShoeColor = shoeColor;
		}
    }
}
