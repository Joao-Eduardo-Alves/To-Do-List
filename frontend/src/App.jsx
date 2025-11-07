import { useEffect } from "react";
import { useState } from "react";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import CadastrarTarefa from "./components/CadastrarTarefa.jsx";
import ListarTarefas from "./components/ListarTarefas";

function App() {
  const aviso = (msg) =>
    toast.warn(msg, {
      position: "top-center",
      autoClose: 2000,
      hideProgressBar: false,
      closeOnClick: false,
      pauseOnHover: true,
      draggable: true,
      progress: undefined,
      theme: "dark",
    });

  const [tarefas, setTarefas] = useState([]);

  const listarTarefas = async () => {
    const res = await fetch("/listarTarefas");
    const data = await res.json();
    setTarefas(data);
  };

  const adicionarTarefa = async (descricao) => {
    const res = await fetch("/adicionarTarefa", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ descricao }),
    });

    if (!res.ok) {
      const mensagem = await res.text();
      aviso(mensagem);
      return;
    }
    const tarefa = await res.json();
    setTarefas([...tarefas, tarefa]);
  };

  const deletarTarefa = async (id) => {
    const res = await fetch(`/removerTarefa/${id}`, {
      method: "DELETE",
    });

    if (res.ok) {
      setTarefas(tarefas.filter((tarefa) => tarefa.id !== id));
    }
  };

  const editarTarefa = async (id, novaDescricao) => {
    const res = await fetch(`/editarTarefa/${id}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ descricao: novaDescricao }),
    });

    if (!res.ok) {
      const mensagem = await res.text();
      aviso(mensagem);
      return;
    }

    setTarefas((prev) =>
      prev.map((tarefa) =>
        tarefa.id === id ? { ...tarefa, descricao: novaDescricao } : tarefa
      )
    );
  };

  const concluirTarefa = async (id) => {
    const res = await fetch(`/concluirTarefa/${id}`, {
      method: "PUT",
    });

    const tarefaAtualizada = await res.json();

    setTarefas(
      tarefas.map((t) =>
        t.id === id ? { ...t, concluida: tarefaAtualizada.concluida } : t
      )
    );
  };

  useEffect(() => {
    listarTarefas();
  }, []);

  return (
    <div className="min-h-screen overflow-y-auto bg-linear-to-t from-blue-300 to-blue-900 flex flex-col items-center gap-12 py-8 px-4">
      <CadastrarTarefa adicionarTarefa={adicionarTarefa} />
      <ListarTarefas
        tarefas={tarefas}
        setTarefas={setTarefas}
        deletarTarefa={deletarTarefa}
        concluirTarefa={concluirTarefa}
        editarTarefa={editarTarefa}
      />
      <ToastContainer />
    </div>
  );
}

export default App;
