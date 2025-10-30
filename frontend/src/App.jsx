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

    const concluirTarefa = async (id) => {

        const res = await fetch(`/concluirTarefa/${id}`, {
            method: 'PUT',
        });

        if (!res.ok) {
            const erro = await res.text();
            alert(`Erro ao concluir tarefa ${id}: ${erro}`);
            return;
        }

        const mensagem = await res.text();
        alert(mensagem);
    };

        useEffect(() => {
            listarTarefas();
        }, []);

        return (
            <div className="flex flex-col gap-15 items-center justify-start h-screen bg-gradient-to-t from-blue-300 to-blue-900">
                <CadastrarTarefa adicionarTarefa={adicionarTarefa} />
                <ListarTarefas tarefas={tarefas} deletarTarefa={deletarTarefa} concluirTarefa={concluirTarefa} />
            </div>
        );
}

export default App
