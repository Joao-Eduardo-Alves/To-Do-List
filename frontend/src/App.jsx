import { useEffect } from 'react'
import { useState } from 'react'
import CadastrarTarefa from './components/CadastrarTarefa.jsx'
import ListarTarefas from './components/ListarTarefas';
function App() {

    const [tarefas, setTarefas] = useState([]);

    const listarTarefas = async () => {
        const res = await fetch('/listarTarefas');
        const data = await res.json();
        setTarefas(data);
    };

    const adicionarTarefa = async (descricao) => {

        const res = await fetch('/adicionarTarefa', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ descricao })
        });

        if (!res.ok) {
            const erro = await res.text();
            alert("Erro ao adicionar tarefa." + erro);
            return;
        }
        const tarefa = await res.json();
        setTarefas([...tarefas, tarefa]);
    };

    const deletarTarefa = async (id) => {
        try {
            const res = await fetch(`/removerTarefa/${id}`, {
                method: 'DELETE',
            });

            if (res.ok) {
                setTarefas(tarefas.filter((tarefa) => tarefa.id !== id));
                console.log("Tarefa deletada com sucesso");
            }
            else {
                console.log("Tarefa não encontrada");
            }

        } catch (erro) {
            console.error("Erro na requisição:", erro);
        }
    };

    const editarTarefa = async (id, novaDescricao) => {
        try {
            const res = await fetch(`/editarTarefa/${id}`, {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ descricao: novaDescricao })
            });

            const mensagem = await res.text();

            alert(mensagem);

            if (!res.ok) {
                return;
            }

            setTarefas(prev =>
                prev.map(tarefa =>
                    tarefa.id === id ? { ...tarefa, descricao: novaDescricao } : tarefa
                )
            );

        } catch (error) {
            console.error(error);
        }
    };

    const concluirTarefa = async (id) => {

        const res = await fetch(`/concluirTarefa/${id}`, {
            method: 'PUT',
        });

        const mensagem = await res.text();

        if (!res.ok) {
            alert(mensagem);
            return;
        }
        setTarefas(tarefas.map(t =>
            t.id === id ? { ...t, concluida: true } : t
        ));

        alert(mensagem);
    };

    useEffect(() => {
        listarTarefas();
    }, []);

    return (
        <div className="min-h-screen overflow-y-auto bg-gradient-to-t from-blue-300 to-blue-900 flex flex-col items-center gap-12 py-8 px-4">
            <CadastrarTarefa adicionarTarefa={adicionarTarefa} />
            <ListarTarefas tarefas={tarefas} setTarefas={setTarefas} deletarTarefa={deletarTarefa} concluirTarefa={concluirTarefa} editarTarefa={editarTarefa} />
        </div>
    );
}

export default App
