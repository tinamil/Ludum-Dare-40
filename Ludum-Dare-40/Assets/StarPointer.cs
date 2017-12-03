using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarPointer : MonoBehaviour
{

    public GameObject star;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (star == null)
        {
            Destroy(gameObject);
            return;
        }
        var screenPoint = Camera.main.WorldToScreenPoint(star.transform.position);
        if (screenPoint.x > 0 && screenPoint.x < Camera.main.pixelWidth && screenPoint.y > 0 && screenPoint.y < Camera.main.pixelHeight)
        {
            GetComponent<Image>().enabled = false;
            return;
        } else
        {
            GetComponent<Image>().enabled = true;
            var centerScreen = new Vector2(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);
            var relative = centerScreen - (Vector2)screenPoint;
            var angle = Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg + 180;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            var slope = relative.y / relative.x;

            Vector2 indicatorPos;
            if (relative.y < 0) //Above the screen
            {
                indicatorPos.x = centerScreen.y / slope;
                indicatorPos.y = centerScreen.y;
            } else //Below the screen
            {
                indicatorPos.x = -centerScreen.y / slope;
                indicatorPos.y = -centerScreen.y;
            }
            if (indicatorPos.x < -centerScreen.x) //Far left
            {
                indicatorPos.x = -centerScreen.x;
                indicatorPos.y = slope * -centerScreen.x;
            } else if (indicatorPos.x > centerScreen.x) //Far right
            {
                indicatorPos.x = centerScreen.x;
                indicatorPos.y = slope * centerScreen.x;
            }
            GetComponent<RectTransform>().anchoredPosition = indicatorPos + centerScreen;
        }
    }
}
