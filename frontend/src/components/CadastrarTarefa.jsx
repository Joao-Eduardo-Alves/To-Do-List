import { useState } from "react";

function CadastrarTarefa({ adicionarTarefa }) {
  const [descricao, setDescricao] = useState("");
  const [mostrarAviso, setMostrarAviso] = useState(false);

  const handleAdd = () => {
    if (!descricao.trim()) {
      setMostrarAviso(true);
      return;
    }
    adicionarTarefa(descricao);
    setDescricao("");
  };

  return (
    <div className="rounded-xl w-96 h-40 bg-[oklch(28.2%_0.091_267.935)] flex flex-col gap-10 py-2">
      <h1 className="text-white text-center font-bold text-4xl">
        Adicionar tarefa
      </h1>
      <div className="flex gap-4 justify-center">
        <input
          type="text"
          className="bg-gray-700 text-white p-3 rounded-xl focus:outline-none"
          placeholder="Digite uma tarefa..."
          onChange={(e) => {
            setDescricao(e.target.value);
            setMostrarAviso(false);
          }}
          value={descricao}
          onKeyDown={(e) => {
            if (e.key === "Enter") {
              handleAdd();
            }
          }}
        />
        <button
          onClick={handleAdd}
          className={`px-4 py-2 rounded-xl transition ${
            descricao.trim()
              ? "bg-blue-500 hover:bg-blue-600"
              : "bg-gray-500 cursor-not-allowed"
          }`}
        >
          Adicionar
        </button>
      </div>
      {mostrarAviso && (
        <p className="text-red-500 text-center w-full font-bold drop-shadow-xl">
          Descrição da tarefa não pode ser vazia
        </p>
      )}
    </div>
  );
}

export default CadastrarTarefa;
