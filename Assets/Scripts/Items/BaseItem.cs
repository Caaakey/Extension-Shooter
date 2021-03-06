using UnityEngine;

namespace YourName.SurvivalShooter
{
    //  abstract ? 
    //  추상 클래스
    //  단독으론 사용 불가!
    //  무조건 상속해야 사용 가능
    public abstract class BaseItem : MonoBehaviour
    {
        public int Price;

        public abstract void Buy();
        public abstract void Sell();
        public abstract void Use();

    }
}
