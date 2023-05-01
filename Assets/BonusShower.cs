using UnityEngine;
using UnityEngine.UI;

public class BonusShower : MonoBehaviour
{
    float _Timer;

    void Update()
    {
        _Timer += Time.deltaTime;
        
        if (_Timer > 3)
        {
            gameObject.SetActive(false);
        }

    }

    public void ShowBonus(string bonusName)
    {
        gameObject.SetActive(true);
        _Timer = 0;

        GetComponent<Text>().text = "Bonus:\n" + bonusName;
    }
}
