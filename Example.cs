using KitchenMods;
using Kitchen;
using Unity.Entities;
using Unity.Collections;

namespace DonerModPU
{

    // Only gets loaded if we put IModSystem here
    public class Example : GenericSystemBase //, IModSystem
    {
        private EntityQuery Appliances;

        struct CHasBeenSetOnFire : IModComponent { }

        protected override void Initialise()
        {
            base.Initialise();
            Appliances = GetEntityQuery(new QueryHelper()
                .All(typeof(CAppliance))
                .None(
                    typeof(CFire),
                    typeof(CIsOnFire),
                    typeof(CFireImmune),
                    typeof(CHasBeenSetOnFire)
                ));
        }

        protected override void OnUpdate()
        {
            var appliances = Appliances.ToEntityArray(Allocator.TempJob);
            foreach (var appliance in appliances)
            {
                EntityManager.AddComponent<CIsOnFire>(appliance);
                EntityManager.AddComponent<CHasBeenSetOnFire>(appliance);
            }
            appliances.Dispose();
        }
    }
}