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
            Console.WriteLine("==================================");
            Console.WriteLine("1 - cadastrar tarefa:");
            Console.WriteLine("2 - listar tarefas:");
            Console.WriteLine("3 - marcar tarefa como concluída:");
            Console.WriteLine("4 - Remover tarefa:");
            Console.WriteLine("5 - sair:");
            Console.WriteLine("");
            Console.WriteLine("Digite a opção desejada:");

            string opcao = Console.ReadLine();
            Console.WriteLine("");

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
    static int proximoID = 1;
    static void CadastrarTarefa(List<Tarefa> tarefas)
    {
        Console.WriteLine("Digite a descrição da tarefa:");
        Console.WriteLine("");
        string descricao = Console.ReadLine();
        Console.WriteLine("");

        Tarefa novaTarefa = new Tarefa
        {
            Id = proximoID++,
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

        int numeroExibicao = 1;

        if (tarefas.Count == 0)
        {
            Console.WriteLine("Não há tarefas cadastradas.");
            return;
        }
        foreach (var tarefa in tarefas)
        {
            string status = tarefa.Concluida ? "CONCLUÍDA" : "NÃO CONCLUÍDA";

            Console.WriteLine("-------------------------------------");
            Console.WriteLine($"{numeroExibicao++} - Tarefa: {tarefa.Descricao} - {status}");
            Console.WriteLine("");
            Console.WriteLine("-------------------------------------");
        }
    }
    static void MarcarListaConcluida(List<Tarefa> tarefas)
    {
        if (tarefas.Count == 0)
        {
            Console.WriteLine("Não há tarefas cadastradas!");
            return;
        }

        Console.WriteLine("Digite o número da tarefa que deseja concluir:");

        if (int.TryParse(Console.ReadLine(), out int numeroConcluir))
        {
            if (numeroConcluir >= 1 && numeroConcluir <= tarefas.Count)
            {
                tarefas[numeroConcluir - 1].MarcarConcluida();
                Console.WriteLine("Tarefa concluída com sucesso!");

                ListarTarefas(tarefas);
            }
            else
            {
                Console.WriteLine("Tarefa inválida");
                Console.WriteLine("");
            }
        }
    }
    static void DeletarTarefa(List<Tarefa> tarefas)
    {
        if (tarefas.Count == 0)
        {
            Console.WriteLine("Não há tarefas cadastradas!");
            return;
        }

        Console.WriteLine("Digite o número da tarefa a ser removida:");

        if (int.TryParse(Console.ReadLine(), out int numeroExibicao))
        {
            Console.WriteLine("");

            if (numeroExibicao >= 1 && numeroExibicao <= tarefas.Count)
            {
                tarefas.RemoveAt(numeroExibicao - 1);
                Console.WriteLine("Tarefa removida!");
            }
            else
            {
                Console.WriteLine($"Número inválido! Digite um valor entre 1 e {tarefas.Count}");
                Console.WriteLine("");
            }
        }
        else
        {
            Console.WriteLine("Entrada Inválida! Digite apenas números");
        }
    }
}



//Funcionalidades futura:
//Salvar as tarefas em um arquivo de texto para que elas persistam entre execuções do programa
//Adicionar interface desktop (WPF).