public class Tarefa
{
    public int NumeroTarefa { get; set; }
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
            Console.WriteLine("4 - Remover tarefa:");
            Console.WriteLine("5 - sair:");
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
                    DeletarTarefa(tarefas);
                    break;

                case "5":
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
            NumeroTarefa = tarefas.Count + 1,
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
            string status = tarefa.Concluida ? "CONCLUÍDA" : "NÃO CONCLUÍDA";
            Console.WriteLine($"{tarefa.NumeroTarefa}, Tarefa: {tarefa.Descricao} - {status}");
            Console.WriteLine("");
        }
    }
    static void MarcarListaConcluida(List<Tarefa> tarefas)
    {
        Console.WriteLine("Digite o número da tarefa que deseja marcar como concluída:");

        int numero = int.Parse(Console.ReadLine());

        var tarefaConcluida = tarefas.FirstOrDefault(t => t.NumeroTarefa == numero);

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
    static void DeletarTarefa(List<Tarefa> tarefas)
    {
        Console.WriteLine("Digite o número da tarefa a ser removida");
        int numeroTarefa = int.Parse(Console.ReadLine());

        var tarefaRemovida = tarefas.FirstOrDefault(t => t.NumeroTarefa == numeroTarefa);

        if (tarefaRemovida != null)
        {
            tarefas.Remove(tarefaRemovida);
            Console.WriteLine("Tarefa removida com sucesso!");
        }
    }
}



//Funcionalidade futura: Salvar as tarefas em um arquivo de texto para que elas persistam entre execuções do programa.