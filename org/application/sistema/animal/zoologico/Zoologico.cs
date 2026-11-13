using Application.Sistema.Animais.Zoologico.Aves;
using Application.Sistema.Animais.Zoologico.Mamiferos;
using Application.Sistema.Animais.Zoologico.Mamiferos.Felinos;
using Application.Sistema.Animais.Zoologico.Mamiferos.Primatas;
using Application.Sistema.Animais.Zoologico.Repteis;

namespace Application.Sistema.Animais.Zoologico;

public class Zoologico
{
    private readonly List<Animal> _animais;

    public string   Nome                { get; }
    public int      CapacidadeMaxima    { get; }

    public Zoologico(string nome, int capacidadeMaxima)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            throw new ArgumentException("Nome do zoologico nao pode ser vazio.");
        }

        if (capacidadeMaxima <= 0)
        {
            throw new ArgumentException("Capacidade deve ser positiva.");
        }

        Nome = nome;
        CapacidadeMaxima = capacidadeMaxima;
        _animais = new List<Animal>();
    }

    public int TotalAnimais => _animais.Count;

    // --- RETORNA UMA CÓPIA DA LISTA DE ANIMAIS ---
    public List<Animal> GetAnimais()
    {
        return new List<Animal>(_animais);
    }

    public bool AdicionarAnimal(Animal animal)
    {
        if (_animais.Count >= CapacidadeMaxima)
        {
            Console.WriteLine("Zoologico cheio. Nao foi possivel adicionar " + animal.Nome + ".");
            return false;
        }

        _animais.Add(animal);
        return true;
    }

    public bool RemoverAnimal(string nomeAnimal)
    {
        Animal? alvo = BuscarPorNome(nomeAnimal);
        if (alvo != null)
        {
            _animais.Remove(alvo);
            Console.WriteLine(nomeAnimal + " foi removido do zoologico.");
            return true;
        }

        Console.WriteLine(nomeAnimal + " nao foi encontrado.");
        return false;
    }

    public Animal? BuscarPorNome(string nomeAnimal)
    {
        return _animais.FirstOrDefault(a => a.Nome.Equals(nomeAnimal, StringComparison.OrdinalIgnoreCase));
    }

    // --- LISTA OS ANIMAIS DE UM TIPO (INCLUINDO SUBCLASSES) ---
    public List<T> ListarPorTipo<T>() where T : Animal
    {
        return _animais.OfType<T>().ToList();
    }

    public void ExibirTodos()
    {
        Console.WriteLine("\n=== ANIMAIS DO " + Nome.ToUpper() + " ===");
        if (_animais.Count == 0)
        {
            Console.WriteLine("(Nenhum animal cadastrado)");
            return;
        }

        foreach (Animal a in _animais)
        {
            Console.WriteLine("- " + a);
        }
    }

    public void ExibirContagemPorCategoria()
    {
        long aves = ListarPorTipo<Ave>().Count;
        long mamiferos = ListarPorTipo<Mamifero>().Count;
        long felinos = ListarPorTipo<Felino>().Count;
        long primatas = ListarPorTipo<Primata>().Count;
        long repteis = ListarPorTipo<Reptil>().Count;

        Console.WriteLine("\n=== CONTAGEM POR CATEGORIA ===");
        Console.WriteLine("Total geral: " + _animais.Count + " / " + CapacidadeMaxima);
        Console.WriteLine("Aves: " + aves);
        Console.WriteLine("Mamiferos: " + mamiferos + " (sendo " + felinos + " felinos e " + primatas + " primatas)");
        Console.WriteLine("Repteis: " + repteis);
    }

    public void AlimentarTodos()
    {
        Console.WriteLine("\n=== HORA DA ALIMENTACAO ===");
        _animais.ForEach(a => a.Alimentar());
    }

    public void ColocarParaDormir()
    {
        Console.WriteLine("\n=== HORA DE DORMIR ===");
        _animais.ForEach(a => a.Dormir());
    }

    public void CoroDosAnimais()
    {
        Console.WriteLine("\n=== CORO DOS ANIMAIS ===");
        _animais.ForEach(a => a.EmitirSom());
    }
}
