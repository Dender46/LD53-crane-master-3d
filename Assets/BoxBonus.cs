using UnityEngine;

public class BoxBonus : MonoBehaviour
{
    public enum BonusType
    {
        Regular, AddTime, BonusPoints
    }
    public BonusType _BonusType = BonusType.Regular;
}
