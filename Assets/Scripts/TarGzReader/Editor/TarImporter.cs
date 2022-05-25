using UnityEditor;
using UnityEngine;

using UnityEditor.AssetImporters;
using System.Collections.Generic;

namespace KageKirin.TarGzReader
{
    [ScriptedImporter(1, new[] { "tar" })]
    public class TarImporter : ScriptedImporter
    {
        public List<string> contents;

        public override void OnImportAsset(AssetImportContext ctx)
        {
            Debug.Log($"importing tar: {ctx.assetPath}");

            using (var tar = new Tar(ctx.assetPath))
            {
                contents = new List<string>(tar.dictionary.Keys);
            }
        }
    }
}
