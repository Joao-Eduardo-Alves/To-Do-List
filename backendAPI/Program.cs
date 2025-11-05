using Microsoft.AspNetCore.Components.Forms;

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

var tarefas = new List<Tarefa>();

var proximoId = 1;

app.MapGet("/listarTarefas", () => tarefas.OrderBy(t => t.Ordem));

app.MapPost("/adicionarTarefa", (Tarefa novaTarefa) =>
{
    if (string.IsNullOrWhiteSpace(novaTarefa.Descricao))
    {
        return Results.BadRequest("A descrição da tarefa não pode ser nula.");
    }

    novaTarefa.Id = proximoId++;
    novaTarefa.Concluida = false;
    tarefas.Add(novaTarefa);
    return Results.Created($"/adicionarTarefa/{novaTarefa.Id}", novaTarefa);
});

app.MapDelete("/removerTarefa/{id}", (int id) =>
{
    var tarefaARemover = tarefas.FirstOrDefault(t => t.Id == id);
    if (tarefaARemover == null)
        return Results.NotFound($"Tarefa '{id}' não encontrada");

    tarefas.Remove(tarefaARemover);
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
    return Results.Ok($"Tarefa editada com sucesso.");
});

app.MapPut("/concluirTarefa/{id}", (int id) =>
{
    var tarefaAConcluir = tarefas.FirstOrDefault(t => t.Id == id);
    if (tarefaAConcluir == null)
        return Results.NotFound($"Tarefa não encontrada");

    tarefaAConcluir.Concluida = !tarefaAConcluir.Concluida;
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
    return Results.Ok("Ordem atualizada");
});

app.Run();

