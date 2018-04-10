using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frustration
{
    class Health
    {
        Enemy enemyScript;

        public float ChangeHealth (float health, float damage, int index)
        {
            health -= damage;
            if (health <= 0)
            {
                enemyScript.Destroy(index);
            }
            return health;
        }
    }
}
