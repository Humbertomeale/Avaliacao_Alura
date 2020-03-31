using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.IO;
using System.Collections.ObjectModel;

public class SalvadorDePlacar : MonoBehaviour
{
    //privadas//
    [SerializeField]
    private static string arquivoSalvamento = "Ranking.json";
    private string caminhoParaOArquivo;
    [SerializeField]
    private List<ListaDeColocados> lista;

    //-------//

    private void Awake()
    {
        caminhoParaOArquivo = Path.Combine(Application.persistentDataPath, arquivoSalvamento);
        Debug.Log(caminhoParaOArquivo);

        if (File.Exists(caminhoParaOArquivo))
        {
            // Abaixo, caminho para salvar o arquivo e carregá-lo novamente
            var textoJson = File.ReadAllText(caminhoParaOArquivo);
            JsonUtility.FromJsonOverwrite(textoJson, this);
        }
        else
        {
            lista = new List<ListaDeColocados>();
        }
    }


    public string AdicionarColocacao(string nome, string tempo, int pontos)
    {
        string id = GerarID();
        var novoColocado = new ListaDeColocados(id, nome, tempo, pontos);
        lista.Add(novoColocado);
        lista.Sort();
        this.salvarDados();
        return id;
    }

    private void salvarDados()
    {
        var textoJson = JsonUtility.ToJson(this);
        File.WriteAllText(this.caminhoParaOArquivo, textoJson);
        Debug.Log(Application.persistentDataPath);
    }

    private string GerarID()
    {
        var id = Guid.NewGuid().ToString();
        return id;
    }

    public ReadOnlyCollection<ListaDeColocados> PegarColocados()
    {
        return lista.AsReadOnly();
    }

    public int Quantidade()
    {
        return this.lista.Count;
    }

    public void AlterarNome(string novoNome, string id)
    {
        foreach (var item in this.lista)
        {
            if (item.ID == id)
            {
                item.Nome = novoNome;
                break;
            }
        }
        salvarDados();
    }


}
[System.Serializable]
public class ListaDeColocados:IComparable
{
    public string ID;
    public string Nome;
    public string Tempo;
    public int Pontos;

    public  ListaDeColocados(string ID, string Nome, string Tempo, int Pontos)
    {
        this.ID = ID;
        this.Nome = Nome;
        this.Tempo = Tempo;
        this.Pontos = Pontos;
    }

    public int CompareTo(object obj)
    {
        var outroObjeto = obj as ListaDeColocados;
        return outroObjeto.Pontos.CompareTo(Pontos);
    }
}
