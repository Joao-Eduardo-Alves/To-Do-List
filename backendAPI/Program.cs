using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseHttpsRedirection();

string caminhoArquivo = "tarefas.json";

List<Tarefa> CarregarTarefas()
{
    if (!File.Exists(caminhoArquivo))
        return new List<Tarefa>();

    string json = File.ReadAllText(caminhoArquivo);
    return JsonSerializer.Deserialize<List<Tarefa>>(json) ?? new List<Tarefa>();
}

void SalvarTarefas(List<Tarefa> tarefas)
{
    string json = JsonSerializer.Serialize(tarefas, new JsonSerializerOptions { WriteIndented = true });
    File.WriteAllText(caminhoArquivo, json);
    //Se não existir arquivo JSON, ele será criado automaticamente
}

var tarefas = CarregarTarefas();

var proximoId = tarefas.Any() ? tarefas.Max(t => t.Id) + 1 : 1;

app.MapGet("/listarTarefas", () => tarefas.OrderBy(t => t.Ordem));

app.MapPost("/adicionarTarefa", (Tarefa novaTarefa) =>
{
    if (string.IsNullOrWhiteSpace(novaTarefa.Descricao))
    {
        return Results.BadRequest("A descrição da tarefa não pode ser nula.");
    }

    novaTarefa.Id = proximoId++;
    novaTarefa.Concluida = false;
    novaTarefa.Ordem = tarefas.Count > 0 ? tarefas.Max(t => t.Ordem) + 1 : 0;
    tarefas.Add(novaTarefa);

    SalvarTarefas(tarefas);

    return Results.Created($"/adicionarTarefa/{novaTarefa.Id}", novaTarefa);
});

app.MapDelete("/removerTarefa/{id}", (int id) =>
{
    var tarefaARemover = tarefas.FirstOrDefault(t => t.Id == id);
    if (tarefaARemover == null)
        return Results.NotFound($"Tarefa '{id}' não encontrada");

    tarefas.Remove(tarefaARemover);
    SalvarTarefas(tarefas);

    return Results.Ok($"Tarefa '{id}' removida com sucesso.");
});

app.MapPut("/editarTarefa/{id}", (int id, Tarefa tarefaEditada) =>
{
    var tarefaAEditar = tarefas.FirstOrDefault(t => t.Id == id);
    if (tarefaAEditar == null)
        return Results.NotFound($"Tarefa não encontrada");

    if (string.IsNullOrWhiteSpace(tarefaEditada.Descricao))
    {
        return Results.BadRequest("A descrição da tarefa não pode ser nula.");
    }
    tarefaAEditar.Descricao = tarefaEditada.Descricao;
    SalvarTarefas(tarefas);

    return Results.Ok($"Tarefa editada com sucesso.");
});

app.MapPut("/concluirTarefa/{id}", (int id) =>
{
    var tarefaAConcluir = tarefas.FirstOrDefault(t => t.Id == id);
    if (tarefaAConcluir == null)
        return Results.NotFound($"Tarefa não encontrada");

    tarefaAConcluir.Concluida = !tarefaAConcluir.Concluida;
    SalvarTarefas(tarefas);

    return Results.Ok(tarefaAConcluir);
});

app.MapPost("/atualizarOrdem", (List<Tarefa> tarefasReordenadas) =>
{
    foreach (var t in tarefasReordenadas)
    {
        var tarefa = tarefas.FirstOrDefault(x => x.Id == t.Id);
        if (tarefa != null)
        {
            tarefa.Ordem = t.Ordem;
        }
    }
    SalvarTarefas(tarefas);

    return Results.Ok("Ordem atualizada");
});

app.Run();

