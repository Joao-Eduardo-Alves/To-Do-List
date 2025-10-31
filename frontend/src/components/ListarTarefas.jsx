import { Trash2 } from "lucide-react";
import { CircleCheck } from "lucide-react";
import { useState } from "react";
function ListarTarefas({ tarefas, deletarTarefa, concluirTarefa, editarTarefa }) {
    const [editandoId, setEditandoId] = useState(null);

    return (
        <ul className="space-y-2">
            {tarefas.map((tarefa) => (
                <li key={tarefa.id} className="bg-[oklch(28.2%_0.091_267.935)] text-white p-4 rounded-xl flex items-center gap-6">
                    <button
                        onClick={() => concluirTarefa(tarefa.id)}
                        className={`flex items-center gap-2 p-2 rounded-sm transition ${tarefa.concluida ? 'bg-green-500 cursor-not-allowed' : 'bg-gray-400 hover:bg-green-400'}`}
                        disabled={tarefa.concluida}
                    >
                        <CircleCheck size={22} color="white"/>
                    </button> 

                    {editandoId === tarefa.id ? (
                        <input
                            type="text"
                            defaultValue={tarefa.descricao}
                            onBlur={(e) => {
                                editarTarefa(tarefa.id, e.target.value);
                                setEditandoId(null);
                            }}
                            className="bg-gray-700 text-white p-2 rounded w-full"
                            autoFocus
                        />
                    ) : (
                        <span
                            onClick={() => setEditandoId(tarefa.id)}
                            className={`flex-1 cursor-pointer hover:underline ${tarefa.concluida ? "line-through text-gray-400" : "text-white"
                                }`}
                        >
                            {tarefa.descricao}
                        </span>
                    )}

                    <button
                        onClick={() => deletarTarefa(tarefa.id)}
                        className="flex items-center bg-red-400 hover:bg-red-500 p-1 rounded-sm transition">
                        <Trash2 size={12} color="white" />
                    </button>  
                </li>
            ))}
        </ul>
    );
}
export default ListarTarefas;