using UnityEngine;
using UnityEngine.UI;

public class CombatSpriteController
{
    private Image _image;
    private Sprite[] _skillSprites;
    public CombatSpriteController(Image image, Sprite[] skillSprites) 
    { 
        _image = image;
        _skillSprites = skillSprites;
    }

    public void SortRandomSkillSprite()
    {
        _image.sprite = _skillSprites[Random.Range(0, _skillSprites.Length-1)];
    }
}
