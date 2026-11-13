using System.Text;
using Application.Sistema.Animais.Zoologico.Aves;
using Application.Sistema.Animais.Zoologico.Mamiferos;
using Application.Sistema.Animais.Zoologico.Mamiferos.Felinos;
using Application.Sistema.Animais.Zoologico.Mamiferos.Primatas;
using Application.Sistema.Animais.Zoologico.Repteis;

namespace Application.Sistema.Animais.Zoologico;

public class Programa
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        var zoo = new Zoologico("Zoologico Municipal", 20);

        zoo.AdicionarAnimal(new Ema("Perna", 5));
        zoo.AdicionarAnimal(new Flamingo("Cheirinho", 2));
        zoo.AdicionarAnimal(new Ganso("Paulo Henrique", 4));
        zoo.AdicionarAnimal(new Arara("Azul", 3));
        zoo.AdicionarAnimal(new Tucano("Tuquinha", 6));

        zoo.AdicionarAnimal(new Elefante("Dumbo", 10));
        zoo.AdicionarAnimal(new Girafa("Melman", 8));
        zoo.AdicionarAnimal(new Zebra("Marty", 7));

        zoo.AdicionarAnimal(new Leao("Simba", 5));
        zoo.AdicionarAnimal(new Tigre("Sher Khan", 12));
        zoo.AdicionarAnimal(new Onca("Onix", 4));

        zoo.AdicionarAnimal(new Chimpanze("Cesar", 9));
        zoo.AdicionarAnimal(new Gorila("King", 15));
        zoo.AdicionarAnimal(new MicoLeaoDourado("Tico", 3));

        zoo.AdicionarAnimal(new Cobra("Kaa", 6));
        zoo.AdicionarAnimal(new Jacare("Tic Tac", 20));
        zoo.AdicionarAnimal(new Tartaruga("Donatello", 50));

        zoo.ExibirTodos();
        zoo.ExibirContagemPorCategoria();

        Console.WriteLine("\n=== APRESENTACAO DOS ANIMAIS ===");
        foreach (Animal a in zoo.GetAnimais())
        {
            a.Apresentar();
        }

        zoo.CoroDosAnimais();
        zoo.AlimentarTodos();
        zoo.ColocarParaDormir();

        Console.WriteLine("\n=== BUSCA ===");
        Animal? encontrado = zoo.BuscarPorNome("Simba");
        if (encontrado != null)
        {
            Console.WriteLine("Encontrado: " + encontrado);
        }

        Console.WriteLine("\n=== REMOCAO ===");
        zoo.RemoverAnimal("Marty");

        zoo.ExibirContagemPorCategoria();
    }
}
