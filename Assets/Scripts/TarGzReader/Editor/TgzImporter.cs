using UnityEditor;
using UnityEngine;

using UnityEditor.AssetImporters;
using System.Collections.Generic;

namespace KageKirin.TarGzReader
{
    [ScriptedImporter(1, new[] { "tgz" })]
    public class TgzImporter : ScriptedImporter
    {
        public List<string> contents;

        public override void OnImportAsset(AssetImportContext ctx)
        {
            Debug.Log($"importing tgz: {ctx.assetPath}");

            using (var tar = new Tar(GzReader.ExtractGz(ctx.assetPath)))
            {
                contents = new List<string>(tar.dictionary.Keys);
            }
        }
    }
}
