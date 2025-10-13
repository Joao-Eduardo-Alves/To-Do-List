public class Tarefa
{
    public int Id { get; set; }
    public string Descricao { get; set; }
    public bool Concluida { get; private set; }
    public void MarcarConcluida()
    {
        if (!Concluida)
            Concluida = true;
    }
}
class Program
{
    static void Main(string[] args)
    {
        bool rodando = true;

        Console.WriteLine("Bem-vindo ao sistema de tarefas!");
        Console.WriteLine("=================================");
        Console.WriteLine();

        List<Tarefa> tarefas = new List<Tarefa>();

        while (rodando)
        {
            Console.WriteLine("1 - cadastrar tarefa:");
            Console.WriteLine("2 - listar tarefas:");
            Console.WriteLine("3 - marcar tarefa como concluída:");
            Console.WriteLine("4 - sair:");
            Console.WriteLine("Digite a opção desejada:");

            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    CadastrarTarefa(tarefas);
                    break;

                case "2":
                    ListarTarefas(tarefas);
                    break;

                case "3":
                    MarcarListaConcluida(tarefas);  
                    break;

                case "4":
                    rodando = false;
                    break;

                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
    }
    static void CadastrarTarefa(List<Tarefa> tarefas)
    {
        Console.WriteLine("Digite a descrição da tarefa:");
        Console.WriteLine("");
        string descricao = Console.ReadLine();

        Tarefa novaTarefa = new Tarefa
        {
            Id = tarefas.Count + 1,
            Descricao = descricao
        };

        tarefas.Add(novaTarefa);

        Console.WriteLine("Tarefa cadastrada com sucesso!");
        Console.WriteLine("");
    }
    static void ListarTarefas(List<Tarefa> tarefas)
    {
        Console.WriteLine("");
        Console.WriteLine("Tarefas cadastradas:");
        Console.WriteLine("");

        if (tarefas.Count == 0)
        {
            Console.WriteLine("Nenhuma tarefa cadastrada.");
            return;
        }

        foreach (var tarefa in tarefas)
        {
            Console.WriteLine($"ID: {tarefa.Id}, Descrição: {tarefa.Descricao}, Concluída: {tarefa.Concluida}");
            Console.WriteLine("");
        }
    }
    static void MarcarListaConcluida(List<Tarefa> tarefas)
    {
        Console.WriteLine("Digite o ID da tarefa que deseja marcar como concluída:");

        int id = int.Parse(Console.ReadLine());

        var tarefaConcluida = tarefas.FirstOrDefault(t => t.Id == id);

        if (tarefaConcluida != null)
        {
            tarefaConcluida.MarcarConcluida();

            Console.WriteLine("Tarefa marcada como concluída!");
            Console.WriteLine("");
        }
        else
        {
            Console.WriteLine("Tarefa não encontrada.");
        }
    }
}



//Funcionalidade futura: Salvar as tarefas em um arquivo de texto para que elas persistam entre execuções do programa.