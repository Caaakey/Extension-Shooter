using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourName.SurvivalShooter.Consumable
{
    public class Health : ConsumableItem
    {
        public override void Use()
        {
            PlayerStatus.Get.CurrentHP += 100;

        }
    }
}
