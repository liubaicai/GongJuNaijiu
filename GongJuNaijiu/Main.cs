using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Harmony12;
using UnityModManagerNet;
using System.Reflection;

namespace GongJuNaijiu
{
    static class Main
    {
        static bool Load(UnityModManager.ModEntry modEntry)
        {
            Data.modEntry = modEntry;

            var harmony = HarmonyInstance.Create(modEntry.Info.Id);
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            return true;
        }
    }

    static class Data
    {
        public static UnityModManager.ModEntry modEntry { get; set; }
    }

    [HarmonyPatch(typeof(DateFile), "ChangeItemHp")]
    static class DateFile_ChangeItemHp_Patch
    {
        static void Postfix(ref int actorId, ref int itemId, ref int hpValue, ref int maxHpValue, ref bool removeItem)
        {
            DateFile df = DateFile.instance;

            var type = int.Parse(df.GetItemDate(itemId, 5));
            var level = int.Parse(df.GetItemDate(itemId, 8));
            if (type == 22 && level <= 3)
            {
                df.itemsDate[itemId][901] = df.itemsDate[itemId][902];
            }
        }
    }
}
