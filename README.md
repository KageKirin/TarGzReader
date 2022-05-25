# TarGzReader

A relatively simple and straightforward class to read the file data from a `.tar` archive,
with or without `gz` compression.

## Notes

- `PaxHeader` entries are skipped, since we mostly target to get the entries data, not their metadata.

[More on PaxHeaders](https://pubs.opengroup.org/onlinepubs/9699919799/utilities/pax.html#tag_20_92_13_03).

## Usage

The intended use-case is the retrieval of the contained files, e.g. by an asset importer.

As an example, 3 `ScriptedImporters` are part of the repo, BUT NOT of the package.

### `.tar` file

```csharp
using (var tar = new Tar(assetPath))
{
    contents = new List<string>(tar.dictionary.Keys);
}
```

### `.tgz` or `.tar.gz` file

```csharp
using (var tar = new Tar(GzReader.ExtractGz(assetPath)))
{
    contents = new List<string>(tar.dictionary.Keys);
}
```
