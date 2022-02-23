using System.Data;
using System.IO;
using UnityEngine;

public sealed class SaveAndLoadRepository
{
    private LoadManager _LoadManager;

    private readonly IData<GameMemento> _data;

    private const string _folderName = "dataSave";
    private const string _fileName = "data.bat";
    private readonly string _path;

    public SaveAndLoadRepository(LoadManager loadManager)
    {
        _LoadManager = loadManager;
        _data = new JsonData<GameMemento>();
        _path = Path.Combine(Application.dataPath, _folderName);
    }

    public void Save(GameMemento gameMemento)
    {
        if (!Directory.Exists(Path.Combine(_path)))
        {
            Directory.CreateDirectory(_path);
        }
        var saveGame = gameMemento;

        _data.Save(saveGame, Path.Combine(_path, _fileName));
        Debug.Log("Save");
    }

    public void Load()
    {
        var file = Path.Combine(_path, _fileName);

        if (!File.Exists(file))
        {
            throw new DataException($"File {file} not found");
        }

        var savedData = _data.Load(file);
        _LoadManager.Load(savedData);
    }
}