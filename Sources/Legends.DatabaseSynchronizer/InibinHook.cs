﻿using Legends.Core.IO.Inibin;
using Legends.Core.IO.RAF;
using Legends.DatabaseSynchronizer.Attributes;
using Legends.Protocol.GameClient.Enum;
using Legends.Records;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.DatabaseSynchronizer
{
    public class InibinHook
    {
        [InibinMethod(typeof(ItemRecord))]
        public static RAFFileEntry[] GetItemInibin(RafManager manager)
        {
            return manager.GetFilesInDirectory("DATA/Items", ".inibin");
        }
        [InibinMethod(typeof(SkinRecord))]
        public static RAFFileEntry[] GetSkinsInibin(RafManager manager)
        {
            List<RAFFileEntry> results = new List<RAFFileEntry>();

            foreach (ChampionEnum champion in Enum.GetValues(typeof(ChampionEnum)))
            {
                string path = string.Format("DATA/Characters/{0}/Skins/", champion.ToString());
                var values = Array.FindAll(manager.GetFiles(path), x => x.Path.Contains(".inibin"));
                results.AddRange(values);

            }
            return results.ToArray();
        }
        [InibinMethod(typeof(SpellRecord))]
        public static RAFFileEntry[] GetSpellsInibin(RafManager manager)
        {
            return manager.GetFilesInDirectory("DATA/Spells", ".inibin");
        }
        [InibinMethod(typeof(AIUnitRecord))]
        public static RAFFileEntry[] GetChampionsInibin(RafManager manager)
        {
            List<RAFFileEntry> results = new List<RAFFileEntry>();

            var rf = manager.GetFiles("DATA/Characters/");
            rf = rf.ToList().FindAll(x => x.Path.Contains(".inibin")).ToArray();


            foreach (var f in rf)
            {
                string aiName = f.Path.Split('/')[2];

                string path = string.Format("DATA/Characters/{0}/{0}.inibin", aiName);


                if (manager.Exists(path) && results.Find(x => x.Path == path) == null)
                    results.Add(manager.GetFile(path));
            }

            return results.ToArray();

        }
    }
}
